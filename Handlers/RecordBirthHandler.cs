using FluentValidation;
using MediatR;
using person_api_1.Commands;
using person_api_1.Models;
using person_api_1.Repositories;

namespace person_api_1.Handlers
{
    public class RecordBirthHandler : IRequestHandler<RecordBirthCommand, bool>
    {
        private readonly IPersonRepository _repository;
        private readonly IValidator<RecordBirthCommand> _validator;
        private readonly ILogger<RecordBirthHandler> _logger;

        public RecordBirthHandler(IPersonRepository repository, IValidator<RecordBirthCommand> validator, ILogger<RecordBirthHandler> logger)
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<bool> Handle(RecordBirthCommand request, CancellationToken cancellationToken)
        {
            bool flag = false;
            
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }    
            
            try
            {

                var person = await _repository.GetPersonByIdAsync(request.Id);
                _logger.LogInformation("Handling RecordBirthCommand for {GivenName} {Surname}", person.GivenName, person.Surname);
                // Update birth information
                person.BirthDate = request.BirthDate;
                person.BirthLocation = request.BirthLocation;

                // Save changes in the repository
                await _repository.UpdatePersonAsync(person);
                _logger.LogInformation("Updating birth date and birth location for {GivenName} {Surname}", person.GivenName, person.Surname);


                flag = true;
                
                var personHistories = await _repository.GetPersonHistoryAsync(request.Id);
                var lastHistory = personHistories.LastOrDefault();
                
                var history = new PersonHistory()
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
                _logger.LogInformation("Unable to update birth information for user id {Id}", request.Id);
            }
            return flag;
        }
    }
}