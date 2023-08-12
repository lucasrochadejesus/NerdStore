using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events
{
    public class CouponAppliedOrderEvent : Event
    {
        public Guid CustomerId { get; private set; }

        public Guid OrderId { get; private set; }

        public Guid CouponId { get; private set; }

        public CouponAppliedOrderEvent(Guid customerId, Guid orderId, Guid couponId)
        {
            AggregateId = OrderId;
            CustomerId = customerId;
            OrderId = orderId;
            CouponId = couponId;    
        }


    }
}
