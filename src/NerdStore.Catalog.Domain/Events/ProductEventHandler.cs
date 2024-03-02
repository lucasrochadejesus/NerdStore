using MediatR;
using NerdStore.Catalog.Domain.DomainService;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace NerdStore.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<LowStockProductEvent>,
                                       INotificationHandler<OrderDraftEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMediatorHandler _mediatorHandler;

        public ProductEventHandler(IProductRepository productRepository, IStockService stockService, IMediatorHandler mediatorHandler)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(LowStockProductEvent notification, CancellationToken cancellationToken)
        {
            // TODO: create Order to buy more products and sent email to customer.
            
            var product = await _productRepository.GetProductById(notification.AggregateId);
            
        }

        public async Task Handle(OrderDraftEvent message, CancellationToken cancellationToken)
        {
            var result = await _stockService.DecreaseListProductItemStock(message.OrderProducts);

            if (result)
            {
                await _mediatorHandler.PublishEvent(new OrderCreatedEvent(message.OrderId, message.CustomerId, message.Total, message.OrderProducts, message.CardHolder,message.CardNumber,message.ExpirationDate,message.CvvCode));
            }
            else 
            {
                await _mediatorHandler.PublishEvent(new OrderRejectedEvent(message.OrderId, message.CustomerId));
            }
        }
    }
}
