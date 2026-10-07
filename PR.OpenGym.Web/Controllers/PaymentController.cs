using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PR.OpenGym.Data;
using PR.OpenGym.Data.DTOS;
using PR.OpenGym.Web.Models;
using PR.OpenGym.Web.Services;

namespace PR.OpenGym.Web.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IFaceTerminalService _faceOperations;
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;


        public PaymentController(IApiService apiService, IMapper mapper, IFaceTerminalService faceOperations)
        {
            _apiService = apiService;
            _mapper = mapper;
            _faceOperations = faceOperations;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            ViewBag.Current = "Payment";
        }
        public async Task<IActionResult> Index()
        {
            var memberships = await _apiService.GetProducts(true);
            PaymentViweModel paymentViweModel = new PaymentViweModel();
            paymentViweModel.Memberships = memberships.ToList();
            return View(paymentViweModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDTO createPaymentDTO)
        {
            bool wasSuccessUdateOnTerminal = false;
            createPaymentDTO.IssuedBy = User.Identity?.Name;
            PaymentResultDTO? paymentResult = await _apiService.CreatePayment(createPaymentDTO);
            if (paymentResult != null)
            {
                try
                {
                    Membership membership = await _apiService.GetMembershipById(createPaymentDTO.MembershipId);
                    wasSuccessUdateOnTerminal = await _faceOperations.ModifyNameStartTimeEndTimeFlow(createPaymentDTO.AssociateId.ToString(), string.Empty, createPaymentDTO.StartDate, membership.Period);
                }
                catch (Exception)
                {
                    // el pago ya quedo registrado, solo se informa que la terminal no se actualizo
                    wasSuccessUdateOnTerminal = false;
                }
            }
            return Json(new
            {
                Success = paymentResult != null,
                TerminalUpdated = wasSuccessUdateOnTerminal,
                ReceiptId = paymentResult?.ReceiptId,
                Folio = paymentResult?.Folio
            });
        }


        [HttpGet("Payment/GetPaymentsByAssociateId/{id}")]
        public async Task<IActionResult> GetPayments(int id)
        {
            IEnumerable<Payment> payments = await  _apiService.GetPaymentsByAssociateId(id);
            return Json(payments);
        }

    }
}
