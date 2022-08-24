using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class UserRegistrationModelValidator : AbstractValidator<UserRegistrationModel>
    {
        public UserRegistrationModelValidator()
        {
            RuleFor(m => m.FirstName).MinimumLength(1).MaximumLength(24);
            RuleFor(m => m.LastName).MinimumLength(1).MaximumLength(24);
        }
    }
}