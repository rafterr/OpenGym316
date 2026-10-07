using PR.OpenGym.Data;

namespace PR.OpenGym.API.Contracts.Services
{
    public interface IAssociateDetailsService : IGenericService<AssociateDetails>
    {
        Task<AssociateDetails> GetAssociateDetailsByAssociateId(int associateId);
    }
}
