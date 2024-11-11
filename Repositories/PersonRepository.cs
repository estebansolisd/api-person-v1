using person_api_1.Data;

namespace person_api_1.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DatabaseContext _context;

        public PersonRepository(DatabaseContext context)
        {
            _context = context;
        }
        
        public async Task AddPersonAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }

        // public Task<IEnumerable<Person>> GetAllPeopleAsync()
        // {
        //     return Task.FromResult<IEnumerable<Person>>(_people);
        // }
    }
}