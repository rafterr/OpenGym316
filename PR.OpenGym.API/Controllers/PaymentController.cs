using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.Data;
using PR.OpenGym.Data.DTOS;

namespace PR.OpenGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService branchService)
        {
            _paymentService = branchService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _paymentService.GetAllAsync();
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _paymentService.GetAsync(id);
            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post(Payment payment)
        {
            if (ModelState.IsValid)
            {
                await _paymentService.PostAsync(payment);
                return Ok(payment);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("PayMembership")]
        public async Task<IActionResult> PayMembership([FromBody]CreatePaymentDTO createPaymentDTO)
        {
            try
            {
                if (createPaymentDTO.AssociateId == 0)
                    return BadRequest();

                bool res = await _paymentService.PayAssociateMembership(createPaymentDTO);
                if (res)
                    return Ok();
                else
                    return BadRequest("No se pudo compretar la operacion");
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("TodayPayments")]
        public async Task<IActionResult> GetTodayPayments()
        {
            try
            {
                var payments = await _paymentService.GetTodayPayments();
                return Ok(payments);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("PaymentsByAssociate/{associateId}")]
        public async Task<IActionResult> GetTodayPayments(int associateId)
        {
            try
            {
                var payments = await _paymentService.GetPaymentsByAssociateId(associateId);
                return Ok(payments);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Payment payment)
        {
            if (ModelState.IsValid && id == payment.Id)
            {
                await _paymentService.PutAsync(payment);
                return Ok(payment);
            }
            else
            {
                if (id != payment.Id)
                    return NotFound();

                return BadRequest(ModelState);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _paymentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
