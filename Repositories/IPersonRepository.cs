namespace person_api_1.Repositories
{
    public interface IPersonRepository
    {
        Task<bool> UpdatePersonAsync(Person person);
        Task<Person> GetPersonByIdAsync(Guid id);
        Task AddPersonAsync(Person person);
        Task<List<Person>> GetAllPeopleAsync();

    }
}