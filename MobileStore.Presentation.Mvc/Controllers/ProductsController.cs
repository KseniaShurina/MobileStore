using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Models;
using MobileStore.Presentation.Mvc.Controllers.Base;
using MobileStore.Presentation.Mvc.Models;
using MobileStore.Presentation.Mvc.ViewModels;

namespace MobileStore.Presentation.Mvc.Controllers
{
    [Route("{controller}")]
    public class ProductsController : MvcControllerBaseSecure
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Guid? productTypeId)
        {
            var viewModel = new IndexProductsViewModel
            {
                ProductTypes = _mapper.Map<List<ProductTypeDto>>(await _productService.GetProductTypes()),
                Products = _mapper.Map<List<ProductDto>>(await _productService.GetProducts(productTypeId)),
            };
            return View(viewModel);
        }

        [HttpGet("{productId}/{action}")]
        public async Task<IActionResult> Edit([FromRoute] Guid productId)
        {
            var product = await _productService.GetProduct(productId);
            var editProductViewModel = new EditProductViewModel
            {
               Id = product!.Id,
               ProductTypeId = product.ProductTypeId,
               Name = product.Name,
               Company = product.Company,
               Price = product.Price,
               Img = product.Img,
            };
            return View(editProductViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCurrentProduct(ProductViewModel productViewModel)
        {
            var productModel = new ProductModel
            {
                Id = productViewModel.Id,
                ProductTypeId = productViewModel.ProductTypeId,
                Name = productViewModel.Name,
                Company = productViewModel.Company,
                Price = productViewModel.Price,
                Img = productViewModel.Img,
            };
            await _productService.UpdateCurrentProduct(productModel);
            return RedirectToAction("Index", "Products");
        }

        //[HttpPost]
        //public async Task<IActionResult> SubmitCreateOrUpdateProduct(EditProductViewModel model)
        //{
        //    //contents.First()
        //    return RedirectToAction("Edit", "Products", new { productId = model.Id });
        //}
    }
}
