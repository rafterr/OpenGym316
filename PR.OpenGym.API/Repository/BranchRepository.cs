using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Data;
using PR.OpenGym.Data;

namespace PR.OpenGym.API.Repository
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        public BranchRepository(PROpenGymWebContext dbContext) : base(dbContext)
        {
        }
    }
}
