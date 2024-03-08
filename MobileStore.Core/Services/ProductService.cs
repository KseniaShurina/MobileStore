using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Extensions.Entities;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Abstractions.Contexts;
using MobileStore.Infrastructure.Entities;
using System.Linq;

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
    /// Returns current product by ID.
    /// </summary>
    /// <param name="productId">ProductId</param>
    /// <returns>ProductModel, or null if the product does not exist.</returns>
    public async Task<ProductModel?> GetProduct(Guid productId)
    {
        var entity = await GetBaseQuery()
            .Include(i => i.Contents)
            .Where(i => i.Id == productId)
            .FirstOrDefaultAsync();

        return entity?.MapToModel();
    }

    /// <summary>
    /// Returns all products by productTypeId from database.
    /// </summary>
    /// <param name="productTypeId">productTypeId</param>
    /// <returns>A list of ProductModels.</returns>
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
    /// Retrieves all product types from the database.
    /// </summary>
    /// <returns>A list of ProductTypeModels.</returns>
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
    /// Create a new product and add to DB
    /// </summary>
    /// <param name="productTypeId">The ID of the product type.</param>
    /// <param name="name">The name of the product.</param>
    /// <param name="company">The company of the product.</param>
    /// <param name="price">The price of the product.</param>
    /// <param name="contents">The list of content for the product.</param>
    /// <returns>The newly created ProductModel.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the product type does not exist.</exception>
    public async Task<ProductModel> Create(
        Guid productTypeId,
        string name,
        string company,
        double price,
        List<ContentCreateModel> contents)
    {
        Guard.Against.NullOrEmpty(name);
        Guard.Against.NullOrEmpty(company);
        Guard.Against.NegativeOrZero(price);
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

    /// <summary>
    /// Updates a product in the database with the provided productModel.
    /// </summary>
    /// <param name="productModel">The productModel containing the updated information.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Update(ProductModel productModel)
    {
        Guard.Against.Null(productModel);
        // Begins a transaction
        await using var transaction = await _context.BeginTransactionAsync();
        try
        {
            var product = await _context.Products
                              .Include(i => i.Contents)
                              .FirstOrDefaultAsync(p => p.Id == productModel.Id) ??
                          throw new ArgumentNullException($"Product does not exist {nameof(productModel.Id)}");

            // Identifies the contents that need to be deleted
            // by comparing the contents of the existing product with the contents provided in the productModel.
            var contentsForDelete = product.Contents
                .Where(i => !productModel.Contents.Select(pc => pc.Id).Contains(i.Id))
            .ToList();

            // TODO move to method ToEntity
            // Creates a list of new ProductContent objects based on the productModel.Contents.
            var contentsForAdd = productModel.Contents
                .Where(i => !product.Contents.Select(c => c.Id).Contains(i.Id))
                .Select(i => new ProductContent
                {
                    Id = i.Id,
                    ContentId = i.ContentId,
                    ContentType = i.ContentType,
                    Name = i.Name,
                    ProductId = i.ProductId,
                })
                .ToList();


            if (contentsForDelete.Any())
            {
                _context.ProductContents.RemoveRange(contentsForDelete);
                await _contentService.Delete(contentsForDelete.Select(i => i.ContentId));
            }

            // Updates the properties of the product object with the values from productModel.
            product.ProductTypeId = productModel.ProductTypeId;
            product.Name = productModel.Name;
            product.Company = productModel.Company;
            product.Price = productModel.Price;
            product.Contents = null!;

            if (contentsForAdd.Any())
            {
                _context.ProductContents.AddRange(contentsForAdd);
            }

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
    /// Deletes a product from the database based on the productId.
    /// </summary>
    /// <param name="productId">The ID of the product to delete.</param>
    /// <returns> task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentException">Thrown if the product with the specified ID is not found.</exception>
    public async Task Delete(Guid productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId) ??
                      throw new ArgumentException($"Product not found {nameof(productId)}");

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}