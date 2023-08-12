using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands;

public class RemoveItemOrderCommand : Command
{
    public Guid CustomerId { get; private set; } 

    public Guid ProductId { get; private set; }


    public RemoveItemOrderCommand(Guid customerId, Guid productId)
    {
        CustomerId = customerId;
        ProductId = productId;  
    }

    public override bool IsValid()
    {
        ValidationResult = new RemoveItemOrderValidation().Validate(this);
        return ValidationResult.IsValid;
    }

}

public class RemoveItemOrderValidation : AbstractValidator<RemoveItemOrderCommand>
{
    public RemoveItemOrderValidation()
    {
        RuleFor(c => c.CustomerId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid Customer ID");
         

        RuleFor(c => c.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid Product Id");

    }
}