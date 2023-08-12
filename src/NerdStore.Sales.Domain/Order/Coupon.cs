using FluentValidation;
using FluentValidation.Results;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Sales.Domain.Order
{
    public class Coupon : Entity
    {
        public string Code { get; private set; } 
        public decimal? Percentage { get; private set; } 
        public decimal? Discount { get; private set; }
        public int Quantity { get; private set; }
        public CouponType CouponType { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime? UsageDate { get; private set; }
        public DateTime ExpirationDate { get; private set; } 
        public bool Active { get; private set; }
        public bool Usage { get; private set; }

        //EF Relation
        public ICollection<Order> Orders { get; private set; }


        internal ValidationResult ValidationIfApplicable()
        {
            return new CouponApplicableValidation().Validate(this);
        }
    }

    public class CouponApplicableValidation : AbstractValidator<Coupon>
    {
        public CouponApplicableValidation()
        {
            RuleFor(c => c.ExpirationDate)
                .Must(IsBetweenAcceptedDates)
                .WithMessage("Coupon Expired!");

            RuleFor(c => c.Active)
                .Equal(true)
                .WithMessage("Coupon not valid!");

            RuleFor(c => c.Usage)
                .Equal(false)
                .WithMessage("Coupon already used!");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Coupon not available!");
        }

        protected static bool IsBetweenAcceptedDates(DateTime validDate)
        {
            return validDate >= DateTime.Now;
        }
    }
}
