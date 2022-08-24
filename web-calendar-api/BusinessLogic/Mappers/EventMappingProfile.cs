using AutoMapper;
using BusinessLogic.Models;
using DataAccess.Entities;

namespace BusinessLogic.Mappers
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Event, EventCreateModel>()
                .ForMember(dest => dest.RepeatType, opt => opt.MapFrom(src => src.RepeatTypeId));
            CreateMap<Event, EventViewModel>()
                .ForMember(dest => dest.RepeatType, opt => opt.MapFrom(src => src.RepeatTypeId));
            CreateMap<Event, EventEditModel>()
                .ForMember(dest => dest.RepeatType, opt => opt.MapFrom(src => src.RepeatTypeId));
            CreateMap<EventCreateModel, Event>()
                .ForMember(dest => dest.RepeatTypeId, opt => opt.MapFrom(src => src.RepeatType));
            CreateMap<EventViewModel, Event>()
                .ForMember(dest => dest.RepeatTypeId, opt => opt.MapFrom(src => src.RepeatType));
            CreateMap<EventEditModel, Event>()
                .ForMember(dest => dest.RepeatTypeId, opt => opt.MapFrom(src => src.RepeatType));
        }
    }
}