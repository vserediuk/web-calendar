using System.Collections.Generic;
using System.Linq;
using DataAccess.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using AutoMapper;
using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using DataAccess.Entities;

namespace BusinessLogic.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IMapper _mapper;
        private readonly ICalendarRepository _repository;

        public CalendarService(IMapper mapper, ICalendarRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public CalendarViewModel Create(CalendarCreateModel calendar)
        {
            _repository.Add(_mapper.Map<Calendar>(calendar));
            return _mapper.Map<CalendarViewModel>(calendar);
        }

        public CalendarViewModel GetById(int id)
        {
            var calendar = _repository.GetById(id);
            if (calendar == null)
                throw new NotFoundException("There is no such calendar");
            return _mapper.Map<CalendarViewModel>(_repository.GetById(id));
        }

        public IEnumerable<CalendarViewModel> GetAllByUserId(int userId)
        {
            var calendars = _repository.GetAllByUserId(userId);
            if (!calendars.Any())
                throw new NotFoundException("There is no calendars");
            return _mapper.Map<IEnumerable<CalendarViewModel>>(_repository.GetAllByUserId(userId));
        }
    }
}