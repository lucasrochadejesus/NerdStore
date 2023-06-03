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
    }
}
