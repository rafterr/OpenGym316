using PR.OpenGym.Data;
using PR.OpenGym.Data.DTOS;

namespace PR.OpenGym.Web.Services
{
    public interface IApiService
    {
        Task<Product> GetProduct(int id);
        Task<IEnumerable<Product>> GetProducts(bool isMembership);
       
        
        Task<AssociateGetDTO> GetAssociate(int id);
        Task<AssociateDetails> GetAssociateDetails(int associateDetailsId);
        Task<IEnumerable<AssociateGetDTO>> GetAssociates();
        Task<IEnumerable<AssociateGetDTO>> GetAssociatesByName(string associateName);
        Task<IEnumerable<CheckIn>> GetCheckIns();


        Task<AssociateGetDTO> PostAssociate(AssociatePostDTO associate, IFormFile formFile);
        Task<AssociateDetails> PostAssociateDetails(AssociateDetails associate);

        Task<AssociateMembership> GetAssociateMembership(int associateId);
        Task<AssociateMembership> PostAssociateMembership(AssociateMembership membership);

        Task<IEnumerable<Payment>> GetTodayPayments();
        Task<IEnumerable<Payment>> GetPaymentsByAssociateId(int associateId);


        Task<bool> UpdateAssociate(int associateId, AssociatePostDTO associate);
        Task<bool> UpdateAssociateDetails(int associateDetailsId,  AssociateDetails associateDetails);


        Task<Membership> GetMembershipById(int membershipId);
        Task<bool> PostMembership(Membership membership);
        Task<bool> UpdateMembership(Membership membership);
        Task<bool> DeleteMembership(int mebershipId);


        Task<PaymentResultDTO?> CreatePayment(CreatePaymentDTO createPaymentDTO);

        Task<Receipt?> GetReceipt(int receiptId);
        Task<IEnumerable<Receipt>> GetReceiptsByAssociateId(int associateId);
        Task<IEnumerable<Receipt>> GetReceipts(DateTime from, DateTime to);
        Task<bool> CancelReceipt(int receiptId, string? reason);

        Task<bool> DeleteAssociate(int associateId);
    }
}
