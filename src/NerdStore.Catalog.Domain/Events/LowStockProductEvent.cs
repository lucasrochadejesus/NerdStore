using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdStore.Core.Messages.CommonMessages.DomainEvents;

namespace NerdStore.Catalog.Domain.Events
{
    public class LowStockProductEvent : DomainEvent
    {
        public int RemainingAmount { get; private set; }

        public LowStockProductEvent(Guid aggregateId, int remainingAmount) : base(aggregateId)
        {
            RemainingAmount = remainingAmount;
        }
    }
}
