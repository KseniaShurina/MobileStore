using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Presentation.Mvc.Controllers.Base;

namespace MobileStore.Presentation.Mvc.Controllers
{
    public class ContentController : ApiControllerBase
    {
        private readonly IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }


        [HttpGet("{contentId}")]
        public async Task<IActionResult> Get(Guid contentId, CancellationToken cancellationToken)
        {
            var contentInfo = await _contentService.GetContentInfo(contentId, cancellationToken);

            if (contentInfo == null)
            {
                return NotFound();
            }

            var content = await _contentService.Get(contentId, cancellationToken);
            return File(content, contentInfo.ContentType, contentInfo.Name);
        }

        public async Task Delete(Guid contentId)
        {
            await _contentService.Delete(contentId);
        }
    }
}
