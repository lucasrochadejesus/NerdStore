using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events;

public class OrderUpdatedEvent : Event
{
    public Guid CustomerId { get; private set; }

    public Guid OrderId { get; private set; }

    public decimal Total { get; private set; }

    public OrderUpdatedEvent(Guid customerId, Guid orderId, decimal total)
    {
        AggregateId = orderId;
        CustomerId = customerId;
        OrderId = orderId;
        Total = total;
    }
}