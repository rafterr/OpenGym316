using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Data;
using PR.OpenGym.Data;

namespace PR.OpenGym.API.Repository
{
    public class AssociateDetailsRepository :
        GenericRepository<AssociateDetails>, IAssociateDetailsRepository
    {
        private readonly PROpenGymWebContext _dbContext;

        public AssociateDetailsRepository(PROpenGymWebContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AssociateDetails> GetAssociateDetailsByAssociateIdAsync(int associateId)
        {
            var associateDetails =  _dbContext.AssociateDetails.Where(assocciateDetails => assocciateDetails.AssociateId == associateId).SingleOrDefault();
            return associateDetails;
        }
    }
}
