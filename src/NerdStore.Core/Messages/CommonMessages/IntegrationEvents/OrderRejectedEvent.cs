using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderRejectedEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public OrderRejectedEvent(Guid orderId, Guid customerId)
        {

           AggregateId = orderId;
           OrderId = orderId;
           CustomerId = customerId;

        }
    }
}
