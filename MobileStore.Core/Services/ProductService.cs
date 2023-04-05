using Microsoft.EntityFrameworkCore;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Contexts;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Services
{
    internal class ProductService : IProductService
    {
        private readonly DefaultContext _context;

        public ProductService(DefaultContext context)
        {
            _context = context;
        }

        public async Task<ProductModel?> GetProduct(int productId)
        {
            var entity = await _context.Products
                .AsNoTracking()
                .Where(i => i.Id == productId)
                .FirstOrDefaultAsync();

            return entity != null ? MapFromEntity(entity) : null;
        }

        public async Task<List<ProductModel>> GetProductsAsync(int? productTypeId)
        {
            if (!productTypeId.HasValue)
            {
                productTypeId = await _context.ProductTypes.Select(i => i.Id).FirstOrDefaultAsync();
            }

            // достаём из БД продукты по типу
            var entities = await _context.Products
                .AsNoTracking()
                .Where(i => i.ProductTypeId == productTypeId)
                .ToListAsync();

            return entities
                .Select(MapFromEntity)
                .ToList();
        }

        public async Task<List<ProductTypeModel>> GetProductTypesAsync()
        {
            var entities = await _context
                .ProductTypes
                .AsNoTracking()
                .ToListAsync();

            return entities
                .Select(MapFromEntity)
                .ToList();
        }

        private static ProductTypeModel MapFromEntity(ProductType entity)
        {
            return new ProductTypeModel
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        private static ProductModel MapFromEntity(Product entity)
        {
            return new ProductModel
            {
                Id = entity.Id,
                ProductTypeId = entity.ProductTypeId,
                Name = entity.Name,
                Company = entity.Company,
                Img = entity.Img,
                Price = entity.Price,
            };
        }
    }
}
