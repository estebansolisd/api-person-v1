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

        public GetPersonByIdHandler(IPersonRepository repository)
        {
            _repository = repository;
        }
        public async Task<Person> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken) =>
            await _repository.GetPersonByIdAsync(request.Id);
    }
}