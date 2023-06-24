using NerdStore.Core.Data;

namespace NerdStore.Sales.Domain.Order
{
    public interface IOrderRepository : IRepository<Order>
    {
        // Order
        Task<Order> GetById(Guid id);

        Task<IEnumerable<Order>> GetAllByCustomerId(Guid customerId);

        Task<Order> GetOrderQuoteByCustomerId(Guid customerId);

        void Add(Order order);

        void Update(Order order);

        
        // Order Item
        Task<OrderItem> GetItemById(Guid id);

        Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId);

        void AddItem(OrderItem orderItem);

        void UpdateItem(OrderItem orderItem);

        void RemoveItem(OrderItem orderItem);

        // Coupon 
        Task<Coupon> GetCouponByCode(string code);

    }
}
