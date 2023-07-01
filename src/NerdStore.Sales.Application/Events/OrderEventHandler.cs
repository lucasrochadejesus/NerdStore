using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace NerdStore.Sales.Application.Events
{
    public class OrderEventHandler : 
        INotificationHandler<OrderQuoteStartedEvent>,
        INotificationHandler<OrderItemAddedEvent>,
        INotificationHandler<OrderUpdatedEvent>
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
    }
}
