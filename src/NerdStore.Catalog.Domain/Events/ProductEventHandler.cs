using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace NerdStore.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<LowStockProductEvent>
    {
        private readonly IProductRepository _productRepository;

        public ProductEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(LowStockProductEvent notification, CancellationToken cancellationToken)
        {
            // TODO: create Order to buy more products and sent email to buyer.
            
            var product = await _productRepository.GetProductById(notification.AggregateId);
            
        }
    }
}
