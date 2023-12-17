using AutoMapper;
using fStore.Core;

namespace fStore.Business;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User,UserReadDTO>();
            CreateMap<UserCreateDTO, User>();
            CreateMap<UserUpdateDTO, User>();

           CreateMap<Address, AddressReadDTO>();
           CreateMap<AddressCreateDTO, Address>();
           CreateMap<AddressUpdateDTO, Address>();
        }
        
    }
