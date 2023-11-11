using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Application.Queries;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class ShoppingCartController : ControllerBase
    { 
        private readonly IProductAppService _productAppService;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IOrderQueries _orderQueries;

        public ShoppingCartController(INotificationHandler<DomainNotification> notifications,
                                      IProductAppService productAppService, 
                                      IMediatorHandler mediatorHandler, IOrderQueries orderQueries) : base(notifications, mediatorHandler)
        {
            _productAppService = productAppService;
            _mediatorHandler = mediatorHandler;
            _orderQueries = orderQueries;
        }

        [Route("my-cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _orderQueries.GetShoppingCartByCustomerId(CustomerId));
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

            if (ValidOperation()) return RedirectToAction("Index", "Shop");
            
            TempData["Error"] =  GetErrorMessage();
            return RedirectToAction("ProductDetail", "Shop", routeValues: new { id });

        }


        [HttpPost]
        [Route("remove-item")]
        public async Task<IActionResult> RemoveItem(Guid id)
        {
            var product = await _productAppService.GetProductById(id);
            if (product == null) return BadRequest();

            var command = new RemoveItemOrderCommand(CustomerId,product.Id);
            await _mediatorHandler.SendCommand(command);

            if (ValidOperation()) return RedirectToAction("Index");

            return View("Index", await _orderQueries.GetShoppingCartByCustomerId(CustomerId));

        }

        [HttpPost]
        [Route("update-item")]
        public async Task<IActionResult> UpdateItem(Guid id, int quantity)
        {
            var product = await _productAppService.GetProductById(id);
            if (product == null) return BadRequest();

            var command = new UpdateItemOrderCommand(CustomerId, id, product.Id, quantity);
            await _mediatorHandler.SendCommand(command);

            if (ValidOperation()) return RedirectToAction("Index");

            return View("Index", await _orderQueries.GetShoppingCartByCustomerId(CustomerId));

        }

        [HttpPost]
        [Route("apply-coupon")]
        public async Task<IActionResult> ApplyCoupon(string coupon,Guid id)
        {
            var command = new ApplyCouponOrderCommand(CustomerId, id, coupon);
            await _mediatorHandler.SendCommand(command);

            if (ValidOperation()) return RedirectToAction("Index");

            return View("Index", await _orderQueries.GetShoppingCartByCustomerId(CustomerId));


        }

        [Route("PurchaseSummary")]
        public async Task<IActionResult> PurchaseSummary()
        {
            return View(await _orderQueries.GetShoppingCartByCustomerId(CustomerId));
        }
        
    }
}
