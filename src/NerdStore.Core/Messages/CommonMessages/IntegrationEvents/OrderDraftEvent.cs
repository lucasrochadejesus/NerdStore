using NerdStore.Core.DomainObjects.DTO;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderDraftEvent : IntegrationEvent
    {

        public Guid OrderId { get; private set; }

        public Guid CustomerId { get; private set; }

        public decimal Total { get; private set; }

        public ListOrderProducts OrderProducts { get; private set; }

        public string CardHolder { get; private set; }

        public string CardNumber {get; private set; }

        public string ExpirationDate { get; private set; }

        public string CvvCode { get; private set; }

        public OrderDraftEvent(Guid orderId, Guid customerId, decimal total, ListOrderProducts items, string cardHolder, string cardNumber, string expirationDate, string cvvCode)
        {

            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
            Total = total;
            OrderProducts = items;
            CardHolder = cardHolder;
            CardNumber = cardNumber;
            ExpirationDate = expirationDate;
            CvvCode = cvvCode;

        }

    }
}
