using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Application.ViewModels;

namespace NerdStore.WebApp.MVC.Controllers.Admin
{
    public class AdminProductsController : Controller
    {
        private readonly IProductAppService _productAppService;

        public AdminProductsController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("admin-products")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.GetAll());
        }

        #region new-product
        [Route("new-product")]
        public async Task<IActionResult> NewProduct()
        {
            return View(await PopulateCategories(new ProductViewModel()));
        }

        [HttpPost] 
        [Route("new-product")]
        public async Task<IActionResult> NewProduct(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return View(await PopulateCategories(productViewModel));

            await _productAppService.AddProduct(productViewModel);

            return RedirectToAction("Index");
        }
        #endregion
        #region edit-product

        [HttpGet]
        [Route("edit-product")]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            return View(await PopulateCategories(await _productAppService.GetProductById(id)));
        }

        
        [HttpPost]
        [Route("edit-product")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductViewModel productViewModel)
        {
            var product = await _productAppService.GetProductById(id);
            productViewModel.StockQuantity = product.StockQuantity;

            ModelState.Remove("StockQuantity");
            if (!ModelState.IsValid) return View(await PopulateCategories(productViewModel));

            await _productAppService.UpdateProduct(productViewModel);
            return RedirectToAction("Index");

        }
        #endregion
        #region products stock

        [HttpGet]
        [Route("products-update-stock")]
        public async Task<IActionResult> UpdateStock(Guid id)
        {
            return View("Stock", await _productAppService.GetProductById(id));
        }
        

        [HttpPost]
        [Route("products-update-stock")]
        public async Task<IActionResult> UpdateStock(Guid id, int amount)
        {
            if (amount > 0)
            {
                await _productAppService.IncreaseStock(id, amount);
            }
            else
            {
                await _productAppService.DecreaseStock(id, amount);
            }

            return View("Index", await _productAppService.GetAll());

        }

        #endregion


        private async Task<ProductViewModel> PopulateCategories(ProductViewModel product)
        {
            product.Categories = await _productAppService.GetCategories();
            return product;
        }
    }
}
