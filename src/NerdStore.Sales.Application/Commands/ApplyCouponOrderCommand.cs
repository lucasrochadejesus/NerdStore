using FluentValidation;
using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands;

public class ApplyCouponOrderCommand : Command
{
    public Guid CustomerId { get; private set; }

    public Guid OrderId { get; private set; }
     
    public string CouponCode { get; private set; }

    public ApplyCouponOrderCommand(Guid customerId, Guid orderId, string couponCode)
    {
        CustomerId = customerId;
        OrderId = orderId;
        CouponCode = couponCode;
    }

    public override bool IsValid()
    {
        ValidationResult = new ApplyCouponOrderValidation().Validate(this);
        return ValidationResult.IsValid;
    }

}

public class ApplyCouponOrderValidation : AbstractValidator<ApplyCouponOrderCommand>
{
    public ApplyCouponOrderValidation()
    {

        RuleFor(c => c.CustomerId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid Customer ID");

        RuleFor(c => c.OrderId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid Order ID");

        RuleFor(c => c.CouponCode)
            .NotEmpty()
            .WithMessage("Coupon code can't be empty");

    }
}
