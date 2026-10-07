using Microsoft.EntityFrameworkCore;
using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Data;

namespace PR.OpenGym.API.Repository

{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly PROpenGymWebContext _dbContext;

        public GenericRepository(PROpenGymWebContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            if (entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }
    }
}
