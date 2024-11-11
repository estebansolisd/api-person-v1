using FluentValidation;
using person_api_1.Commands;

namespace person_api_1.Validators
{
    public class RecordBirthCommandValidator : AbstractValidator<RecordBirthCommand>
    {
        public RecordBirthCommandValidator()
        {
            // Custom rule to ensure that either BirthDate or BirthLocation is provided
            RuleFor(x => x)
                .Must(command => command.BirthDate.HasValue || command.BirthDate != DateTime.MinValue || !string.IsNullOrEmpty(command.BirthLocation))
                .WithMessage("Either BirthDate or BirthLocation must be provided.");
        }
    }
}