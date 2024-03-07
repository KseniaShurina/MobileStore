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
    private readonly IContentService _contentService;

    public ProductService(IDefaultContext context, IContentService contentService)
    {
        _context = context;
        _contentService = contentService;
    }

    private IQueryable<Product> GetBaseQuery()
    {
        return _context.Products
            .AsNoTracking()
            .Include(i => i.Contents);
    }

    /// <summary>
    /// Gets current product by ID
    /// </summary>
    /// <param name="productId">ProductId</param>
    /// <returns>ProductModel</returns>
    public async Task<ProductModel?> GetProduct(Guid productId)
    {
        var entity = await GetBaseQuery()
            .Include(i => i.Contents)
            .Where(i => i.Id == productId)
            .FirstOrDefaultAsync();

        return entity?.MapToModel();
    }

    /// <summary>
    /// Gets all products by productTypeId
    /// </summary>
    /// <param name="productTypeId">productTypeId</param>
    /// <returns>ProductModels</returns>
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
    /// <returns>ProductTypeModels</returns>
    public async Task<List<ProductTypeModel>> GetProductTypes()
    {
        var entities = await _context
            .ProductTypes
            .AsNoTracking()
            .ToListAsync();

        return entities
            .Select(i => i.MapToModel())
            .ToList();
    }

    /// <summary>
    /// Create new product and add to DB
    /// </summary>
    /// <param name="productTypeId">productTypeId</param>
    /// <param name="name">product name</param>
    /// <param name="company">product company</param>
    /// <param name="price">product price</param>
    /// <param name="contents">product content</param>
    /// <returns>ProductModel</returns>
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

    public async Task Update(ProductModel productModel)
    {
        await using var transaction = await _context.BeginTransactionAsync();
        try
        {
            var product = await _context.Products
                              .Include(i => i.Contents)
                              .FirstOrDefaultAsync(p => p.Id == productModel.Id) ??
                          throw new ArgumentNullException($"Product does not exist {nameof(productModel.Id)}");

            var contentsForDelete = product.Contents
                .Where(i => !productModel.Contents.Select(pc => pc.Id).Contains(i.Id))
            .ToList();

            // TODO move to method ToEntity
            var contentsForAdd = productModel.Contents
                .Select(i => new ProductContent
                {
                    Id = i.Id,
                    ContentId = i.ContentId,
                    ContentType = i.ContentType,
                    Name = i.Name,
                    ProductId = i.ProductId,
                })
                .ToList();

            _context.ProductContents.RemoveRange(contentsForDelete);

            await _contentService.Delete(contentsForDelete.Select(i => i.Id));

            
            product.ProductTypeId = productModel.ProductTypeId;
            product.Name = productModel.Name;
            product.Company = productModel.Company;
            product.Price = productModel.Price;
            product.Contents = null!;

            _context.ProductContents.AddRange(contentsForAdd);

            await _context.SaveChangesAsync();


            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Delete product
    /// </summary>
    /// <param name="productId">ProductId</param>
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