using Microsoft.AspNetCore.Mvc;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.Data.DTOS;
using PR.OpenGym.Utilities.ExtensionMethods;

namespace PR.OpenGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptService _receiptService;

        public ReceiptController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        // GET api/Receipt?from=2026-09-01&to=2026-09-30
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var today = DateTime.Now.ConvertDateToMexicoCentralLocalZone().Date;
            var start = (from ?? today).Date;
            var end = (to ?? start).Date.AddDays(1);
            var receipts = await _receiptService.GetByDateRangeAsync(start, end);
            return Ok(receipts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var receipt = await _receiptService.GetAsync(id);
            if (receipt == null)
                return NotFound();
            return Ok(receipt);
        }

        [HttpGet("ByAssociate/{associateId}")]
        public async Task<IActionResult> GetByAssociate(int associateId)
        {
            var receipts = await _receiptService.GetByAssociateIdAsync(associateId);
            return Ok(receipts);
        }

        // Los recibos no se eliminan, solo se cancelan
        [HttpPut("{id}/Cancel")]
        public async Task<IActionResult> Cancel(int id, [FromBody] CancelReceiptDTO cancelReceiptDTO)
        {
            var result = await _receiptService.CancelAsync(id, cancelReceiptDTO?.Reason);
            return result ? NoContent() : BadRequest("No se pudo cancelar el recibo");
        }
    }
}
