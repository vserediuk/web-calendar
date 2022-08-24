using BusinessLogic.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using AutoMapper;
using BusinessLogic.Models;
using System.Collections.Generic;
using BusinessLogic.Exceptions;
using System.Linq;
using System;
using System.Security.Claims;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Services
{
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _repository;
        private readonly INotificationService _notificationService;
        private readonly IFileService _fileService;
        private readonly ICalendarRepository _calendarRepository;
        private readonly Dictionary<EventRepeatType, (int, int)> _creationLimits =
            new Dictionary<EventRepeatType, (int, int)>()
            {
                { EventRepeatType.None,    (0, 0) },
                { EventRepeatType.Daily,   (365, 1) },
                { EventRepeatType.Weekly,  (365, 7) },
            };

        public EventService(IMapper mapper, IEventRepository repository, 
            INotificationService notificationService, IFileService fileService,
            ICalendarRepository calendarRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _notificationService = notificationService;
            _fileService = fileService;
            _calendarRepository = calendarRepository;
        }

        public void Create(EventCreateModel calendarEvent, string userEmail)
        {
            if (userEmail != _calendarRepository.GetOwnerById(calendarEvent.CalendarId))
                throw new ForbiddenException("This action is not allowed");

            EmailModel email = new EmailModel
            {
                Subject = "Event invitation",
                Body = String.Format("<p>You were invited to an {0} event.</p>", calendarEvent.Name),
                DestinationUsers = new List<UserViewModel>() { new UserViewModel { Email = userEmail } }
            };

            if (calendarEvent.AttachedFile != null)
            {
                var createdFile = _fileService.Create(calendarEvent.AttachedFile);
                calendarEvent.FileId = createdFile.Id;
                email.AttachmentPath = createdFile.Path;
            }


            if (calendarEvent.RepeatType != EventRepeatType.None)
            {
                calendarEvent.GroupCode = Guid.NewGuid();

                var limits = _creationLimits[calendarEvent.RepeatType];
                var timeIteration = new TimeSpan(limits.Item2, 0, 0, 0);

                if (timeIteration < calendarEvent.EndDate - calendarEvent.StartDate ||
                    calendarEvent.EndDate < calendarEvent.StartDate)
                    throw new BadRequestException("Cannot create an event with specified repeat type and date");

                List<Event> tmpEvents = new List<Event>();
                for (var i = 0; i <= limits.Item1; i += limits.Item2)
                {
                    calendarEvent.JobID = _notificationService.ScheduleEmail(email, calendarEvent.StartDate - TimeSpan.FromMinutes(calendarEvent.NotifyTime));
                    tmpEvents.Add(_mapper.Map<Event>(calendarEvent));
                    calendarEvent.StartDate += timeIteration;
                    calendarEvent.EndDate += timeIteration;
                }
                _repository.AddRange(tmpEvents);
            }
            else
            {
                calendarEvent.JobID = _notificationService.ScheduleEmail(email, calendarEvent.StartDate - TimeSpan.FromMinutes(calendarEvent.NotifyTime));
                _repository.Add(_mapper.Map<Event>(calendarEvent));
            }

            _notificationService.SendEmail(email);
        }

        public EventViewModel Edit(EventEditModel calendarEvent, string userEmail)
        {
            if (!_repository.Exists(calendarEvent.Id))
                throw new NotFoundException("There is no such event");

            if (userEmail != _repository.GetOwnerById(calendarEvent.Id))
                throw new ForbiddenException("This action is not allowed");

            EmailModel email = new EmailModel
            {
                Subject = "Event invitation",
                Body = String.Format("<p>Event {0} was changed.</p>", calendarEvent.Name),
                DestinationUsers = new List<UserViewModel>() { new UserViewModel { Email = userEmail } }
            };

            var oldEvent = _mapper.Map<EventViewModel>(_repository.GetById(calendarEvent.Id));

            var limits = _creationLimits[calendarEvent.RepeatType];
            var timeIteration = new TimeSpan(limits.Item2, 0, 0, 0);

            if (calendarEvent.RepeatType != EventRepeatType.None)
            {
                if (timeIteration < calendarEvent.EndDate - calendarEvent.StartDate ||
                    calendarEvent.EndDate < calendarEvent.StartDate)
                    throw new BadRequestException("Cannot edit an event with specified date");
            }

            calendarEvent.FileId = null;
            if (calendarEvent.AttachedFile != null)
            {
                var createdFile = _fileService.Create(calendarEvent.AttachedFile);
                calendarEvent.FileId = createdFile.Id;
                email.AttachmentPath = createdFile.Path;
            }

            var edited = _mapper.Map<EventViewModel>(_repository.Update(_mapper.Map<Event>(calendarEvent)));

            if (edited.StartDate != oldEvent.StartDate ||
                edited.NotifyTime != oldEvent.NotifyTime) {
                _notificationService.CancelEmail(edited.JobId.ToString());
                _notificationService.ScheduleEmail(new EmailModel
                {
                    Subject = "Event invitation",
                    Body = String.Format("<p>You were invited to an {0} event.</p>", edited.Name),
                    DestinationUsers = new List<UserViewModel>() { new UserViewModel { Email = userEmail } }
                }, edited.StartDate - TimeSpan.FromMinutes(edited.NotifyTime));
            }

            _notificationService.SendEmail(email);
            return edited;
        }
        public void Delete(int id, string userEmail)
        {
            if (!_repository.Exists(id))
                throw new NotFoundException("There is no such event");

            if (userEmail != _repository.GetOwnerById(id))
                throw new ForbiddenException("This action is not allowed");

            var calendarEvent = _mapper.Map<EventViewModel>(_repository.GetById(id));
            EmailModel email = new EmailModel
            {
                Subject = "Event invitation",
                Body = String.Format("<p>Event {0} was deleted.</p>", calendarEvent.Name),
                DestinationUsers = new List<UserViewModel>() { new UserViewModel { Email = userEmail } }
            };

            _notificationService.CancelEmail(calendarEvent.JobId.ToString());
            _repository.DeleteById(id);

            _notificationService.SendEmail(email);
        }

        public EventViewModel GetById(int id)
        {
            var calendarEvent = _repository.GetById(id);
            if (calendarEvent == null)
                throw new NotFoundException("There is no such event");
            return _mapper.Map<EventViewModel>(calendarEvent);
        }

        public IEnumerable<EventViewModel> GetAllByCalendarId(int calendarId)
        {
            return _mapper.Map<IEnumerable<EventViewModel>>(_repository.GetAllByCalendarId(calendarId));
        }
    }
}