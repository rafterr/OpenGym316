using PR.OpenGym.Data;

namespace PR.OpenGym.API.Contracts.Repositories
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetTodayPayments();
        Task<IEnumerable<Payment>> GetPaymentsByAssociateId(int associateId);
    }
}
