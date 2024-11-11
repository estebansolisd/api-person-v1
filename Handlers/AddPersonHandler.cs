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
        private readonly ILogger<AddPersonHandler> _logger;


        public AddPersonHandler(IPersonRepository repository, IValidator<AddPersonCommand> validator, ILogger<AddPersonHandler> logger)
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Person> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            _logger.LogInformation("Handling AddPersonCommand for {GivenName} {Surname}", request.Person.GivenName, request.Person.Surname);
            
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
            
            var history = new PersonHistory
            {
                PersonId = person.Id,
                Version = 1,
                GivenName = person.GivenName,
                Surname = person.Surname,
                Gender = person.Gender,
                BirthDate = person.BirthDate,
                BirthLocation = person.BirthLocation,
                DeathDate = person.DeathDate,
                DeathLocation = person.DeathLocation,
                Timestamp = DateTime.UtcNow
            };
            

            await _repository.AddPersonHistoryAsync(history);
            
            _logger.LogInformation("History added for person: {PersonId}", person.Id);

            await _repository.AddPersonAsync(person);
            
            _logger.LogInformation("Person added successfully with Id: {PersonId}", person.Id);

            return person;
        }
    }
}