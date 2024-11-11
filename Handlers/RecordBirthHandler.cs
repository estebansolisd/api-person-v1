using MediatR;
using person_api_1.Commands;
using person_api_1.Repositories;

namespace person_api_1.Handlers
{
    public class RecordBirthHandler : IRequestHandler<RecordBirthCommand, bool>
    {
        private readonly IPersonRepository _repository;

        public RecordBirthHandler(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(RecordBirthCommand request, CancellationToken cancellationToken)
        {
            bool flag = false;
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