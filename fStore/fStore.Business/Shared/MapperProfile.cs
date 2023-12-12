using AutoMapper;
using fStore.Core;

namespace fStore.Business;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User,UserReadDTO>();
            CreateMap<UserCreateDTO, User>();
        }
        
    }
