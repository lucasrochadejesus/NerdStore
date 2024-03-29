﻿using NerdStore.Core.DomainObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderCreatedEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }

        public Guid CustomerId { get; private set; }

        public decimal Total { get; private set; }

        public ListOrderProducts OrderProducts { get; private set; }

        public string CardHolder { get; private set; }

        public string CardNumber { get; private set; }

        public string ExpirationDate { get; private set; }

        public string CvvCode { get; private set; }

        public OrderCreatedEvent(Guid orderId, Guid customerId, decimal total, ListOrderProducts items, string cardHolder, string cardNumber, string expirationDate, string cvvCode)
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
