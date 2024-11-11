using MediatR;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using person_api_1.Data;
using person_api_1.Queries;
using person_api_1.Repositories;

namespace person_api_1.Handlers
{
    public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, Person>
    {
        private readonly IPersonRepository _repository;
        private readonly ILogger<GetPersonByIdHandler> _logger;

        public GetPersonByIdHandler(IPersonRepository repository, ILogger<GetPersonByIdHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<Person> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetPersonQuery for PersonId: {PersonId}", request.Id);
            try
            {
                var person = await _repository.GetPersonByIdAsync(request.Id);
                _logger.LogInformation("Person found: {GivenName} {Surname}", person.GivenName, person.Surname);
                return person;
            }
            catch (Exception e)
            {
                _logger.LogWarning("Person with Id {PersonId} was not found", request.Id);
                throw;
            }
        }
    }
}