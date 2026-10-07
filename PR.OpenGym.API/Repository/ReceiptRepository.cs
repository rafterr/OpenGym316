using Microsoft.EntityFrameworkCore;
using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Data;
using PR.OpenGym.Data;

namespace PR.OpenGym.API.Repository
{
    public class ReceiptRepository : GenericRepository<Receipt>, IReceiptRepository
    {
        private readonly PROpenGymWebContext _dbContext;

        public ReceiptRepository(PROpenGymWebContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Receipt>> GetByAssociateIdAsync(int associateId)
        {
            return await _dbContext.Receipts
                .Where(r => r.AssociateId == associateId)
                .OrderByDescending(r => r.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Receipt>> GetByDateRangeAsync(DateTime from, DateTime to)
        {
            return await _dbContext.Receipts
                .Where(r => r.CreatedOn >= from && r.CreatedOn < to)
                .OrderByDescending(r => r.Id)
                .ToListAsync();
        }
    }
}
