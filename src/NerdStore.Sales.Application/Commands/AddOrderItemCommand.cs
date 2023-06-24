using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands
{
    public class AddOrderItemCommand : Command
    {

        public Guid CustomerId { get; private set; }

        public Guid ProductId { get; private set; }

        public string ProductName { get; private set; }

        public int Quantity { get; private set; }

        public decimal UnitPrice { get; private set; }

        public AddOrderItemCommand(Guid customerId, Guid productId, string productName, int quantity, decimal unitPrice)
        {
            CustomerId = customerId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        } 
    }

    public class AddOrderItemValidation : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemValidation()
        {
            RuleFor(c=> c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customer Id Invalid!");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Product Id Invalid");

            RuleFor(c => c.ProductName)
                .NotEqual(String.Empty)
                .WithMessage("Product Name Invalid");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Minimum Quantity Must be > 0");

            RuleFor(c => c.Quantity)
                .LessThan(15)
                .WithMessage("Maximum Quantity 15");

            RuleFor(c => c.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit price must be > 0");

        }
    }
}
