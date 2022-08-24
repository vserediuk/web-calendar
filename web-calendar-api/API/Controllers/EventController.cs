using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ICalendarService _calendarService;
        private readonly IUserService _userService;

        public EventController(IEventService eventService, ICalendarService calendarService, IUserService userService)
        {
            _eventService = eventService;
            _calendarService = calendarService;
            _userService = userService;
        }

        [HttpPost]
        public ActionResult<EventCreateModel> Post([FromForm] EventCreateModel calendarEvent)
        {
            _eventService.Create(calendarEvent, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(calendarEvent);
        }

        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<EventViewModel>> Get(int id)
        {
            return Ok(_eventService.GetAllByCalendarId(id));
        }

        [HttpPut]
        public ActionResult<EventViewModel> Edit([FromForm] EventEditModel calendarEvent)
        {
            return Ok(_eventService.Edit(calendarEvent, User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        [HttpDelete("{id:int}")]
        public NoContentResult Delete(int id)
        {
            _eventService.Delete(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return NoContent();
        }
    }
}
