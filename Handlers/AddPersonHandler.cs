using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using person_api_1.Commands;
using person_api_1.Models;
using person_api_1.Repositories;
using ValidationException = FluentValidation.ValidationException;

namespace person_api_1.Handlers
{
    public class AddPersonHandler : IRequestHandler<AddPersonCommand, Person>
    {
        private readonly IPersonRepository _repository;
        private readonly IValidator<AddPersonCommand> _validator;

        public AddPersonHandler(IPersonRepository repository, IValidator<AddPersonCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Person> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            
            var person = new Person
            {
                Id = Guid.NewGuid(),
                GivenName = request.Person.GivenName,
                Surname = request.Person.Surname,
                Gender = (Gender)request.Person.Gender!,
                BirthDate = request.Person.BirthDate,
                BirthLocation = request.Person.BirthLocation,
                DeathDate = request.Person.BirthDate,
                DeathLocation = request.Person.DeathLocation,
            };

            await _repository.AddPersonAsync(person);

            return person;
        }
    }
}