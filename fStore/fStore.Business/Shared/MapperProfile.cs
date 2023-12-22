using AutoMapper;
using fStore.Business.DTO;
using fStore.Core;

namespace fStore.Business;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserReadDTO>();
        CreateMap<UserCreateDTO, User>();
        CreateMap<UserUpdateDTO, User>();

        CreateMap<Address, AddressReadDTO>();
        CreateMap<AddressCreateDTO, Address>();
        CreateMap<AddressUpdateDTO, Address>();

        CreateMap<Category, CategoryReadDTO>();
        CreateMap<CategoryCreateDTO, Category>();
        CreateMap<CategoryUpdateDTO, Category>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

        CreateMap<Product, ProductReadDTO>()
           .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
           .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images
               .Select(img => new ImageReadDTO { ImageUrl = img.ImageUrl, Id = img.Id, ProductId = img.ProductId })));

        CreateMap<ProductCreateDTO, Product>()
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.Images,
                 opt => opt.MapFrom(src => src.Images.Select(i =>
            new Image { ImageUrl = i.ImageUrl })));

        CreateMap<ProductUpdateDTO, Product>()
                .ForMember(dest => dest.CategoryId,
                    opt => opt.MapFrom(src => src.CategoryId))
                .ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

        CreateMap<Image, ImageReadDTO>();
        CreateMap<ImageCreateDTO, Image>();
        CreateMap<ImageUpdateDTO, Image>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null)
            );


    }

}
