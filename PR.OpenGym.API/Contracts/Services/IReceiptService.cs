using PR.OpenGym.Data;

namespace PR.OpenGym.API.Contracts.Services
{
    public interface IReceiptService : IGenericService<Receipt>
    {
        /// <summary>
        /// Genera el recibo de un pago con la copia de los datos del socio y la membresia
        /// </summary>
        Task<Receipt> CreateForPaymentAsync(Payment payment, Associate associate, Membership membership, AssociateMembership associateMembership, string? issuedBy);
        Task<IEnumerable<Receipt>> GetByAssociateIdAsync(int associateId);
        Task<IEnumerable<Receipt>> GetByDateRangeAsync(DateTime from, DateTime to);
        Task<bool> CancelAsync(int receiptId, string? reason);
    }
}
