using System.Text;
using FluentValidation.Results;
using NerdStore.Core;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Sales.Domain.Order
{
    public class Order : Entity, IAggregateRoot
    {

        public int OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid? CouponId { get; private set; }
        public bool CouponUsed { get; private set; }
        public decimal Discount { get; private set; }
        public decimal Total { get; private set; }
        public DateTime DateCreated { get; private set; }

        public OrderStatus OrderStatus { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        // EF Relation
        public Coupon Coupon { get; set; }

        public Order(Guid customerId, bool couponUsed, decimal discount, decimal total)
        {
            CustomerId = customerId;
            CouponUsed = couponUsed;
            Discount = discount;
            Total = total;
            _orderItems = new List<OrderItem>();
        }

        // EF prevent null reference exception
        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public ValidationResult ApplyCoupon(Coupon coupon)
        {

            var validationResult = coupon.ValidationIfApplicable();
           
            if(!validationResult.IsValid) return validationResult;

            Coupon = coupon;
            CouponUsed = true;
            
            CalculateOrder();

            return validationResult;

        }

        public bool OrderItemExists(OrderItem item)
        {
            return _orderItems.Any(p => p.ProductId == item.ProductId);
        }
         

        public void AddItem(OrderItem item)
        {
            if (!item.IsValid()) return;
            
            item.AssociateOrder(Id);

            if (OrderItemExists(item))
            {
                var itemExist = _orderItems.FirstOrDefault(p => p.ProductId == item.ProductId);
                itemExist.AddUnity(item.Quantity);
                item = itemExist;
                _orderItems.Remove(itemExist);
            }

            item.Calculate();
            _orderItems.Add(item);
            
            CalculateOrder();

        }

        public void RemoveItem(OrderItem item)
        {
            if(!item.IsValid()) return;

            var itemExist = OrderItems.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (itemExist == null) throw new DomainException("Item doesn't exists on Order");

            _orderItems.Remove(itemExist);

            CalculateOrder();

        }

        public void UpdateItem(OrderItem item)
        {
            if(!item.IsValid()) return;

            var itemExist = _orderItems.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (itemExist == null) throw new DomainException("Item doesn't exists on Order");

            _orderItems.Remove(itemExist);
            _orderItems.Add(item);

            CalculateOrder();

        }

        public void MakeDraft()
        {
            OrderStatus = OrderStatus.Quote;
        }
        public void MakeQuote()
        {
            OrderStatus = OrderStatus.Started;
        }

        public void MakePaid()
        {
            OrderStatus = OrderStatus.Paid;
        }

        public void MakeProcessing()
        {
            OrderStatus = OrderStatus.Processing;
        }

        public void MakeShipped()
        {
            OrderStatus = OrderStatus.Shipped;
        }

        public void MakeCancelled()
        {
            OrderStatus = OrderStatus.Cancelled;
        }

        public void UpdateUnity(OrderItem item, int unity)
        {
            item.UpdateUnity(unity);
            UpdateItem(item);
        }

        public void CalculateOrder()
        {
            Total = OrderItems.Sum(p => p.Calculate());
            CalculateTotalDiscount();
        }

        public void CalculateTotalDiscount()
        {
            if (!CouponUsed) return;

            decimal discount = 0;
            var totalVal = Total;

            if (Coupon.CouponType == CouponType.Percentage)
            {
                if (Coupon.Percentage.HasValue)
                {
                    discount = (totalVal * Coupon.Percentage.Value) / 100;
                    totalVal -= discount; 
                }
            }
            else
            {
                if (Coupon.Discount.HasValue)
                {
                    discount = Coupon.Discount.Value;
                    totalVal -= discount;
                }
            }

            Total = totalVal < 0 ? 0 : totalVal;
            Discount = discount;
        }
        
        public override bool IsValid()
        {
            return true;
        }

        // Factory to manipulate Order (children class)
        public static class OrderFactory
        {
            public static Order NewOrderQuote(Guid customerId)
            {
                var order = new Order
                {
                    CustomerId = customerId,
                };

                order.MakeQuote();
                return order;
            }

        }
    }
}
