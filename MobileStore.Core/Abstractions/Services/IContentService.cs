using MobileStore.Core.Models;

namespace MobileStore.Core.Abstractions.Services
{
    public interface IContentService
    {
        Task Create(string contentType, string name, byte[] data);
        Task<ContentModel> Get(Guid contentId);
        Task Delete(Guid contentId);



        //Task<ProductModel> Create(Guid productTypeId, string productTypeName, string name, string company, double price, string img);
        //Task<ProductModel> Update(ProductModel productModel);
        //Task Delete(Guid productId);
    }
}
