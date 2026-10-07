using Microsoft.AspNetCore.Mvc;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PR.OpenGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: api/<ProductController>
        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] bool isMembership = false)
        {
            var products = await _productService.GetProductsAsync(isMembership);
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetAsync(id);
            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.PostAsync(product);
                return Ok(product);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("membership/{membershipId}")]
        public async Task<IActionResult> GetMembershipById(int membershipId)
        {

            var membership = await _productService.GetMembershipByIdAsync(membershipId);
            return Ok(membership);


        }

        // POST api/<ProductController>
        [HttpPost("membership")]
        public async Task<IActionResult> PostMembership(Membership membership)
        {
            if (ModelState.IsValid)
            {
                await _productService.PostAsync(membership);
                return Ok(membership);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("membership")]
        public async Task<IActionResult> PutMembership(Membership membership)
        {
            if (ModelState.IsValid)
            {
                await _productService.PutAsync(membership);
                return Ok(membership);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Product product)
        {
            if (ModelState.IsValid && id == product.Id)
            {
                await _productService.PutAsync(product);
                return Ok(product);
            }
            else
            {
                if (id != product.Id)
                    return NotFound();

                return BadRequest(ModelState);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
