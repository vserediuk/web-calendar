using AutoMapper;
using DataAccess.Entities;
using BusinessLogic.Models;

namespace BusinessLogic.Mappers
{
    public class CalendarMappingProfile : Profile
    {
        public CalendarMappingProfile()
        {
            CreateMap<Calendar, CalendarViewModel>();
            CreateMap<CalendarCreateModel, Calendar>()
                .ForMember(dest => dest.OwnerUserId, opt => opt.MapFrom(c => c.UserId));
            CreateMap<CalendarCreateModel, CalendarViewModel>();
        }
    }
}