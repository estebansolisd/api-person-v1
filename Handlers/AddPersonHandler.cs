using MediatR;
using person_api_1.Commands;
using person_api_1.Models;
using person_api_1.Repositories;

namespace person_api_1.Handlers
{
    public class AddPersonHandler : IRequestHandler<AddPersonCommand, Person>
    {
        private readonly IPersonRepository _repository;

        public AddPersonHandler(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<Person> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person
            {
                Id = Guid.NewGuid(),
                GivenName = request.Person.GivenName,
                Surname = request.Person.Surname,
                Gender = request.Person.Gender,
                BirthDate = request.Person.BirthDate,
                BirthLocation = request.Person.BirthLocation,
            };

            await _repository.AddPersonAsync(person);

            return person;
        }
    }
}