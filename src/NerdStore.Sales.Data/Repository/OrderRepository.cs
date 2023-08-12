using NerdStore.Core.Data;
using NerdStore.Sales.Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace NerdStore.Sales.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {

        private readonly SalesContext _context;

        public OrderRepository(SalesContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
         
        public async Task<Order> GetById(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllByCustomerId(Guid customerId)
        {
           return await _context.Orders.AsNoTracking().Where(o => o.CustomerId == customerId).ToListAsync();
        }

        public async Task<Order> GetOrderQuoteByCustomerId(Guid customerId)
        {
            
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.CustomerId == customerId 
                                                                          && o.OrderStatus == OrderStatus.Quote);
            if (order == null) return null;

            await _context.Entry(order).Collection(i => i.OrderItems).LoadAsync();

            if (order.CouponId != null)
            {
                await _context.Entry(order).Reference(i => i.Coupon).LoadAsync();
            }

            return order;

        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }

        public async Task<OrderItem> GetItemById(Guid id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(i => i.ProductId == productId && i.OrderItemId == orderId);
        }

        public void AddItem(OrderItem orderItem)
        {
           _context.OrderItems.Add(orderItem);
        }

        public void UpdateItem(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
        }

        public void RemoveItem(OrderItem orderItem)
        {
           _context.OrderItems.Remove(orderItem);
        }

        public async Task<Coupon> GetCouponByCode(string code)
        {
            return await _context.Coupons.FirstOrDefaultAsync(o => o.Code == code);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
