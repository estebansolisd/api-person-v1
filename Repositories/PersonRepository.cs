using Microsoft.EntityFrameworkCore;
using person_api_1.Data;
using person_api_1.Models;

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

        public async Task<bool> UpdatePersonAsync(Person person)
        {
            _context.Persons.Update(person);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Person> GetPersonByIdAsync(Guid id) =>
            await _context.Persons.FirstAsync( x => x.Id == id);

        public async Task<List<Person>> GetAllPeopleAsync() => 
            await _context.Persons.ToListAsync();
        public async Task<List<PersonHistory>> GetPersonHistoryAsync(Guid personId) =>
            await _context.PersonHistories
                .Where(ph => ph.PersonId == personId)
                .OrderBy(ph => ph.Timestamp)
                .ToListAsync();
        public async Task AddPersonHistoryAsync(PersonHistory personHistory)
        {
            _context.PersonHistories.Add(personHistory);
            await _context.SaveChangesAsync();
        }
    }
}