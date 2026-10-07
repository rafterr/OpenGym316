using PR.OpenGym.Data;

namespace PR.OpenGym.API.Contracts.Repositories
{
    public interface IReceiptRepository : IGenericRepository<Receipt>
    {
        Task<IEnumerable<Receipt>> GetByAssociateIdAsync(int associateId);
        Task<IEnumerable<Receipt>> GetByDateRangeAsync(DateTime from, DateTime to);
    }
}
