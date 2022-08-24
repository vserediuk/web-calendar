using BusinessLogic.Models;
using FluentValidation;
using System;

namespace BusinessLogic.Validators
{
    class EventEditModelValidator : AbstractValidator<EventEditModel>
    {
        public EventEditModelValidator()
        {
            RuleFor(calendarEvent => calendarEvent.Name).NotEmpty().MaximumLength(20);
            RuleFor(calendarEvent => calendarEvent.StartDate).NotEmpty().GreaterThan(DateTime.Now);
            RuleFor(calendarEvent => calendarEvent.EndDate).NotEmpty().GreaterThan(DateTime.Now);
            RuleFor(calendarEvent => calendarEvent.RepeatType).NotEmpty().IsInEnum();
        }
    }
}
