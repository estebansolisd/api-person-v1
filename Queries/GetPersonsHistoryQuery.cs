using MediatR;
using person_api_1.Models;

namespace person_api_1.Queries
{
    public class GetPersonHistoryQuery : IRequest<List<PersonHistory>>
    {
        public Guid PersonId { get; set; }
        public GetPersonHistoryQuery(Guid personId) => PersonId = personId;
    }
}