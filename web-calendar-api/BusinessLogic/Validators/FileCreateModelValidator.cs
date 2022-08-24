using BusinessLogic.Models;
using FluentValidation;

namespace BusinessLogic.Validators
{
    class FileCreateModelValidator : AbstractValidator<FileCreateModel>
    {
        public FileCreateModelValidator()
        {
            RuleFor(file => file.Name).NotEmpty();
            RuleFor(file => file.Path).NotEmpty();
        }
    }
}
