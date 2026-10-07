using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;
using PR.OpenGym.Data;
using PR.OpenGym.Data.DTOS;
using PR.OpenGym.Utilities.ExtensionMethods;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.UserInfo;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.FDILib;
using PR.OpenGym.Web.Models;
using PR.OpenGym.Web.Services;
using System.IO;

namespace PR.OpenGym.Web.Controllers
{
    [Authorize]
    public class AssociateController : Controller
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly IFaceTerminalService _faceOperations;

        public string draw = "";
        public string start = "";
        public string length = "";
        public string sortColumn = "";
        public string sortColumnDir = "";
        public string searchValue = "";
        public int pageSize, skip, recordsTotal;

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            ViewBag.Current = "Associate";
        }

        public AssociateController(IApiService apiService, IMapper mapper, IFaceTerminalService faceOperations)
        {
            _apiService = apiService;
            _mapper = mapper;
            _faceOperations = faceOperations;
        }
        public async Task<IActionResult> Index()
        {
            SearchRequest searchRequest = new SearchRequest();


            var associates = await _apiService.GetAssociates();

            //List<TerminalUserViewModel> listTerminalUsers = new List<TerminalUserViewModel>();
            //var isFaceTerminalConnected = await _faceOperations.TestConnection();
            ViewBag.IsFaceTerminalConnected = await _faceOperations.TestConnection();
            //if (isFaceTerminalConnected)
            //{
            //    try
            //    {
            //        SearchResponse searchResponse = await _faceOperations.SearchUsers(searchRequest);
            //        do
            //        {
            //            FillRecordsTerminalUserToViewModel(searchResponse.UserInfoSearch.UserInfo, listTerminalUsers);
            //            searchRequest.UserInfoSearchCond.SearchResultPosition += 30;
            //            searchResponse = await _faceOperations.SearchUsers(searchRequest);
            //        } while (searchResponse.UserInfoSearch.ResponseStatusStrg == "MORE");

            //        FillRecordsTerminalUserToViewModel(searchResponse.UserInfoSearch.UserInfo, listTerminalUsers);

            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}
            return View(associates);
        }

        [HttpPost]
        public async Task<IActionResult> MyJson()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            pageSize = length != null ? int.Parse(length) : 0;
            skip = start != null ? int.Parse(start) : 0;

            SearchRequest searchRequest = new SearchRequest();
            searchRequest.UserInfoSearchCond.SearchResultPosition += skip;
            SearchResponse searchResponse = await _faceOperations.SearchUsers(searchRequest);

            var recordsTotal = searchResponse.UserInfoSearch.TotalMatches;
            var data = searchResponse.UserInfoSearch.UserInfo;
            if (data != null && searchValue != "")
            {
                data = data.Where(p => p.Name.Contains(searchValue)).ToArray();
            }
            recordsTotal = recordsTotal;

            return Json(new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = data
            });
        }
        public async Task<IActionResult> Create()
        {
            var memberships = await _apiService.GetProducts(true);
            return View(new AssociateAndDetailsViewModel()
            {
                IsFaceTerminalConnected = await _faceOperations.TestConnection(),
                Memberships = memberships.Take(50).ToList(),
                Associate = new AssociateViewModel()
                {
                    AssociateMembership = new AssociateMembershipViewModel()
                    {

                    }
                },
                AssociateDetails = new AssociateDetailsViewModel()

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssociateAndDetailsViewModel associateAndDetailsView, IFormFile img)
        {
            var assocateDTO = _mapper.Map<AssociatePostDTO>(associateAndDetailsView.Associate);

            if (assocateDTO.AssociateMembership != null)
                assocateDTO.AssociateMembership.MembershipId = associateAndDetailsView.MembershipId;
            // el alta cobra la primera membresia y genera su recibo
            assocateDTO.PaymentMethod = associateAndDetailsView.PaymentMethod;
            assocateDTO.IssuedBy = User.Identity?.Name;
            var associateResponse = await _apiService.PostAssociate(assocateDTO, null);

            if (associateResponse != null)
            {
                // a partir de aqui el socio y su pago ya existen: siempre se regresa al listado
                // para no duplicar el alta ni el cobro si se reenvia el formulario
                TempData["CreateActionAssociateName"] = associateAndDetailsView.Associate.FirstName;
                var receipts = await _apiService.GetReceiptsByAssociateId(associateResponse.Id);
                TempData["CreateActionReceiptId"] = receipts.FirstOrDefault()?.Id;

                associateAndDetailsView.AssociateDetails.AssociateId = associateResponse.Id;
                var associateDetail = _mapper.Map<AssociateDetails>(associateAndDetailsView.AssociateDetails);
                var associateDetilsResponse = await _apiService.PostAssociateDetails(associateDetail);

                RecordRequest? isFlowOK = null;
                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        if (img != null) img.CopyTo(ms);
                        associateAndDetailsView.Associate.Id = associateResponse.Id;
                        Membership membership = await _apiService.GetMembershipById(associateAndDetailsView.MembershipId);
                        isFlowOK = await _faceOperations.RegisterUserFlow(associateAndDetailsView.Associate, membership.Period, ms.ToArray());
                    }
                }
                catch (Exception)
                {
                    isFlowOK = null;
                }

                TempData["CreateActionResult"] = true;
                TempData["CreateActionTerminalError"] = isFlowOK == null || associateDetilsResponse == null;
                return RedirectToAction(nameof(Index));
            }

            var memberships = await _apiService.GetProducts(true);
            associateAndDetailsView.Memberships = memberships?.ToList();
            associateAndDetailsView.IsFaceTerminalConnected = await _faceOperations.TestConnection();
            return View(associateAndDetailsView);

            //if (ModelState.IsValid && img != null && img.Length > 0)
            //{

            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        img.CopyTo(ms);
            //        Membership membership = await _apiService.GetMembershipById(associateAndDetailsView.MembershipId);
            //        var isFlowOK = await _faceOperations.RegisterUserFlow(associateAndDetailsView.Associate, membership.Period, ms.ToArray());

            //        if (isFlowOK != null)
            //            return RedirectToAction(nameof(Index));

            //        return Ok();
            //    }
            //    return RedirectToAction(nameof(Index));

            //    var associate = _mapper.Map<Associate>(associateAndDetailsView.Associate);
            //    var associateDetails = _mapper.Map<AssociateDetails>(associateAndDetailsView.AssociateDetails);
            //    //associate = await _apiService.PostAssociate(associate, img);

            //    if (associate != null && associate.Id > 0)
            //    {
            //        associateDetails.AssociateId = associate.Id;
            //        await _apiService.PostAssociateDetails(associateDetails);
            //        //await _apiService.PostAssociateMembership(new AssociateMembership()
            //        //{
            //        //    AssociateId = associate.Id,
            //        //    MembershipId = associateAndDetailsView.MembershipId
            //        //});
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //else
            //{
            //    var memberships = await _apiService.GetProducts(true);
            //    associateAndDetailsView.Memberships = memberships?.ToList();
            //    associateAndDetailsView.IsFaceTerminalConnected = await _faceOperations.TestConnection();
            //    return View(associateAndDetailsView);
            //}
        }

        [HttpPost]
        public async Task<IActionResult> CreateVisit()
        {

            var base64_QR = await _faceOperations.CreateVisitFlow();
            return Json(new
            {
                IsSuccess = !string.IsNullOrWhiteSpace(base64_QR),
                Base64URIData = "data:image/png;base64," + base64_QR
            });
        }
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var memberships = await _apiService.GetProducts(true);
            var associate = await _apiService.GetAssociate(id);
            if (associate.Id == 0)
            {
                return RedirectToAction(nameof(Index));
            }
            var associateDetails = await _apiService.GetAssociateDetails(associate.Id);

            var associateViewModel = _mapper.Map<AssociateViewModel>(associate);
            var assocaiteDetailsViewModel = _mapper.Map<AssociateDetailsViewModel>(associateDetails);
            var currentImage = await _faceOperations.GetFaceTerminalImageFlow(associate.Id.ToString());
            return View(new AssociateAndDetailsViewModel()
            {
                Memberships = memberships.ToList(),
                Associate = associateViewModel,
                AssociateDetails = assocaiteDetailsViewModel,
                IsFaceTerminalConnected = await _faceOperations.TestConnection(),
                MembershipId = associateViewModel.AssociateMembership?.MembershipId ?? 1,
                ImageBase64URIData = "data:image/png;base64," + currentImage
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromRoute] int id, AssociateAndDetailsViewModel associateAndDetailsView, IFormFile img)
        {
            try
            {
                //### Actualizacion Imagen en Terminal
                if (img != null && id > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        img.CopyTo(ms);
                        FaceDataRecordRequest faceDataRecordRequest = new FaceDataRecordRequest()
                        {
                            FPID = id.ToString()
                        };
                        await _faceOperations.CreateFaceDataRecord(faceDataRecordRequest, ms.ToArray());
                    }

                }

                var associate = _mapper.Map<AssociatePostDTO>(associateAndDetailsView.Associate);
                var associateDetails = _mapper.Map<AssociateDetails>(associateAndDetailsView.AssociateDetails);
                associateDetails.AssociateId = id;
                associate.AssociateMembership.MembershipId = associateAndDetailsView.MembershipId;

                Membership membership = await _apiService.GetMembershipById(associateAndDetailsView.MembershipId);
                string name = $"{associateAndDetailsView.Associate.FirstName} {associateAndDetailsView.Associate.LastName}";
                bool wasSuccessUdateOnTerminal = await _faceOperations.ModifyNameStartTimeEndTimeFlow(id.ToString(), name, associateAndDetailsView.Associate.AssociateMembership.From.Value, membership.Period);
                if (wasSuccessUdateOnTerminal)
                {
                    await _apiService.UpdateAssociate(associateAndDetailsView.Associate.Id.Value, associate);
                    await _apiService.UpdateAssociateDetails(associateAndDetailsView.AssociateDetails.Id.Value, associateDetails);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorUpdate"] = true;
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">associateID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            //delete database
            if (id == 0)
                return Json(new { Result = "error" });

            bool isDeletedFromDB = await _apiService.DeleteAssociate(id);
            GenericResponse response = null;
            if (isDeletedFromDB) response = await _faceOperations.DeletePerson(id.ToString());


            //delete terminal
            return Json(new { Result = response != null ? "ok" : "error" });
        }

        [HttpGet("TakePhotoDevice")]
        public async Task<IActionResult> TakePhotoDevice()
        {
            var base64 = await _faceOperations.TakeDeviceFace();
            return Json(new
            {
                IsSuccess = !string.IsNullOrWhiteSpace(base64),
                Base64URIData = "data:image/jpeg;base64," + base64
            });
        }

        [HttpGet("GetPhotoDevice")]
        public async Task<IActionResult> GetPhotoDevice([FromQuery] string faceUserID)
        {
            var base64 = await _faceOperations.GetFaceTerminalImageFlow(faceUserID);
            return Json(new
            {
                IsSuccess = !string.IsNullOrWhiteSpace(base64),
                Base64URIData = "data:image/jpeg;base64," + base64
            });
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetAssociatesByName(string name)
        {
            var associates = await _apiService.GetAssociatesByName(name);
            return Json(associates);
        }

        private void FillRecordsTerminalUserToViewModel(Userinfo[] users, List<TerminalUserViewModel> list)
        {
            if (users != null)

                list.AddRange(users.Select(e =>
                {
                    TerminalUserViewModel terminalUserView = new TerminalUserViewModel();
                    terminalUserView.Id = e.EmployeeNo;
                    terminalUserView.Name = e.Name;
                    return terminalUserView;
                }));
        }
    }
}
