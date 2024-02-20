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

    /// <summary>
    /// Gets current product by Id
    /// </summary>
    /// <param name="productId">Product Id</param>
    /// <returns></returns>
    public async Task<ProductModel?> GetProduct(Guid productId)
    {
        var entity = await _context.Products
            .AsNoTracking()
            .Include(i => i.Contents)
            .Where(i => i.Id == productId)
            .FirstOrDefaultAsync();

        return entity?.MapToModel();
    }

    /// <summary>
    /// Gets all products by product type Id
    /// </summary>
    /// <param name="productTypeId">product type Id</param>
    /// <returns></returns>
    public async Task<List<ProductModel>> GetProducts(Guid? productTypeId)
    {
        var query = _context.Products
            .AsNoTracking()
            .Include(i => i.Contents)
            .AsQueryable();

        if (productTypeId != null)
        {
            query = query
                    .Where(i => i.ProductTypeId == productTypeId.Value);
        }

        var entities = await query
            .ToListAsync();

        return entities
            .Select(i => i.MapToModel())
            .ToList();
    }

    /// <summary>
    /// Gets all product types
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// SaveFileToDatabase new product and added to DB
    /// </summary>
    /// <param name="productTypeId">product type Id</param>
    /// <param name="name">product name</param>
    /// <param name="company">product company</param>
    /// <param name="price">product price</param>
    /// <param name="contents">product content</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"> Throw Exception if product exist</exception>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task Delete(Guid productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId) ??
                      throw new ArgumentException($"Product not found {nameof(productId)}");

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}