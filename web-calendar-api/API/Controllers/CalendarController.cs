using System.Collections.Generic;
using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpPost]
        public ActionResult<CalendarCreateModel> Post(CalendarCreateModel calendar)
        {
            return Ok(_calendarService.Create(calendar));
        }

        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<CalendarViewModel>> Get(int id)
        {
            return Ok(_calendarService.GetAllByUserId(id));
        }
    }
}