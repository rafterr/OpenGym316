using PR.OpenGym.Data;
using PR.OpenGym.Data.DTOS;

namespace PR.OpenGym.API.Contracts.Services
{
    public interface IPaymentService : IGenericService<Payment>
    {
        Task<bool> PayAssociateMembership(CreatePaymentDTO createPaymentDTO);
        Task<IEnumerable<Payment>> GetTodayPayments();

        Task<IEnumerable<Payment>> GetPaymentsByAssociateId(int associateId);
    }
}
