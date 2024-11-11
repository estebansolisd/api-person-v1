using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using person_api_1.Commands;
using person_api_1.Models;
using person_api_1.Repositories;
using ValidationException = FluentValidation.ValidationException;

namespace person_api_1.Handlers
{
    public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand, bool>
    {
        private readonly IPersonRepository _repository;
        private readonly IValidator<UpdatePersonCommand> _validator;
        private readonly ILogger<UpdatePersonHandler> _logger;


        public UpdatePersonHandler(IPersonRepository repository, IValidator<UpdatePersonCommand> validator, ILogger<UpdatePersonHandler> logger)
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var flag = false;
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            _logger.LogInformation("Handling UpdatePersonCommand for {GivenName} {Surname}", request.Person.GivenName, request.Person.Surname);

            try
            {
                var person = await _repository.GetPersonByIdAsync(request.Id);
              
                
                
                person.GivenName = request.Person.GivenName;
                person.Surname = request.Person.Surname;
                person.Gender = (Gender)request.Person.Gender;
                person.BirthDate = request.Person.BirthDate;
                person.BirthLocation = request.Person.BirthLocation;
                person.DeathDate = request.Person.DeathDate;
                person.DeathLocation = request.Person.DeathLocation;

                _logger.LogInformation("Person update successfully with Id: {PersonId}", person.Id);
                flag = await _repository.UpdatePersonAsync(person);
                
                var personHistories = await _repository.GetPersonHistoryAsync(request.Id);
                var lastHistory = personHistories.LastOrDefault();
                
                var history = new PersonHistory
                {
                    PersonId = person.Id,
                    Version = (lastHistory?.Version ?? 0) + 1,
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

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return flag;
        }
    }
}