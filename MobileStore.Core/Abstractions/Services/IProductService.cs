using MobileStore.Core.Models;

namespace MobileStore.Core.Abstractions.Services;

public interface IProductService
{
    Task<ProductModel?> GetProduct(Guid productId);

    Task<List<ProductModel>> GetProducts(Guid? productTypeId);

    Task<List<ProductTypeModel>> GetProductTypes();
}