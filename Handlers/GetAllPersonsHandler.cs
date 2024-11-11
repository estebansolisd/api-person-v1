using MediatR;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using person_api_1.Data;
using person_api_1.Queries;
using person_api_1.Repositories;

namespace person_api_1.Handlers
{
    public class GetAllPersonsHandler : IRequestHandler<GetAllPersonsQuery, List<Person>>
    {
        private readonly IPersonRepository _repository;
        private readonly ILogger<GetAllPersonsHandler> _logger;

        public GetAllPersonsHandler(IPersonRepository repository, ILogger<GetAllPersonsHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<Person>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            List<Person> persons;
            try
            {
                _logger.LogInformation("Getting all persons");
                persons = await _repository.GetAllPeopleAsync();
                _logger.LogInformation("Persons retrieved");
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error getting all persons");
                Console.WriteLine(e);
                throw;
            }

            return persons;
        }
    }
}