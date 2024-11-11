namespace person_api_1.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly List<Person> _people = new();

        // public Task<Person> GetPersonByIdAsync(Guid personId)
        // {
        //     return Task.FromResult(_people.FirstOrDefault(p => p.Id == personId));
        // }
        //
        // public Task UpdatePersonAsync(Person person)
        // {
        //     var existingPerson = _people.FirstOrDefault(p => p.Id == person.Id);
        //     if (existingPerson != null)
        //     {
        //         _people.Remove(existingPerson);
        //         _people.Add(person);
        //     }
        //     return Task.CompletedTask;
        // }

        public Task AddPersonAsync(Person person)
        {
            _people.Add(person);
            return Task.CompletedTask;
        }

        // public Task<IEnumerable<Person>> GetAllPeopleAsync()
        // {
        //     return Task.FromResult<IEnumerable<Person>>(_people);
        // }
    }
}