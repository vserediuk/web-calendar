using BusinessLogic.Models;
using FluentValidation;
using System;

namespace BusinessLogic.Validators
{
    public class EventCreateModelValidator : AbstractValidator<EventCreateModel>
    {
        public EventCreateModelValidator()
        {
            RuleFor(calendarEvent => calendarEvent.CalendarId).NotEmpty();
            RuleFor(calendarEvent => calendarEvent.Name).NotEmpty().MaximumLength(20);
            RuleFor(calendarEvent => calendarEvent.StartDate).NotEmpty().GreaterThan(DateTime.Now);
            RuleFor(calendarEvent => calendarEvent.EndDate).NotEmpty().GreaterThan(DateTime.Now);
            RuleFor(calendarEvent => calendarEvent.RepeatType).NotEmpty().IsInEnum();
        }
    }
}
