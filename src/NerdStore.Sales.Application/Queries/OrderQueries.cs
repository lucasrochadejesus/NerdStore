using NerdStore.Sales.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdStore.Sales.Domain.Order;

namespace NerdStore.Sales.Application.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueries(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrdersByCustomerId(Guid customerId)
        {
            var orders = await _orderRepository.GetAllByCustomerId(customerId);

            orders = orders.Where(o =>  o.OrderStatus == OrderStatus.Paid || o.OrderStatus == OrderStatus.Cancelled) 
                           .OrderBy(o => o.OrderId);

            if (!orders.Any()) return null;

            var ordersView = new List<OrderViewModel>();
          
            foreach (var order in orders)
            {
                ordersView.Add(new OrderViewModel
                {
                    Total = order.Total,
                    OrderStatus = (int)order.OrderStatus,
                    OrderId = order.OrderId,
                    DateCreate = order.DateCreated
                });
            }

            return ordersView;
        }

        public async Task<ShoppingCartViewModel> GetShoppingCartByCustomerId(Guid customerId)
        {
            var order = await _orderRepository.GetOrderQuoteByCustomerId(customerId);
            if (order == null) return null;

            var shoppingCart = new ShoppingCartViewModel
            {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                Total = order.Total,
                Discount = order.Discount,
                SubTotal = order.Discount + order.Total
            };

            if (order.CouponId != null)
            {
                shoppingCart.Coupon = order.Coupon.Code;
            }

            foreach (var item in order.OrderItems)
            {
                shoppingCart.Items.Add(new ShoppingCartItemViewModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Total = item.UnitPrice * item.Quantity
                });
            }

            return shoppingCart;


        }
    }
}
