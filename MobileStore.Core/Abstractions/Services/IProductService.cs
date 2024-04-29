using MobileStore.Core.Models;

namespace MobileStore.Core.Abstractions.Services;

public interface IProductService
{
    Task<ProductModel?> GetProduct(Guid productId);

    Task<List<ProductModel>> GetProducts(Guid? productTypeId);

    Task<List<ProductTypeModel>> GetProductTypes();

    /// <summary>
    /// Create
    /// </summary>
    /// <param name="productTypeId"></param>
    /// <param name="name"></param>
    /// <param name="company"></param>
    /// <param name="price"></param>
    /// <param name="contents"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    Task<ProductModel> Create(Guid productTypeId, string name, string company, double price, 
        List<ContentCreateModel> contents);

    Task Update(ProductModel productModel);
}