using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Presentation.Controllers.Base;
using MobileStore.Presentation.ViewModels;

namespace MobileStore.Presentation.Controllers
{
    public class HomeController : MvcControllerBaseSecure
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index([FromQuery] int? productTypeId)
        {
            var productView = new ProductsViewModel();
            productView.ProductTypes = await _productService.GetProductTypesAsync();
            productView.Products = await _productService.GetProductsAsync(productTypeId);

            return View(productView);
        }

        public async Task<IActionResult> Buy(int id)
        {
            var product = await _productService.GetProduct(id);

            return product != null ? View(product) : NotFound();
        }
    }
}