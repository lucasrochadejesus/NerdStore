using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Application.ViewModels;

namespace NerdStore.WebApp.MVC.Controllers.Shop
{
    public class ShopController : Controller
    {

        private readonly IProductAppService _productAppService;

        public ShopController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("")]
        [Route("shop")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.GetAll());
        }

        [HttpGet]
        [Route("product-detail/{id}")]
        public async Task<IActionResult> ProductDetail(Guid id)
        {
            return View(await _productAppService.GetProductById(id));
        }


    }
}
