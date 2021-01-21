using AutoMapper;
using UsersIO.Api.DTOs;
using UsersIO.Business.Models;

namespace UsersIO.Api.Mappers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Phone, PhoneDTO>().ReverseMap();
            CreateMap<SocialMidia, SocialMidiaDTO>().ReverseMap();
        }

    }
}
