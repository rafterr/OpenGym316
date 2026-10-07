using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.Data;

namespace PR.OpenGym.API.Service
{
    public class ReceiptService : GenericService<Receipt>, IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IBranchRepository _branchRepository;

        public ReceiptService(IReceiptRepository receiptRepository, IBranchRepository branchRepository) : base(receiptRepository)
        {
            _receiptRepository = receiptRepository;
            _branchRepository = branchRepository;
        }

        public async Task<Receipt> CreateForPaymentAsync(Payment payment, Associate associate, Membership membership, AssociateMembership associateMembership, string? issuedBy)
        {
            Branch? branch = associate.Branch;
            if (branch == null && associate.BranchId != null)
                branch = await _branchRepository.GetAsync(associate.BranchId.Value);

            var receipt = new Receipt()
            {
                PaymentId = payment.Id,
                AssociateId = associate.Id,
                AssociateName = $"{associate.FirstName} {associate.LastName}".Trim(),
                MembershipName = membership.Name,
                PeriodFrom = associateMembership.From ?? payment.CreatedOn,
                PeriodTo = associateMembership.To ?? payment.CreatedOn,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                BranchName = branch?.Name,
                BranchAddress = branch?.Address,
                IssuedBy = issuedBy,
                Status = ReceiptStatus.Active
            };
            await PostAsync(receipt);
            return receipt;
        }

        public Task<IEnumerable<Receipt>> GetByAssociateIdAsync(int associateId)
        {
            return _receiptRepository.GetByAssociateIdAsync(associateId);
        }

        public Task<IEnumerable<Receipt>> GetByDateRangeAsync(DateTime from, DateTime to)
        {
            return _receiptRepository.GetByDateRangeAsync(from, to);
        }

        public async Task<bool> CancelAsync(int receiptId, string? reason)
        {
            var receipt = await _receiptRepository.GetAsync(receiptId);
            if (receipt == null || receipt.Status == ReceiptStatus.Cancelled)
                return false;

            receipt.Status = ReceiptStatus.Cancelled;
            receipt.CancelReason = reason;
            return await PutAsync(receipt);
        }
    }
}
