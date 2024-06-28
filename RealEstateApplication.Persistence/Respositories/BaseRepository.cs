using Microsoft.EntityFrameworkCore;
using RealEstateApplication.Application.Interfaces.Repositories;
using RealEstateApplication.Persistence.Context;

namespace RealEstateApplication.Persistence.Respositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationContext _dbContext;
        protected DbSet<T> Entities => _dbContext.Set<T>();

        public BaseRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> AddAsync(T Entity)
        {
            await Entities.AddAsync(Entity);
            await _dbContext.SaveChangesAsync();
            return Entity;
        }

        public virtual async Task DeleteAsync(T Entity)
        {
            Entities.Remove(Entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }

        public virtual async Task<List<T>> GetAllWithIncludeAsync(List<string> properties)
        {
            var query = Entities.AsQueryable();

            foreach (string property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await Entities.FindAsync(id);
        }

        public virtual async Task UpdateAsync(T Entity, int id)
        {
            var entry = await Entities.FindAsync(id);
            _dbContext.Entry(entry).CurrentValues.SetValues(Entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
