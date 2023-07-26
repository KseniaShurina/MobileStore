using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Presentation.Controllers.Base;
using MobileStore.Presentation.Models;
using MobileStore.Presentation.ViewModels;

namespace MobileStore.Presentation.Controllers
{
    public class HomeController : MvcControllerBaseSecure
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public HomeController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index([FromQuery] Guid? productTypeId)
        {
            var productView = new ProductsViewModel
            {
                ProductTypes = _mapper.Map<List<ProductTypeDto>>(await _productService.GetProductTypes()),
                Products = _mapper.Map<List<ProductDto>>(await _productService.GetProducts(productTypeId)),
            };

            return View(productView);
        }
    }
}