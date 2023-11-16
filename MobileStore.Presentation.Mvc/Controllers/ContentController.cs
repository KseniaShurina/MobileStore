using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Core.Abstractions.Services;
using MobileStore.Core.Models;
using MobileStore.Presentation.Mvc.Controllers.Base;

namespace MobileStore.Presentation.Mvc.Controllers
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
            await _contentService.SaveFileToDatabase(contentType, name, data);
        }

        [HttpGet("{contentId}")]
        public async Task<ContentInfoModel> Get(Guid contentId)
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
