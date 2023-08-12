using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events
{
    public class OrderRemoveEvent : Event
    {
        public Guid CustomerId { get; private set; }

        public Guid OrderId { get; private set; }

        public Guid ProductId { get; private set; }

        public OrderRemoveEvent(Guid customerId, Guid orderId, Guid productId)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            ProductId = productId;  
        }
    }
}
