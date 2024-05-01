namespace Portfilo.Models.Repository
{
    public interface IRepository<T>
    {
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id, T entity);
        Task ActiveAsync(int id, T entity);
        Task<T> FindAsync(int id);
        Task<IEnumerable<T>> GetAllForAdminAsync();
        Task<IEnumerable<T>> GetAllForClientAsync();
    }
}
