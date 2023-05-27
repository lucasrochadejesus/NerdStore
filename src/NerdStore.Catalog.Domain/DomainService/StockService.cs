using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Core.Bus;

namespace NerdStore.Catalog.Domain.DomainService
{
    public class StockService : IStockService
    {

        private readonly IProductRepository _productRepository;
        private readonly IMediatrHandler _bus;

        public StockService(IProductRepository productRepository, 
                            IMediatrHandler bus)
        {
            _productRepository = productRepository;
            _bus = bus;
        }

        public async Task<bool> IncreaseStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetProductById(productId);

            if (product == null) return false;

            product.IncreaseStock(quantity);

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();

        }

        public async Task<bool> DecreaseStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetProductById(productId);

            if (product == null) return false;

            if (!product.HasStock(quantity)) return false;

            product.DecreaseStock(quantity);

            if (product.StockQuantity < 10)
            {
                await _bus.PublishEvent(new LowStockProductEvent(product.Id, product.StockQuantity));
            }

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();

        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}
