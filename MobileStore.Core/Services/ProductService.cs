using Microsoft.EntityFrameworkCore;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Extensions.Entities;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Abstractions.Contexts;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Services;

internal class ProductService : IProductService
{
    private readonly IDefaultContext _context;

    public ProductService(IDefaultContext context)
    {
        _context = context;
    }

    public async Task<ProductModel?> GetProduct(int productId)
    {
        var entity = await _context.Products
            .AsNoTracking()
            .Where(i => i.Id == productId)
            .FirstOrDefaultAsync();

        return entity?.MapToModel();
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
            .Select(i => i.MapToModel())
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

    
}