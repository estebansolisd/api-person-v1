using MediatR;

namespace person_api_1.Queries
{
    public class GetAllPersonsQuery : IRequest<List<Person>>, IRequest<Person>
    { }
}