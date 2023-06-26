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
            productView.ProductTypes = await _productService.GetProductTypes();
            productView.Products = await _productService.GetProducts(productTypeId);

            return View(productView);
        }
    }
}