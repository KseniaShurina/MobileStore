using AutoMapper;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Entities;
using MobileStore.Presentation.Api.Models.Product;

namespace MobileStore.Presentation.Api.Helpers
{
    public class PresentationMapperProfile : Profile
    {
        public PresentationMapperProfile()
        {
            CreateMap<ProductModel, ProductDto>();
            CreateMap<ProductContent, ProductContentDto>();
        }
    }
}
