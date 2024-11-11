namespace person_api_1.Repositories
{
    public interface IPersonRepository
    {
        Task AddPersonAsync(Person person);
    }
}