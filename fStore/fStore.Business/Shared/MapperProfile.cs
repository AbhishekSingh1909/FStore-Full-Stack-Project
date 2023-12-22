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
        CreateMap<CategoryUpdateDTO, Category>();

        CreateMap<Product, ProductReadDTO>();
        CreateMap<ProductCreateDTO, Product>();
        CreateMap<ProductUpdateDTO, Product>();

        CreateMap<Image, ImageReadDTO>();
        // I need help on this line
        CreateMap<ImageCreateDTO, Image>().ForMember(dest => dest.Product, opt => opt.MapFrom(src => new Product());
        CreateMap<ImageUpdateDTO, Image>();


    }

}
