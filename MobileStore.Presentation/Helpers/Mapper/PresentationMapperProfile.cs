using AutoMapper;
using MobileStore.Core.Models;
using MobileStore.Presentation.Models;

namespace MobileStore.Presentation.Helpers.Mapper
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
