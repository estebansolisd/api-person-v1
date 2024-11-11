using FluentValidation;
using person_api_1.Commands;

namespace person_api_1.Validators
{
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
        {
            RuleFor(x => x.Person.Gender)
                .NotNull().WithMessage("Gender is required.");

            RuleFor(x => x)
                .Must(x => !string.IsNullOrWhiteSpace(x.Person.GivenName) || !string.IsNullOrWhiteSpace(x.Person.Surname))
                .WithMessage("At least one name (GivenName or Surname) must be provided.");
        }
    }
}