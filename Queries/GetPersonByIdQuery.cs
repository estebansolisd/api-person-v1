using MediatR;

namespace person_api_1.Queries
{
    public class GetPersonByIdQuery : IRequest<Person>
    {
        public Guid Id { get; set; }
        public GetPersonByIdQuery(Guid id) => Id = id;
    }
}