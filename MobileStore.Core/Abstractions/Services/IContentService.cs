using MobileStore.Core.Models;

namespace MobileStore.Core.Abstractions.Services
{
    public interface IContentService
    {
        Task<ContentInfoModel> SaveFileToDatabase(
            string contentType, string name, Stream stream, CancellationToken cancellationToken);

        Task<ContentInfoModel?> GetContentInfo(Guid contentId, CancellationToken cancellationToken);
        Task<Stream> Get(Guid contentId, CancellationToken cancellationToken);
        Task Delete(Guid contentId);
    }
}
