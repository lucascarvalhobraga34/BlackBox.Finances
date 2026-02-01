using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public abstract class EfRepository<T> : IRepository<T>
    where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected EfRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
            => await _dbSet.FindAsync(id);

        public virtual async Task AddAsync(T entity)
            => await _dbSet.AddAsync(entity);

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicado)
            => await _dbSet.Where(predicado).ToListAsync();

        public virtual void Update(T entity)
            => _dbSet.Update(entity);

        public virtual void Remove(T entity)
            => _dbSet.Remove(entity);

        public virtual async Task AddRangeAsync(IEnumerable<T> entity)
        => await _dbSet.AddRangeAsync(entity);
    }
}
