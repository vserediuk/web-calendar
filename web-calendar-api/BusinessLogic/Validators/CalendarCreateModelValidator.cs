using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class CalendarCreateModelValidator : AbstractValidator<CalendarCreateModel>
    {
        public CalendarCreateModelValidator()
        {
            RuleFor(calendar => calendar.UserId).NotEmpty();
            RuleFor(calendar => calendar.Name).NotEmpty().MaximumLength(20);
        }
    }
}
