using AutoMapper;
using BusinessLogic.Models;
using DataAccess.Entities;

namespace BusinessLogic.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
            CreateMap<UserRegistrationModel, User>().ForMember(dest => dest.UserName, opt => opt.MapFrom(m => m.Email));
        }
    }
}