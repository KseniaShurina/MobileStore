using MobileStore.Core.Models;

namespace MobileStore.Core.Abstractions.Services;

public interface IProductService
{
    Task<ProductModel?> GetProduct(int productId);

    Task<List<ProductModel>> GetProductsAsync(int? productTypeId);

    Task<List<ProductTypeModel>> GetProductTypesAsync();
}