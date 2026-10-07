using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PR.OpenGym.Data;
using PR.OpenGym.Web.Models;
using PR.OpenGym.Web.Services;

namespace PR.OpenGym.Web.Controllers
{
    [Authorize]
    public class MembershipController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            ViewBag.Current = "Membership";
        }

        public MembershipController(ILogger<HomeController> logger, IApiService apiService, IMapper mapper)
        {
            _logger = logger;
            _apiService = apiService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> membership = await _apiService.GetProducts(true);
            return View(membership);
        }


        public async Task<IActionResult> Upsert(int membershipId)
        {
            MembershipViewModel m = new MembershipViewModel();
            if (membershipId != 0)
            {
                m = _mapper.Map<MembershipViewModel>(await _apiService.GetMembershipById(membershipId));

            }
            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(MembershipViewModel model)
        {
            Membership membership = _mapper.Map<Membership>(model);

            if (!ModelState.IsValid)
                return View(model);

            if (model.Id == null)
            {
                await _apiService.PostMembership(membership);
            }
            else
            {
                await _apiService.UpdateMembership(membership);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int membershipId)
        {
            var result = await _apiService.DeleteMembership(membershipId);
            TempData["ResponseDelete"] = result;
            return RedirectToAction(nameof(Index));
        }

    }
}
