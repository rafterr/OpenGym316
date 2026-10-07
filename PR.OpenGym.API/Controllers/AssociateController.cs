using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PR.OpenGym.API.Contracts.Services;
using PR.OpenGym.API.ViewModel;
using PR.OpenGym.Data;
using PR.OpenGym.Data.DTOS;

namespace PR.OpenGym.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssociateController : ControllerBase
    {
        private readonly IAssociateService _associateService;
        private readonly IAssociateDetailsService _associateDetailsService;
        private readonly IImageStorageService _imageStorageService;
        private readonly IMapper _mapper;

        public AssociateController(
            IAssociateService associateService,
            IImageStorageService imageStorageService,
            IMapper mapper
            )
        {
            _associateService = associateService;
            _imageStorageService = imageStorageService;
            _mapper = mapper;
        }

        // GET: api/Associate
        [HttpGet]
        public async Task<IActionResult> GetAssociates()
        {
            var associates = await _associateService.GetAllAsync();
            return Ok(associates);
        }

        [HttpGet("Search/{associateName}")]
        public async Task<IActionResult> GetAssociates(string associateName)
        {
            var associates = await _associateService.GetByNameAsync(associateName);
            return Ok(associates);
        }

        // GET: api/Associate/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAssociate(int id)
        {
            var associate = await _associateService.GetAsync(id);
            if (associate == null)
            {
                return NotFound();
            }
            return Ok(associate);
        }

        // PUT: api/Associate/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssociate(int id, AssociatePostDTO associateDTO)
        {
            Associate associate1 = null;

            try
            {
                var associate = await _associateService.GetAsync(id);
                if (associate == null) return NotFound();

                associate1 = await _associateService.PutAsync(id, associateDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return associate1 != null ? NoContent() : BadRequest();
        }

        // POST: api/Associate
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Associate>> PostAssociate([FromForm] AssociatePostDTO associateDTO)
        {
            if (

                //model.ImgFile != null && model.ImgFile.Length > 0 && (
                // model.ImgFile.FileName.EndsWith("jpeg", StringComparison.InvariantCultureIgnoreCase)
                //||
                // model.ImgFile.FileName.EndsWith("jpg", StringComparison.InvariantCultureIgnoreCase)
                //||
                // model.ImgFile.FileName.EndsWith("png", StringComparison.InvariantCultureIgnoreCase)
                //)
                true

                )
            {
                var associate = await _associateService.PostAsync(associateDTO);
                if (associate.Id > 0)
                {
                    //var imgPath = await _imageStorageService.SaveImageDocumentManagement(associate.Id, model.ImgFile);
                    //associate.ImgPath = imgPath;
                    //await _associateService.PutAsync(associate);
                    return Ok(associate);
                }
                return Problem("An error ocurred trying to Post Associate in controller PostAssociate");
            }
            else
            {
                return BadRequest(new
                {
                    message = "imgFile [null || wrong format]"
                });
            }
        }

        // DELETE: api/Associate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssociate(int id)
        {
            var associate = await _associateService.GetAsync(id);
            if (associate == null)
            {
                return NotFound();
            }
            var result = await _associateService.DeleteAsync(id);
            return result ? NoContent() : BadRequest();
        }

        [HttpGet("associatemembership")]
        public async Task<IActionResult> GetAssociateMembershipByAssociate(int asscoiateId)
        {
            var associate = await _associateService.GetAssociateMembershipByAssociateIdAsync(asscoiateId);
            if (associate == null)
            {
                return NotFound();
            }
            return Ok(associate);
        }

        [HttpPost("associatemembership")]
        public async Task<IActionResult> CreateAssociateMembership(AssociateMembership associateMembership)
        {
            try
            {
                await _associateService.CreateAssociateMembershipAsync(associateMembership);
                return Ok(associateMembership);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("associatemembership")]
        public async Task<IActionResult> UpdateAssociateMembership(AssociateMembershipDTO associateMembershipDto)
        {
            try
            {
                var associateMembership = _mapper.Map<AssociateMembership>(associateMembershipDto);
               // await _associateService.UpdateAssociateMembershipAsync(associateMembershipId, associateMembership, startDate);
                return Ok(associateMembership);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpPost("checkin/{associateId}")]
        public async Task<ActionResult> CheckIn(int associateId)
        {
            if (associateId == 0)
                return BadRequest("id invalido");

            await _associateService.PostCheckIn(associateId);
            return NoContent();
        }

        [HttpGet("checkin")]
        public async Task<ActionResult> GetCheckIns([FromQuery] int id, [FromQuery] DateTime dateTime)
        {

            IEnumerable<CheckIn> checkIns;
            if (id > 0)
                checkIns = await _associateService.GetAllCheckInsByAssociate(id);
            else
                checkIns = await _associateService.GetAllCheckInsByDate(dateTime);

            return Ok(checkIns);
        }

    }
}
