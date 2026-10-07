using PR.OpenGym.Data;

namespace PR.OpenGym.API.Contracts.Repositories
{
    public interface IAssociateRepository : IGenericRepository<Associate>
    {
        new Task<IEnumerable<Associate>> GetAllAsync();
        Task<IEnumerable<Associate>> GetByNameAsync(string associateName);
        Task AddCheckIn(CheckIn checkIn);
        Task<IEnumerable<CheckIn>> GetCheckIns(DateTime dateTime);
        Task<IEnumerable<CheckIn>> GetCheckIns(int associateId);
        Task<AssociateMembership> GetAssociateMembershipByAssociateIdAsync(int associateId);
        Task<AssociateMembership> AddAssociateMembershipAsync(AssociateMembership associateMembership);
        Task<bool> UpdateAssociateMembershipAsync(int membershipId, AssociateMembership associateMembership);
    }
}
