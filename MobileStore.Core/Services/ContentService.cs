using Microsoft.EntityFrameworkCore;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Extensions.Entities;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Abstractions.Contexts;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Services
{
    internal class ContentService : IContentService
    {
        private readonly IDefaultContext _context;
        public ContentService(IDefaultContext context)
        {
            _context = context;
        }

        public async Task Create(string contentType, string name, byte[] data)
        {
            var content = new Content
            {
                Id = Guid.NewGuid(),
                ContentType = contentType,
                Name = name,
                Data = data
            };
            await _context.Contents.AddAsync(content);
            await _context.SaveChangesAsync();
        }

        public async Task<ContentModel> Get(Guid contentId)
        {
            var content = await _context.Contents.FirstOrDefaultAsync(i => i.Id == contentId) ?? 
                          throw new ArgumentNullException($"Content does not exist {nameof(contentId)}");

            return content.MapToModel();
        }

        public async Task Delete(Guid contentId)
        {
            var content = await _context.Contents.FirstOrDefaultAsync(i => i.Id == contentId) ?? 
                          throw new ArgumentNullException($"Content does not exist {nameof(contentId)}");

            _context.Contents.Remove(content);
            await _context.SaveChangesAsync();
        }

        #region MyRegion

        //public async Task<ProductModel> Create(Guid productTypeId, string productTypeName, string name, string company, double price, string img)
        //{
        //    var productTypeExist = await _context.Products.AnyAsync(p => p.ProductTypeId == productTypeId);
        //    Product? product = null;
        //    if (productTypeExist)
        //    {
        //        product = new Product
        //        {
        //            Id = Guid.NewGuid(),
        //            ProductTypeId = productTypeId,
        //            Name = name,
        //            Company = company,
        //            Price = price,
        //            Img = img
        //        };
        //    }
        //    else
        //    {
        //        var productType = new ProductType
        //        {
        //            Id = Guid.NewGuid(),
        //            Name = productTypeName,
        //        };

        //        product = new Product
        //        {
        //            Id = Guid.NewGuid(),
        //            ProductTypeId = productType.Id,
        //            Name = name,
        //            Company = company,
        //            Price = price,
        //            Img = img
        //        };
        //    }

        //    await _context.Products.AddAsync(product);
        //    await _context.SaveChangesAsync();
        //    return product.MapToModel();
        //    //throw new NotImplementedException();
        //}

        //public async Task<ProductModel> Update(ProductModel productModel)
        //{
        //    var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productModel.Id);
        //    if (product == null)
        //    {
        //        throw new ArgumentNullException($"Product does not exist {nameof(product.Id)}");
        //    }

        //    var updatedProduct = product.MapToModel();

        //    productModel = updatedProduct;

        //    await _context.SaveChangesAsync();

        //    return updatedProduct;
        //}

        //public async Task Delete(Guid productId)
        //{
        //    var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        //    if (product == null)
        //    {
        //        throw new ArgumentException($"Product not found {nameof(product)}");
        //    }
        //    _context.Products.Remove(product);
        //    await _context.SaveChangesAsync();
        //}

        #endregion
    }
}
