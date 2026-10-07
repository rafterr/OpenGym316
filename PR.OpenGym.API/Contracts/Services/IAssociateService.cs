using NuGet.Protocol.Core.Types;
using PR.OpenGym.Data;
using PR.OpenGym.Data.DTOS;

namespace PR.OpenGym.API.Contracts.Services
{
    public interface IAssociateService : IGenericService<Associate>
    {

        Task<AssociateGetDTO> GetAsync(int associateId);
        Task<List<AssociateGetDTO>> GetByNameAsync(string associateFullname);

        Task<Associate> PostAsync(AssociatePostDTO data);
        Task<Associate> PutAsync(int associateId,AssociatePostDTO data);
        Task<List<AssociateGetDTO>> GetAllAsync();

        Task PostCheckIn(int associateId);
        Task<IEnumerable<CheckIn>> GetAllCheckInsByAssociate(int associateId);
        Task<IEnumerable<CheckIn>> GetAllCheckInsByDate(DateTime date);

        Task<AssociateMembership> GetAssociateMembershipByAssociateIdAsync(int associateId);

        Task<AssociateMembership> CreateAssociateMembershipAsync(AssociateMembership associateMembership);

        Task<bool> UpdateAssociateMembershipAsync(AssociateMembership associateMembershipDTO);

    }
}
