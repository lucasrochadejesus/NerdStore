using FluentValidation;
using NerdStore.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Sales.Application.Commands
{
    public class StartOrderCommand : Command
    {
       
        public Guid OrderId { get; private set; }

        public Guid CustomerId { get; private set; }

        public decimal Total { get; private set; }

        public string CardName { get; private set; }

        public string CardNumber { get; private set; }

        public string ExpirationDate { get; private set; }

        public string CvvCode { get; private set; }

        public string ZipCode { get; private set; }


        public StartOrderCommand(Guid orderId, Guid customerId, decimal total, string cardName, string cardNumber, string expirationDate, string cvvCode, string zipCode)
        {
            OrderId = orderId;
            CustomerId = customerId;
            Total = total;
            CardName = cardName;
            CardNumber = cardNumber;
            ExpirationDate = expirationDate;
            CvvCode = cvvCode;
            ZipCode = zipCode;
        }

        public override bool IsValid()
        {
            ValidationResult = new StartOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }

    public class StartOrderValidation : AbstractValidator<StartOrderCommand>
    {
        public StartOrderValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customer ID invalid!");

            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Order ID invalid!"); ;

            RuleFor(c => c.CardName)
                .NotEmpty()
                .WithMessage("Card Holder Name invalid!");

            RuleFor(c => c.CardNumber)
                .NotEmpty()
                .CreditCard()
                .WithMessage("Credit card number invalid!");

            RuleFor(c => c.ExpirationDate)
                .NotEmpty()
                .WithMessage("Expiration Date invalid!");

            RuleFor(c => c.CvvCode)
                .Length(3, 4)
                .WithMessage("CVV Code invalid!");

            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Zip Code invalid!");


        }
    }
}
