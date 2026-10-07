using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.Data;

namespace PR.OpenGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssociateDetailsController : ControllerBase
    {
        private readonly IAssociateDetailsService _associateDetailsService;

        public AssociateDetailsController(IAssociateDetailsService associateDetailsService)
        {
            _associateDetailsService = associateDetailsService;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _associateDetailsService.GetAllAsync();
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _associateDetailsService.GetAsync(id);
            return Ok(product);
        }

        [HttpGet("associate/{associateId}")]
        public async Task<IActionResult> GetAssociateDetailsByAssociateId(int associateId)
        {
            var associateDetails = await _associateDetailsService.GetAssociateDetailsByAssociateId(associateId);
            return Ok(associateDetails);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post(AssociateDetails associateDetails)
        {
            if (ModelState.IsValid)
            {
                await _associateDetailsService.PostAsync(associateDetails);
                return Ok(associateDetails);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AssociateDetails associateDetails)
        {
            if (ModelState.IsValid && id == associateDetails.Id)
            {
                await _associateDetailsService.PutAsync(associateDetails);
                return Ok(associateDetails);
            }
            else
            {
                if (id != associateDetails.Id)
                    return NotFound();

                return BadRequest(ModelState);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _associateDetailsService.DeleteAsync(id);
            return NoContent();
        }
    }
}
