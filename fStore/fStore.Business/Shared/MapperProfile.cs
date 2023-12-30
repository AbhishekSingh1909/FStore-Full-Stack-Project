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
        CreateMap<UserUpdateDTO, User>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

        CreateMap<Address, AddressReadDTO>();
        CreateMap<AddressCreateDTO, Address>();
        CreateMap<AddressUpdateDTO, Address>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

        CreateMap<Category, CategoryReadDTO>();
        CreateMap<CategoryCreateDTO, Category>();
        CreateMap<CategoryUpdateDTO, Category>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

        CreateMap<Product, ProductReadDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(m => m.ImageUrl)))
           .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
        //.ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images
        //    .Select(img => new ImageReadDTO { ImageUrl = img.ImageUrl, Id = img.Id, ProductId = img.ProductId })));

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
        CreateMap<ImageUpdateDTO, Image>().ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

        CreateMap<Order, OrderReadDTO>().ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.OrderDetails))
                                        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OrderStatus));
        CreateMap<OrderCreateDTO, Order>()
            .ForMember(dest => dest.OrderDetails,
                opt => opt.MapFrom(src => src.OrderProducts));
        CreateMap<OrderUpdateDTO, Order>().ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status)).ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

        CreateMap<OrderProduct, OrderProductReadDTO>();
        CreateMap<OrderProductCreateDTO, OrderProduct>();


    }

}
