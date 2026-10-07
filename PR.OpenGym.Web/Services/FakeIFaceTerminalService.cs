using PR.OpenGym.Utilities.ExtensionMethods;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.AcsEvent;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.CardInfo;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.UserInfo;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.FDILib;
using PR.OpenGym.Web.Models;
using QRCoder;
using System.Buffers.Text;
using System.Drawing;
using System.Text;
using System.Text.Json;

namespace PR.OpenGym.Web.Services
{
    public class FakeIFaceTerminalService : IFaceTerminalService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOpt;
        private string _fileFaceCount = "FaceCount.txt";

        public FakeIFaceTerminalService(HttpClient client)
        {
            _client = client;
            _jsonOpt = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            if (!File.Exists(_fileFaceCount))
            {
                var c = GetUserCount().Result;
                File.WriteAllText(_fileFaceCount, (c == -1 ? "1" : c.ToString()));
            }
        }

        public Task<GenericResponse> CreateCardRecord(CIRecordRequest cIRecordRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<FaceDataRecordResponse> CreateFaceDataRecord(FaceDataRecordRequest faceDataRecordRequest, byte[] image)
        {
            File.WriteAllBytes(faceDataRecordRequest.FPID + ".jpeg", image);
            return new FaceDataRecordResponse();
        }

        public Task<FDLibResponse> CreateFDLib(FDLibRequest fDLibRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<RecordResponse> CreateUserInfoRecord(RecordRequest userInfoRequest)
        {
            var response = new RecordResponse();
            return response;
        }

        public async Task<RecordResponse> ModifyUserInfoRecord(RecordRequest userInfoRequest)
        {
            var response = new RecordResponse();
            return response;
        }

        public async Task<string> CreateVisitFlow()
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            return Convert.ToBase64String(qrCode.GetGraphic(20));
        }

        public async Task<string> GetFaceTerminalImageFlow(string faceTerminalID)
        {

            var imgPath = $"{faceTerminalID}.jpeg";
            if (File.Exists(imgPath))
                return Convert.ToBase64String(File.ReadAllBytes(imgPath));
            return null;
        }

        public async Task<int> GetUserCount()
        {
            return new Random().Next(1, 20);
        }

        public async Task<RecordRequest> RegisterUserFlow(AssociateViewModel associateViewModel, int numberOfDays, byte[] image)
        {
            RecordRequest recordRequest = new RecordRequest();
            recordRequest.UserInfo.EmployeeNo = GetUserCount().ToString();
            return recordRequest;
        }

        public async Task<SearchResponse> SearchUsers(SearchRequest searchRequest)
        {
            SearchResponse searchResponse = new SearchResponse();
            Userinfo[] info = new Userinfo[]{
                   new Userinfo(){
                EmployeeNo = "1",
                Gender = "unknown",
                UserType = "normal",
                Name = "admin"
                },
                new Userinfo(){
                EmployeeNo = "2",
                Gender = "Male",
                UserType = "normal",
                Name = "Pablo Roldan"
                }
            };
            Userinfosearch userinfosearch = new Userinfosearch();
            userinfosearch.UserInfo = info;
            userinfosearch.ResponseStatusStrg = "OK";
            searchResponse.UserInfoSearch = userinfosearch;
            return searchResponse;
        }

        public async Task<string> TakeDeviceFace()
        {
            var imgPath = "C:\\img\\rodri.jpg";
            if (File.Exists(imgPath))
                return Convert.ToBase64String(File.ReadAllBytes(imgPath));
            return null;
        }

        public async Task<bool> TestConnection()
        {
            return true;
        }

        public async Task<AcsEventResponse> GetEventLogs(AcsEventRequest acsEventRequest)
        {
            AcsEventResponse n = new AcsEventResponse();
            n.AcsEvent.InfoList = new Infolist[] {
                new Infolist()
                {
                    EmployeeNoString = "3",
                    Name = "Joe Matio",
                }
            };
            return n;
        }

        public async Task<bool> ModifyNameStartTimeEndTimeFlow(string FPID, string fullname, DateTime from, int numberOfDays)
        {
            return true;
        }

        public async Task<GenericResponse> DeletePerson(string FPID)
        {
            GenericResponse genericResponse = new GenericResponse();
            return genericResponse;
        }
    }
}
