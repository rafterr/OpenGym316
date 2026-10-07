using Microsoft.EntityFrameworkCore;
using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Data;
using PR.OpenGym.Data;

namespace PR.OpenGym.API.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly PROpenGymWebContext _dbContext;

        public ProductRepository(PROpenGymWebContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Membership> GetMembeship(int id)
        {
            return _dbContext.Memberships.Find(id);
        }

        public async Task<IEnumerable<Membership>> GetMembeships()
        {
            return await _dbContext.Memberships.ToListAsync();
        }
    }
}
