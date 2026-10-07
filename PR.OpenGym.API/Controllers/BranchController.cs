using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.Data;

namespace PR.OpenGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _branchService.GetAllAsync();
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _branchService.GetAsync(id);
            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post(Branch branch)
        {
            if (ModelState.IsValid)
            {
                await _branchService.PostAsync(branch);
                return Ok(branch);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Branch branch)
        {
            if (ModelState.IsValid && id == branch.Id)
            {
                await _branchService.PutAsync(branch);
                return Ok(branch);
            }
            else
            {
                if (id != branch.Id)
                    return NotFound();

                return BadRequest(ModelState);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _branchService.DeleteAsync(id);
            return NoContent();
        }
    }
}
