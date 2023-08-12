using Microsoft.AspNetCore.Mvc;
using NerdStore.Sales.Application.Queries;

namespace NerdStore.WebApp.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {

        private readonly IOrderQueries _orderQueries;

        // TODO Get Customer
        protected Guid CustomerId = Guid.Parse("BBD6AE49-712B-4F6A-BB55-0414089D7DFD");

        public CartViewComponent(IOrderQueries orderQueries)
        {
            _orderQueries = orderQueries;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var shoppingCart = await _orderQueries.GetShoppingCartByCustomerId(CustomerId);
            var items = shoppingCart?.Items.Count ?? 0;

            return View(items);
        }
    }
}
