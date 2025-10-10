using System.Linq.Expressions;

namespace Finance_it.API.Repositories.GenericRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false, Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false, Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity); 
        Task SaveAsync();
    }
}
