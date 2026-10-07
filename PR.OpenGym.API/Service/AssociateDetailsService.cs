using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.Data;

namespace PR.OpenGym.API.Service
{
    public class AssociateDetailsService : GenericService<AssociateDetails>, IAssociateDetailsService
    {
        private readonly IAssociateDetailsRepository _repository;

        public AssociateDetailsService(IAssociateDetailsRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<AssociateDetails> GetAssociateDetailsByAssociateId(int associateId)
        {
            return await _repository.GetAssociateDetailsByAssociateIdAsync(associateId);
        }
    }
}
