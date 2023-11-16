using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Abstractions.Services;

namespace MobileStore.Presentation.Blazor.Controllers.Api
{
    [ApiController]
    [Route("[controller]")]
    public class ContentsController : ControllerBase
    {
        private readonly IContentService _contentService;

        public ContentsController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var contentInfo = await _contentService.GetContentInfo(id, cancellationToken);

            if (contentInfo == null)
            {
                return NotFound();
            }

            var stream = await _contentService.Get(id, cancellationToken);

            return File(stream, contentInfo.ContentType);
        }
    }
}
