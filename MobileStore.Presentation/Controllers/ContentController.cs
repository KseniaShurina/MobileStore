using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Models;
using MobileStore.Presentation.Controllers.Base;
using AutoMapper;

namespace MobileStore.Presentation.Controllers
{
    public class ContentController : ApiControllerBase
    {
        private readonly IContentService _contentService;
        private readonly IMapper _mapper;

        public ContentController(IContentService contentService, 
            IProductService productService, IMapper mapper)
        {
            _contentService = contentService;
            _mapper = mapper;
        }

        

        public async Task Create(string contentType, string name, byte[] data)
        {
            await _contentService.Create(contentType, name, data);
        }

        [HttpGet("{contentId}")]
        public async Task<ContentModel> Get(Guid contentId)
        {
            var content = await _contentService.Get(contentId);
            return content;
        }

        public async Task Delete(Guid contentId)
        {
            await _contentService.Delete(contentId);
        }
    }
}
