using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces;

public interface IRepository<T> where T : class
{
    Task<ICollection<T>> GetAllAsync();
    Task<T> GetAsync(int id);
    Task<ICollection<T>> FindAsync(Func<T, Boolean> predicate);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task DeleteAsync(T entity);
}
