using MobileStore.Core.Models;

namespace MobileStore.Core.Abstractions.Services;

public interface IProductService
{
    Task<ProductModel?> GetProduct(int productId);

    Task<List<ProductModel>> GetProducts(int? productTypeId);

    Task<List<ProductTypeModel>> GetProductTypes();
}