using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects.DTO;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Catalog.Domain.DomainService
{
    public class StockService : IStockService
    {

        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public StockService(IProductRepository productRepository, 
                            IMediatorHandler mediatorHandler)
        {
            _productRepository = productRepository;
            _mediatorHandler = mediatorHandler;
        }

     

        public async Task<bool> DecreaseStock(Guid productId, int quantity)
        {
            if (!await DecreaseItemStock(productId, quantity)) return false; 
            return await _productRepository.UnitOfWork.Commit();

        }

        private async Task<bool> DecreaseItemStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetProductById(productId);

            if(product == null) return false;

            if (!product.HasStock(quantity)) 
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Stock", $"Stock unavailable for {product.Name}."));
            }

            product.DecreaseStock(quantity);

            if(product.StockQuantity < 3)
            {
                await _mediatorHandler.PublishEvent(new LowStockProductEvent(productId, quantity));
            }

            _productRepository.Update(product);
            return true;
        }


        public async Task<bool> DecreaseListProductItemStock(ListOrderProducts listOrder)
        {
            foreach (var item in listOrder.items)
            {
                if (!await DecreaseItemStock(item.Id, item.Quantity)) return false; 
            }

            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> IncreaseStock(Guid productId, int quantity)
        { 

            var increased = await IncreaseStock(productId, quantity);
            if (!increased) return false;

            return await _productRepository.UnitOfWork.Commit();
             
        }

        private async Task<bool> IncreaseItemStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetProductById(productId);
            if (product == null) return false;

            product.IncreaseStock(quantity);

            _productRepository.Update(product);

            return true; 
        }

        public async Task<bool> IncreaseListProductItemStock(ListOrderProducts listOrder)
        { 
            foreach(var item in listOrder.items)
            {
                await IncreaseItemStock(item.Id,item.Quantity);
            }

            return await _productRepository.UnitOfWork.Commit();

        }
        
        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}
