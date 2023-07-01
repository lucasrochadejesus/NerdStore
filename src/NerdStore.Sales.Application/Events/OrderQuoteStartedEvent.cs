﻿using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events
{
    public class OrderQuoteStartedEvent : Event
    {
        public Guid CustomerId { get; private set; }

        public Guid OrderId { get; private set; }


        public OrderQuoteStartedEvent(Guid customerId, Guid orderId)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}
