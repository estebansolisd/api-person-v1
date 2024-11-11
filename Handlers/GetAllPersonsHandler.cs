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

        public GetAllPersonsHandler(IPersonRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Person>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken) =>
            await _repository.GetAllPeopleAsync();
    }
}