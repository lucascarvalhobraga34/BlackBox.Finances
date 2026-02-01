using System.Linq.Expressions;

namespace Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entity);
        void Update(T entity);
        void Remove(T entity);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicado);
    }
}
