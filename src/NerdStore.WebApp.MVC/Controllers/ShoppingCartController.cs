using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Core.Bus;
using NerdStore.Sales.Application.Commands;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class ShoppingCartController : ControllerBase
    {

        private readonly IProductAppService _productAppService;
        private readonly IMediatorHandler _mediatorHandler;
        public ShoppingCartController(IProductAppService productAppService, IMediatorHandler mediatorHandler)
        {
            _productAppService = productAppService;
            _mediatorHandler = mediatorHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("my-cart")]
        public async Task<IActionResult> AddItem(Guid id, int quantity)
        {
            var product = await _productAppService.GetProductById(id);
            if (product == null) return BadRequest();

            if (product.StockQuantity < quantity)
            {
                TempData["Error"] = "Stock not available";
                return RedirectToAction("ProductDetail", "Shop", routeValues: new { id }); 
            }

            var command = new AddOrderItemCommand(CustomerId, product.Id, product.Name, quantity, product.Price);

            await _mediatorHandler.SendCommand(command);
             

            TempData["Error"] = "Product not available";
            return RedirectToAction("ProductDetail", "Shop", routeValues: new { id });

        }
    }
}
