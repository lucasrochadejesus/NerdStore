using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands;

public class UpdateItemOrderCommand : Command
{
    public Guid CustomerId { get; private set; }

    public Guid OrderId { get; private set; }

    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }

    public UpdateItemOrderCommand(Guid customerId, Guid orderId, Guid productId, int quantity)
    {
        CustomerId = customerId;
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateItemOrderValidation().Validate(this);
        return ValidationResult.IsValid;
    }

}

public class UpdateItemOrderValidation : AbstractValidator<UpdateItemOrderCommand>
{
    public UpdateItemOrderValidation()
    {
        RuleFor(c => c.CustomerId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid Customer ID");

        RuleFor(c => c.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid Product Id");

        RuleFor(c => c.Quantity)
            .GreaterThan(0)
            .WithMessage("Minimum 1 quantity");

        RuleFor(c => c.Quantity)
            .LessThan(15)
            .WithMessage("Max item quantity is 15");

    }
}
