using FluentValidation;
using MediatR;
using person_api_1.Commands;
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
            }
            catch (Exception e)
            {
                _logger.LogInformation("Unable to update birth information for user id {Id}", request.Id);
            }
            return flag;
        }
    }
}