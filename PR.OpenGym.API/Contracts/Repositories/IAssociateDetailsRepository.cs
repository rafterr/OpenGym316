using PR.OpenGym.Data;

namespace PR.OpenGym.API.Contracts.Repositories
{
    public interface IAssociateDetailsRepository: IGenericRepository<AssociateDetails>
    {
        Task<AssociateDetails> GetAssociateDetailsByAssociateIdAsync(int associateId);
    }
}
