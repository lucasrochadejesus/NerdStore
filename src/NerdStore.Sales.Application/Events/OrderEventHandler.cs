using MediatR;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace NerdStore.Sales.Application.Events
{
    public class OrderEventHandler : 
        INotificationHandler<OrderQuoteStartedEvent>,
        INotificationHandler<OrderItemAddedEvent>,
        INotificationHandler<OrderUpdatedEvent>,
        INotificationHandler<OrderRejectedEvent>
    {
        public Task Handle(OrderQuoteStartedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemAddedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderRejectedEvent notification, CancellationToken cancellationToken)
        {
            // cancel Order proccess and return error. 
            return Task.CompletedTask;
        }
    }
}
