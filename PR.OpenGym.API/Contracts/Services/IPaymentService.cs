using PR.OpenGym.Data;
using PR.OpenGym.Data.DTOS;

namespace PR.OpenGym.API.Contracts.Services
{
    public interface IPaymentService : IGenericService<Payment>
    {
        /// <summary>
        /// Cobra la membresia, actualiza la vigencia del socio y genera el recibo
        /// </summary>
        Task<PaymentResultDTO?> PayAssociateMembership(CreatePaymentDTO createPaymentDTO);

        /// <summary>
        /// Alta de socio cobrando la primera membresia y generando su recibo
        /// </summary>
        Task<Associate> CreateAssociateWithInitialPaymentAsync(AssociatePostDTO associatePostDTO);

        Task<IEnumerable<Payment>> GetTodayPayments();

        Task<IEnumerable<Payment>> GetPaymentsByAssociateId(int associateId);
    }
}
