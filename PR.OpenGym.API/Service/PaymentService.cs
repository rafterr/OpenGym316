using Microsoft.VisualBasic;
using PR.OpenGym.API.Contracts.Repositories;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.Data;
using PR.OpenGym.Data.DTOS;

namespace PR.OpenGym.API.Service
{
    public class PaymentService : GenericService<Payment>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IAssociateService _associateService;


        public PaymentService(IPaymentRepository paymentRepository, IAssociateService associateService) : base(paymentRepository)
        {
            _paymentRepository = paymentRepository;
            _associateService = associateService;
        }

        public Task<IEnumerable<Payment>> GetTodayPayments()
        {
            return _paymentRepository.GetTodayPayments();
        }

        public async Task<bool> PayAssociateMembership(CreatePaymentDTO createPaymentDTO)
        {
            var membershipAssociate = await _associateService.GetAssociateMembershipByAssociateIdAsync(createPaymentDTO.AssociateId);
            if (membershipAssociate != null)
            {
                decimal? price = membershipAssociate.Membership?.Price ?? 0;
                AssociateMembership associateMembership = await _associateService.GetAssociateMembershipByAssociateIdAsync(createPaymentDTO.AssociateId);
                if (associateMembership != null) {
                    associateMembership.MembershipId = createPaymentDTO.MembershipId;
                    associateMembership.From = createPaymentDTO.StartDate;
                    await _associateService.UpdateAssociateMembershipAsync(associateMembership);
                }


                Payment payment = new Payment()
                {
                    Amount = price.Value,
                    AssociateId = createPaymentDTO.AssociateId,
                    Concept = "Pago Membresia",
                    ProductId = membershipAssociate.MembershipId,
                };

                await this.PostAsync(payment);
                return payment.Id != 0;
            }
            return false;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByAssociateId(int associateId) 
        {
            return await _paymentRepository.GetPaymentsByAssociateId(associateId);
        }
    }
}
