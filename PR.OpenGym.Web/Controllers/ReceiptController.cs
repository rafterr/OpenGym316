using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PR.OpenGym.Utilities.ExtensionMethods;
using PR.OpenGym.Web.Models;
using PR.OpenGym.Web.Services;

namespace PR.OpenGym.Web.Controllers
{
    [Authorize]
    public class ReceiptController : Controller
    {
        private readonly IApiService _apiService;

        public ReceiptController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            ViewBag.Current = "Receipt";
        }

        public async Task<IActionResult> Index([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var today = DateTime.Now.ConvertDateToMexicoCentralLocalZone().Date;
            var model = new ReceiptListViewModel()
            {
                From = (from ?? today).Date,
                To = (to ?? from ?? today).Date
            };
            model.Receipts = (await _apiService.GetReceipts(model.From, model.To)).ToList();
            return View(model);
        }

        /// <summary>
        /// Ticket de 80mm listo para imprimir
        /// </summary>
        public async Task<IActionResult> Print(int id, [FromQuery] bool auto = false)
        {
            var receipt = await _apiService.GetReceipt(id);
            if (receipt == null)
                return NotFound();

            ViewBag.AutoPrint = auto;
            return View(receipt);
        }

        [HttpGet("Receipt/ByAssociate/{associateId}")]
        public async Task<IActionResult> ByAssociate(int associateId)
        {
            var receipts = await _apiService.GetReceiptsByAssociateId(associateId);
            return Json(receipts.Select(r => new
            {
                r.Id,
                r.PaymentId,
                r.Folio,
                r.CreatedOn,
                r.Amount,
                r.MembershipName,
                r.PaymentMethod,
                r.Status
            }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id, string? reason)
        {
            var result = await _apiService.CancelReceipt(id, reason);
            TempData["ReceiptCancelResult"] = result;
            return RedirectToAction(nameof(Print), new { id });
        }
    }
}
