namespace CleanArchitectrure.Application.Interface.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        /* Commands */
        Task<int> AddEditAsync(T entity);
        Task<bool> DeleteAsync(int id);
        /* Queries */
        Task<T> GetAsync(int id);
        Task<List<T>> GetAllAsync();
    }
}
