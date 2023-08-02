using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Presentation.Controllers.Base;
using MobileStore.Presentation.ViewModels;

namespace MobileStore.Presentation.Controllers
{
    [Route("{controller}")]
    public class ProductsController : MvcControllerBaseSecure
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{productId}/{action}")]
        public async Task<IActionResult> Edit([FromRoute] Guid productId)
        {
            var product = await _productService.GetProduct(productId);
            var editProductViewModel = new EditProductViewModel
            {
               Id = product.Id,
               ProductTypeId = product.ProductTypeId,
               Name = product.Name,
               Company = product.Company,
               Price = product.Price,
               Img = product.Img,
            };
            return View(editProductViewModel);
        }
    }
}
