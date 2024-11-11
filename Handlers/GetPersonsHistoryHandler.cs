using MediatR;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using person_api_1.Models;
using person_api_1.Queries;
using person_api_1.Repositories;

namespace person_api_1.Handlers
{
    public class GetPersonHistoryHandler : IRequestHandler<GetPersonHistoryQuery, List<PersonHistory>>
    {
        private readonly IPersonRepository _repository;
        private readonly ILogger<GetPersonHistoryHandler> _logger;

        public GetPersonHistoryHandler(IPersonRepository repository,  ILogger<GetPersonHistoryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<List<PersonHistory>> Handle(GetPersonHistoryQuery request,
            CancellationToken cancellationToken)
        {

            List<PersonHistory> personHistories;

            try
            {
                _logger.LogInformation("Getting person history from user id {Id}", request.PersonId);
                personHistories = await _repository.GetPersonHistoryAsync(request.PersonId);
                _logger.LogInformation("History retrieved for user {Id}", request.PersonId);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error loading the person histories {Message}", e.Message);
                Console.WriteLine(e);
                throw;
            }

            return personHistories;
        }
    }
}