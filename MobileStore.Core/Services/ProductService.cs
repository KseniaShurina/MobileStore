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

    private static ProductTypeModel MapFromEntity(ProductType entity)
    {
        return new ProductTypeModel
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }

    public async Task<ProductModel?> GetProduct(Guid productId)
    {
        var entity = await _context.Products
            .AsNoTracking()
            .Where(i => i.Id == productId)
            .FirstOrDefaultAsync();

        return entity?.MapToModel();
    }

    public async Task<List<ProductModel>> GetProducts(Guid? productTypeId)
    {
        //
        productTypeId ??= await _context.ProductTypes.Select(i => i.Id).FirstOrDefaultAsync();

        var entities = await _context.Products
            .AsNoTracking()
            .Where(i => i.ProductTypeId == productTypeId)
            .ToListAsync();

        return entities
            .Select(i => i.MapToModel())
            .ToList();
    }

    public async Task<List<ProductTypeModel>> GetProductTypes()
    {
        var entities = await _context
            .ProductTypes
            .AsNoTracking()
            .ToListAsync();

        return entities
            .Select(MapFromEntity)
            .ToList();
    }

    public async Task<ProductModel> Create(
        Guid productTypeId,
        string name,
        string company,
        double price,
        List<ContentCreateModel> contents)
    {
        var productTypeExist = await _context.ProductTypes.AnyAsync(p => p.Id == productTypeId);
        if (!productTypeExist)
        {
            throw new ArgumentNullException($"Product type does not exist {nameof(productTypeId)}");
        }


        var product = new Product
        {
            Id = Guid.NewGuid(),
            ProductTypeId = productTypeId,
            Name = name,
            Company = company,
            Price = price,
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product.MapToModel();

    }

    public async Task UpdateCurrentProduct(ProductModel productModel)
    {
        var product = await _context.Products
            .Include(i => i.Contents)
            .FirstOrDefaultAsync(p => p.Id == productModel.Id) ??
                      throw new ArgumentNullException($"Product does not exist {nameof(productModel.Id)}");
       
        product.Id = productModel.Id;
        product.ProductTypeId = productModel.ProductTypeId;
        product.Name = productModel.Name;
        product.Company = productModel.Company;
        product.Price = productModel.Price;

        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId) ??
                      throw new ArgumentException($"Product not found {nameof(productId)}");

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}