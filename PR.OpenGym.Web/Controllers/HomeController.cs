using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PR.OpenGym.Utilities.ExtensionMethods;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.AcsEvent;
using PR.OpenGym.Web.Models;
using PR.OpenGym.Web.Services;
using System.Diagnostics;

namespace PR.OpenGym.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiService _apiService;
        private readonly IFaceTerminalService _faceOperations;
        public HomeController(ILogger<HomeController> logger, IApiService apiService, IFaceTerminalService faceOperations)
        {
            _logger = logger;
            _apiService = apiService;
            _faceOperations = faceOperations;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Current = "Home";
            //HomeViewModel viewModel = new HomeViewModel();  
            //var checkins = await _apiService.GetCheckIns();
            //var payments = (await _apiService.GetTodayPayments()).Sum(e=>e.Amount);
            //viewModel.CheckIns = checkins;
            //viewModel.PaymentsTotal = 3678.99m;
            var isFaceTerminalConnected = await _faceOperations.TestConnection();
            ViewBag.IsFaceTerminalConnected = isFaceTerminalConnected;
            IEnumerable<IGrouping<string, FaceTerminalCheckInsViewModel>> group = new List<IGrouping<string, FaceTerminalCheckInsViewModel>>();
            if (isFaceTerminalConnected)
            {
                List<FaceTerminalCheckInsViewModel> faceTerminalCheckInsViewModel = new List<FaceTerminalCheckInsViewModel>();
                await AddFaceEvents(faceTerminalCheckInsViewModel);
                await AddCardEvents(faceTerminalCheckInsViewModel);
                await AddFaceEventsExpired(faceTerminalCheckInsViewModel);
                group = faceTerminalCheckInsViewModel.GroupBy(el => el.Name);
            }
            return View(group);

        }

        [HttpGet]
        public async Task<IActionResult> GetDetailsMembership([FromQuery] int associateId)
        {
            var memberShip = await _apiService.GetAssociateMembership(associateId);
            return Json(memberShip);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// FaceEvent significa aquellas accesos detectados por medio de rostro
        /// </summary>
        /// <param name="modelCheckins"></param>
        /// <returns></returns>
        private async Task AddFaceEvents(List<FaceTerminalCheckInsViewModel> modelCheckins)
        {
            AcsEventRequest acsEventRequest = new AcsEventRequest();
            //esto 2 parametros indican son los eventos de autenticacion por rostro
            acsEventRequest.AcsEventCond.Major = 5;
            acsEventRequest.AcsEventCond.Minor = 75;
            await AddCheckIns(modelCheckins, acsEventRequest);
        }

        /// <summary>
        /// FaceEvent significa aquellas accesos detectados por medio de rostro
        /// </summary>
        /// <param name="modelCheckins"></param>
        /// <returns></returns>
        private async Task AddFaceEventsExpired(List<FaceTerminalCheckInsViewModel> modelCheckins)
        {
            AcsEventRequest acsEventRequest = new AcsEventRequest();
            //esto 2 parametros indican son los eventos de autenticacion por rostro
            acsEventRequest.AcsEventCond.Major = 5;
            acsEventRequest.AcsEventCond.Minor = 08;
            await AddCheckIns(modelCheckins, acsEventRequest,true);
        }

        /// <summary>
        /// CardEvent determina los accesos con tarjeta en este casos por codigo QR
        /// </summary>
        /// <param name="modelCheckins"></param>
        /// <returns></returns>
        private async Task AddCardEvents(List<FaceTerminalCheckInsViewModel> modelCheckins)
        {
            AcsEventRequest acsEventRequest = new AcsEventRequest();
            //estos 2 parametros indican que son los eventos exitosos de autenticacaion para Tarjeta (QR)
            acsEventRequest.AcsEventCond.Major = 5;
            acsEventRequest.AcsEventCond.Minor = 1;
            await AddCheckIns(modelCheckins, acsEventRequest);
        }
        private async Task AddCheckIns(List<FaceTerminalCheckInsViewModel> modelCheckins, AcsEventRequest acsEventRequest, bool isExpired = false)
        {
            //ejemplo formato
            //acsEventRequest.AcsEventCond.StartTime = "2023-10-07T00:00:00-06:00";
            //acsEventRequest.AcsEventCond.EndTime = "2023-10-07T23:59:59-06:00";
            var now = DateTime.Now.ConvertDateToMexicoCentralLocalZone();
            acsEventRequest.AcsEventCond.StartTime = now.ToString("yyyy-MM-ddT00:00:00-06:00");
            acsEventRequest.AcsEventCond.EndTime = now.ToString("yyyy-MM-ddT23:59:59-06:00");

            AcsEventResponse acsEventRespons;
            do
            {
                acsEventRespons = await _faceOperations.GetEventLogs(acsEventRequest);
                acsEventRequest.AcsEventCond.SearchResultPosition += 30;
                AddACSEventosToModel(modelCheckins, acsEventRespons,isExpired);
            } while (acsEventRespons != null && acsEventRespons.AcsEvent.ResponseStatusStrg == "MORE");
            //agregar el sobrante
            //AddACSEventosToModel(modelCheckins, acsEventRespons);

        }
        private void AddACSEventosToModel(List<FaceTerminalCheckInsViewModel> modelCheckins, AcsEventResponse acsEventRespons, bool isExpired = false)
        {
            if (acsEventRespons != null && acsEventRespons.AcsEvent.InfoList != null)
            {
                modelCheckins.AddRange(acsEventRespons.AcsEvent.InfoList.Select(el => new FaceTerminalCheckInsViewModel()
                {
                    Id = el.EmployeeNoString,
                    Name = el.Name,
                    Date = el.Time.ToString("hh:mm:ss tt"),
                    IsExpired = isExpired
                }));
            }
        }

    }
}