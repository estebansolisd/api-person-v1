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

        public RecordBirthHandler(IPersonRepository repository, IValidator<RecordBirthCommand> validator)
        {
            _repository = repository;
            _validator = validator;
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
                // Update birth information
                person.BirthDate = request.BirthDate;
                person.BirthLocation = request.BirthLocation;

                // Save changes in the repository
                await _repository.UpdatePersonAsync(person);

                flag = true;
            }
            catch (Exception e)
            {
             // Logging here  
            }
            return flag;
        }
    }
}