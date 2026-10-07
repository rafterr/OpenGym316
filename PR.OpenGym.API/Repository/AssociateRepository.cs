using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Data;
using PR.OpenGym.Data;
using System;
using System.Linq;

namespace PR.OpenGym.API.Repository
{
    public class AssociateRepository : GenericRepository<Associate>, IAssociateRepository
    {
        private readonly PROpenGymWebContext _dbContext;

        public AssociateRepository(PROpenGymWebContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        new public async Task<IEnumerable<Associate>> GetAllAsync()
        {
            return await _dbContext
                .Associates
                .Include(e => e.AssociateMembership)
                .Include(e => e.Branch)
                .Take(10000)
                .ToListAsync();
        }

        public async Task<IEnumerable<Associate>> GetByNameAsync(string associateName)
        {
            string sql = $"SELECT * FROM associates a WHERE concat(a.FirstName,' ',a.LastName) like '%{associateName}%'";
            return await _dbContext
               .Associates
               .FromSqlRaw(sql)
               .Include(e => e.AssociateMembership)
               .Include(e => e.Branch)
               .ToListAsync();
            //.Include(e => e.AssociateMembership)
            //.Include(e => e.Branch)
            //.Where(e => (e.FirstName + " " + e.LastName).Contains(associateName))
            //.Where(e => e.FirstName.Contains(associateName))

        }

        new public async Task<Associate> GetAsync(int id)
        {
            var entity = await _dbContext.Associates
                .Include(a => a.AssociateMembership)
                .Include(a => a.Branch)
                .SingleOrDefaultAsync(e => e.Id == id);

            return entity;
        }

        public async Task AddCheckIn(CheckIn checkIn)
        {
            await _dbContext.CheckIns.AddAsync(checkIn);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<AssociateMembership> AddAssociateMembershipAsync(AssociateMembership associateMembership)
        {
            await _dbContext.AssociateMemberships.AddAsync(associateMembership);
            await _dbContext.SaveChangesAsync();
            return associateMembership;
        }

        public async Task<AssociateMembership> GetAssociateMembershipByAssociateIdAsync(int associateId)
        {
            return _dbContext.AssociateMemberships.Where(m => m.Associate.Id == associateId).Include(m => m.Membership).OrderByDescending(m => m.CreatedOn).FirstOrDefault();
        }

        public async Task<IEnumerable<CheckIn>> GetCheckIns(DateTime dateTime)
        {
            return _dbContext.CheckIns.Include(c => c.Associate).Where(c => c.CreatedOn.Date == dateTime.Date).GroupBy(c => c.AssociateId).Select(s => s.First());
        }

        public async Task<IEnumerable<CheckIn>> GetCheckIns(int associateId)
        {
            return _dbContext.CheckIns.Include(c => c.Associate).Where(c => c.AssociateId == associateId);
        }

        public async Task<bool> UpdateAssociateMembershipAsync(int membershipId, AssociateMembership associateMembership)
        {
            _dbContext.Entry(associateMembership).State = EntityState.Modified;
            var res = await _dbContext.SaveChangesAsync();
            return res > 0;
        }
    }
}
