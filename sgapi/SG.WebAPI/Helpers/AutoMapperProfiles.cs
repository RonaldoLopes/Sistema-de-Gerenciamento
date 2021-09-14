

using AutoMapper;
using SG.Domain.Entities;
using SG.Domain.Identity;
using SG.WebApi.Dto;

namespace SG.WebApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();

        }
    }
}
