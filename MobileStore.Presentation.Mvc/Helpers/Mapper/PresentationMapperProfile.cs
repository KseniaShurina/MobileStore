using AutoMapper;
using MobileStore.Core.Models;
using MobileStore.Presentation.Mvc.Models;

namespace MobileStore.Presentation.Mvc.Helpers.Mapper
{
    public class PresentationMapperProfile : Profile
    {
        public PresentationMapperProfile()
        {
            CreateMap<CartItemModel, CartItemDto>();
            CreateMap<ProductModel, ProductDto>();
            CreateMap<ProductTypeModel, ProductTypeDto>();
            CreateMap<UserModel, UserDto>();
        }
    }
}
