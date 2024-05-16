using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Models;
using MobileStore.Presentation.Api.Models.Product;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MobileStore.Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody][Required] ProductCreateDto request)
        {
            try
            {
                var response = await _productService.Create(
                    request.ProductTypeId,
                    request.Name,
                    request.Company,
                    request.Price,
                    new List<ContentCreateModel>());

                return CreatedAtRoute(
                    nameof(GetProduct), 
                    new { productId = response.Id }, 
                    _mapper.Map<ProductDto>(response));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Returns product by productId from database.
        /// </summary>
        /// <param name="productId">productId</param>
        /// <returns>ProductDto</returns>
        [HttpGet("{productId:guid}", Name = nameof(GetProduct))]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(Guid productId)
        {
            var response = await _productService.GetProduct(productId);
            if (response is null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<ProductDto>(response);

            return Ok(dto);
        }

        /// <summary>
        /// Returns list of products by productTypeId from database.
        /// </summary>
        /// <param name="productTypeId">productTypeId</param>
        /// <returns>IReadOnlyCollection&lt;ProductDto&gt;</returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<ProductDto>))]
        public async Task<IActionResult> GetProducts([FromQuery] Guid? productTypeId)
        {
            var response = await _productService.GetProducts(productTypeId);

            var dto = _mapper.Map<IReadOnlyCollection<ProductDto>>(response);

            return Ok(dto);
        }

        /// <summary>
        /// Update a product by productId
        /// </summary>
        /// <param name="productId">productId</param>
        /// <param name="request"></param>
        /// <returns>ProductDto</returns>
        [HttpPut("{productId:guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] ProductUpdateDto request)
        {
            var current = await _productService.GetProduct(productId);
            if (current is null)
            {
                return NotFound();
            }

            var updateModel = new ProductModel
            {
                Id = current.Id,
                ProductTypeId = current.ProductTypeId,
                Name = request.Name,
                Company = request.Company,
                Price = request.Price,
                Contents = current.Contents,
            };

            await _productService.Update(updateModel);

            var dto  = _mapper.Map<ProductDto>(updateModel);

            return Ok(dto);
        }


    }
}
