using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.API.Data;
using PR.OpenGym.Data;
using PR.OpenGym.Data.DTOS;

namespace PR.OpenGym.API.Service
{
    public class PaymentService : GenericService<Payment>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IAssociateService _associateService;
        private readonly IAssociateRepository _associateRepository;
        private readonly IProductRepository _productRepository;
        private readonly IReceiptService _receiptService;
        private readonly PROpenGymWebContext _dbContext;

        public PaymentService(
            IPaymentRepository paymentRepository,
            IAssociateService associateService,
            IAssociateRepository associateRepository,
            IProductRepository productRepository,
            IReceiptService receiptService,
            PROpenGymWebContext dbContext) : base(paymentRepository)
        {
            _paymentRepository = paymentRepository;
            _associateService = associateService;
            _associateRepository = associateRepository;
            _productRepository = productRepository;
            _receiptService = receiptService;
            _dbContext = dbContext;
        }

        public Task<IEnumerable<Payment>> GetTodayPayments()
        {
            return _paymentRepository.GetTodayPayments();
        }

        public async Task<PaymentResultDTO?> PayAssociateMembership(CreatePaymentDTO createPaymentDTO)
        {
            Membership membership = await _productRepository.GetMembeship(createPaymentDTO.MembershipId);
            if (membership == null)
                return null;

            var associate = await _associateRepository.GetWithMembershipAndBranchAsync(createPaymentDTO.AssociateId);
            var associateMembership = associate?.AssociateMembership;
            if (associate == null || associateMembership == null)
                return null;

            // membresia, pago y recibo se guardan juntos o no se guarda nada
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            associateMembership.MembershipId = membership.Id;
            associateMembership.Membership = membership;
            associateMembership.From = createPaymentDTO.StartDate;
            await _associateService.UpdateAssociateMembershipAsync(associateMembership);

            var result = await RegisterPaymentAsync(associate, membership, associateMembership, createPaymentDTO.PaymentMethod, createPaymentDTO.IssuedBy);

            await transaction.CommitAsync();
            return result;
        }

        public async Task<Associate> CreateAssociateWithInitialPaymentAsync(AssociatePostDTO associatePostDTO)
        {
            if (associatePostDTO.AssociateMembership == null || associatePostDTO.PaymentMethod == null)
                throw new ArgumentException("La membresia y el metodo de pago son requeridos para el cobro inicial");

            Membership membership = await _productRepository.GetMembeship(associatePostDTO.AssociateMembership.MembershipId);
            if (membership == null)
                throw new ArgumentException("Membership not found");

            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            var associate = await _associateService.PostAsync(associatePostDTO);
            await RegisterPaymentAsync(associate, membership, associate.AssociateMembership!, associatePostDTO.PaymentMethod.Value, associatePostDTO.IssuedBy);

            await transaction.CommitAsync();
            return associate;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByAssociateId(int associateId)
        {
            return await _paymentRepository.GetPaymentsByAssociateId(associateId);
        }

        private async Task<PaymentResultDTO> RegisterPaymentAsync(Associate associate, Membership membership, AssociateMembership associateMembership, PaymentMethod paymentMethod, string? issuedBy)
        {
            Payment payment = new Payment()
            {
                Amount = membership.Price,
                AssociateId = associate.Id,
                Concept = $"Pago Membresia {membership.Name}",
                ProductId = membership.Id,
                PaymentMethod = paymentMethod
            };
            await PostAsync(payment);

            var receipt = await _receiptService.CreateForPaymentAsync(payment, associate, membership, associateMembership, issuedBy);

            return new PaymentResultDTO()
            {
                PaymentId = payment.Id,
                ReceiptId = receipt.Id,
                Folio = receipt.Folio
            };
        }
    }
}
