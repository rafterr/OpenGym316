using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.Data;

namespace PR.OpenGym.API.Service
{
    public class BranchService : GenericService<Branch>, IBranchService
    {
        public BranchService(IBranchRepository repository) : base(repository)
        {
        }
    }
}
