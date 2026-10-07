using Humanizer;
using PR.OpenGym.Data;
using PR.OpenGym.Utilities.ExtensionMethods;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.AcsEvent;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.CardInfo;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.UserInfo;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.FDILib;
using PR.OpenGym.Web.Models;
using QRCoder;
using System;
using System.Drawing;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace PR.OpenGym.Web.Services
{
    public class FaceTerminalService : IFaceTerminalService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOpt;
        public FaceTerminalService(HttpClient client)
        {
            _client = client;
            _jsonOpt = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
        }
        public async Task<int> GetUserCount()
        {
            int? res = -1;
            string resource = "/ISAPI/AccessControl/UserInfo/Count?format=json";
            var result = await _client.GetAsync(resource);
            if (result.IsSuccessStatusCode)
            {
                var responseBody = JsonSerializer.Deserialize<UserInfoCountResponse>(await result.Content.ReadAsStringAsync(), _jsonOpt);
                res = responseBody?.UserInfoCount?.UserNumber;
            }
            return res.Value;
        }
        public async Task<RecordRequest> RegisterUserFlow(AssociateViewModel associateViewModel, int numberOfDays, byte[] image)
        {
            //int faceId = GetNextUserId();
            int faceId = associateViewModel.Id.Value;
            if (faceId != -1)
            {
                string FPID = faceId.ToString();

                string name = $"{associateViewModel.FirstName} {associateViewModel.LastName}";
                string gender = "unknown";
                DateTime now = DateTime.Now.ConvertDateToMexicoCentralLocalZone();
                DateTime from = associateViewModel.AssociateMembership.From ?? now;
                from = from.AddTicks(now.TimeOfDay.Ticks);
                DateTime to;
                if (numberOfDays == 30)
                    to = from.AddMonths(1);
                else if (numberOfDays == 365)
                    to = from.AddYears(1);
                else
                    to = from.AddDays(numberOfDays);

                if (associateViewModel.Gender != null && associateViewModel.Gender.Value != Gender.Other)
                    gender = associateViewModel.Gender.ToString().ToLower();

                RecordRequest userInfoRequest = CreateRecordRequest(FPID, name, "normal", from, to, gender);

                var responseUserRecord = await CreateUserInfoRecord(userInfoRequest);
                if (responseUserRecord != null)
                {
                    //File.WriteAllText(_fileFaceCount, faceId.ToString());
                    //Por default se crea la libreria con FPID="1" de tipo  blackFD, no es necesario recrearla al agregar una imagen 
                    //FDLibRequest r = new FDLibRequest();
                    //r.Name = Guid.NewGuid().ToString();
                    //var responseFDLib = await CreateFDLib(r);
                    if (image.Length > 0)
                    {
                        var FaceDataRecordRequest = new FaceDataRecordRequest()
                        {
                            FPID = FPID
                        };
                        var faceDataRecordResponse = await CreateFaceDataRecord(FaceDataRecordRequest, image);
                        if (faceDataRecordResponse != null)
                        {
                            //detectar si no se registro la imagen
                            return userInfoRequest;
                        }
                    }
                    else
                    {
                        // no tiene imagen pero todo fue correcto
                        return userInfoRequest;
                    }
                }

            }
            return null;
        }
        public async Task<string> TakeDeviceFace()
        {
            string resource = "/ISAPI/AccessControl/CaptureFaceData";
            string request = "<CaptureFaceDataCond version=\"2.0\" xmlns=\"http://www.isapi.org/ver20/XMLSchema\">\r\n<captureInfrared>false</captureInfrared>\r\n<dataType>binary</dataType>\r\n</CaptureFaceDataCond>";
            StringContent stringContent = new StringContent(request);
            var result = await _client.PostAsync(resource, stringContent);
            if (result.IsSuccessStatusCode)
            {
                MultipartMemoryStreamProvider multipart = await result.Content.ReadAsMultipartAsync();
                foreach (var content in multipart.Contents)
                {
                    var bytes = await content.ReadAsByteArrayAsync();
                    var base64 = Convert.ToBase64String(bytes);
                    return base64;
                }

            }
            return null;
        }
        public async Task<bool> TestConnection()
        {
            var res = await _client.GetAsync("/ISAPI/Intelligent/FDLib/capabilities?format=json");
            return res.IsSuccessStatusCode;
        }
        public async Task<FDLibResponse> CreateFDLib(FDLibRequest fDLibRequest)
        {
            string resource = "/ISAPI/Intelligent/FDLib?format=json";
            StringContent stringContent = new StringContent(JsonSerializer.Serialize(fDLibRequest), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync(resource, stringContent);
            if (result.IsSuccessStatusCode)
            {
                var response = JsonSerializer.Deserialize<FDLibResponse>(await result.Content.ReadAsStringAsync(), _jsonOpt);
                return response;
            }
            return null;
        }
        public async Task<RecordResponse> CreateUserInfoRecord(RecordRequest userInfoRequest)
        {
            string resource = "/ISAPI/AccessControl/UserInfo/Record?format=json";
            StringContent stringContent = new StringContent(JsonSerializer.Serialize(userInfoRequest), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync(resource, stringContent);
            if (result.IsSuccessStatusCode)
            {
                var response = JsonSerializer.Deserialize<RecordResponse>(await result.Content.ReadAsStringAsync(), _jsonOpt);
                return response;
            }
            return null;
        }

        public async Task<RecordResponse> ModifyUserInfoRecord(RecordRequest userInfoRequest)
        {
            string resource = "/ISAPI/AccessControl/UserInfo/Modify?format=json";
            StringContent stringContent = new StringContent(JsonSerializer.Serialize(userInfoRequest), Encoding.UTF8, "application/json");
            var result = await _client.PutAsync(resource, stringContent);
            if (result.IsSuccessStatusCode)
            {
                var response = JsonSerializer.Deserialize<RecordResponse>(await result.Content.ReadAsStringAsync(), _jsonOpt);
                return response;
            }
            return null;
        }

        public async Task<FaceDataRecordResponse> CreateFaceDataRecord(FaceDataRecordRequest faceDataRecordRequest, byte[] image)
        {
            // este limite es como el que genera postman
            var boundary = "--------------------------397023958260752008831279";

            //string resource = "/ISAPI/Intelligent/FDLib/FDSetUp?format=json";

            //var request = new HttpRequestMessage(HttpMethod.Put, resource);

            string resource = "/ISAPI/Intelligent/FDLib/FaceDataRecord?format=json";

            var request = new HttpRequestMessage(HttpMethod.Post, resource);
            var content = new MultipartFormDataContent(boundary);
            var json = JsonSerializer.Serialize(faceDataRecordRequest);

            content.Add(new StringContent(json, Encoding.UTF8, "application/json"), "FaceDataRecord");

            using (var ms = new MemoryStream(image))
            {
                content.Add(new StringContent(json, Encoding.UTF8, "application/json"), "FaceDataRecord");
                var contentFile = new StreamContent(ms);
                //super importante que sea "image/jpeg" de no hacerlo no lo interpreta como imagen y truena
                contentFile.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                content.Add(contentFile, "FaceImage");
                // al parecer quitar y poner el tipo de dato funciona bien en las pruebas
                content.Headers.Remove("Content-Type");
                content.Headers.TryAddWithoutValidation("Content-Type", $"multipart/form-data; boundary={boundary}");

                request.Content = content;
                var response = await _client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<FaceDataRecordResponse>(await response.Content.ReadAsStringAsync(), _jsonOpt);
                }

            }
            return null;
        }
        public async Task<GenericResponse> CreateCardRecord(CIRecordRequest cIRecordRequest)
        {
            string resource = "/ISAPI/AccessControl/CardInfo/Record?format=json";
            StringContent stringContent = new StringContent(JsonSerializer.Serialize(cIRecordRequest), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync(resource, stringContent);
            if (result.IsSuccessStatusCode)
            {
                var response = JsonSerializer.Deserialize<GenericResponse>(await result.Content.ReadAsStringAsync(), _jsonOpt);
                return response;
            }
            return null;
        }

        public async Task<SearchResponse> SearchUsers(SearchRequest searchRequest)
        {
            string resource = "/ISAPI/AccessControl/UserInfo/Search?format=json";
            StringContent stringContent = new StringContent(JsonSerializer.Serialize(searchRequest), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync(resource, stringContent);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var response = JsonSerializer.Deserialize<SearchResponse>(content, _jsonOpt);
                return response;
            }
            return null;
        }
        public async Task<string> GetFaceTerminalImageFlow(string faceTerminalID)
        {
            string bytesEncodedBase64 = null;
            string resource = "/ISAPI/Intelligent/FDLib/FDSearch?format=json";
            FDSearchRequest fDSearchRequest = new FDSearchRequest();
            fDSearchRequest.FPID = faceTerminalID;
            StringContent stringContent = new StringContent(JsonSerializer.Serialize(fDSearchRequest), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync(resource, stringContent);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var response = JsonSerializer.Deserialize<FDSearchResponse>(content, _jsonOpt);

                if (response.MatchList != null && response.MatchList.Length >= 1)
                {
                    Uri uri = new Uri(response.MatchList[0].FaceURL);
                    result = await _client.GetAsync(uri.AbsolutePath);
                    if (result.IsSuccessStatusCode)
                        bytesEncodedBase64 = Convert.ToBase64String(await result.Content.ReadAsByteArrayAsync());
                }
            }
            return bytesEncodedBase64;
        }
        public async Task<string> CreateVisitFlow()
        {
            string employeeFPID = string.Empty;
            string qr = null;
            SearchRequest searchRequest = new SearchRequest();
            searchRequest.UserInfoSearchCond.FuzzySearch = "Visita";
            searchRequest.UserInfoSearchCond.MaxResults = 1;

            SearchResponse busquedaVisitor = await SearchUsers(searchRequest);
            if (busquedaVisitor != null && busquedaVisitor.UserInfoSearch.UserInfo != null && busquedaVisitor.UserInfoSearch.UserInfo.Length == 1)
            {
                employeeFPID = busquedaVisitor.UserInfoSearch.UserInfo[0].EmployeeNo;
                //actualizar acceso
            }
            else
            {
                string faceId = "2"; // Son usuarios ya definiso 1.- Admin y 2.- Visita
                DateTime from = DateTime.Now.ConvertDateToMexicoCentralLocalZone();
                DateTime to = from.AddMinutes(5);
                RecordRequest userInfoRequest = CreateRecordRequest(faceId, "Visita", "visitor", from, to, "unknown");
                RecordResponse recordResponse = await CreateUserInfoRecord(userInfoRequest);
                if (recordResponse != null)
                {
                    employeeFPID = faceId;
                }
            }
            //Eliminar Tarjetas


            //Crear Tarjeta
            var cardNo = new Random().Next(1, 1000000).ToString();

            CIRecordRequest cIRecordRequest = new CIRecordRequest();
            cIRecordRequest.CardInfo.CardNo = cardNo;
            cIRecordRequest.CardInfo.EmployeeNo = employeeFPID;

            GenericResponse cardResponse = await CreateCardRecord(cIRecordRequest);
            if (cardResponse != null)
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(cardNo, QRCodeGenerator.ECCLevel.Q);
                PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

                byte[] qrCodeImageAsBase64 = qrCode.GetGraphic(20);
                qr = Convert.ToBase64String(qrCodeImageAsBase64);
            }
            return qr;

        }
        public async Task<AcsEventResponse> GetEventLogs(AcsEventRequest acsEventRequest)
        {
            string resource = "/ISAPI/AccessControl/AcsEvent?format=json";
            StringContent stringContent = new StringContent(JsonSerializer.Serialize(acsEventRequest), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync(resource, stringContent);
            if (result.IsSuccessStatusCode)
            {
                var response = JsonSerializer.Deserialize<AcsEventResponse>(await result.Content.ReadAsStringAsync(), _jsonOpt);
                return response;
            }
            return null;
        }

        public async Task<bool> ModifyNameStartTimeEndTimeFlow(string FPID, string fullname, DateTime from, int numberOfDays) 
        {
            RecordResponse recordResponseModifyUser = null;
            if (string.IsNullOrWhiteSpace(FPID)) throw new ArgumentNullException("FPID no puede ser nulo, ID persona en Terminal(No Empleado)");
            SearchRequest searchRequest = new SearchRequest();
            searchRequest.UserInfoSearchCond.FuzzySearch = FPID;
            SearchResponse searchResponse = await SearchUsers(searchRequest);
            if (searchResponse != null)
            {
                var userInfoArray = searchResponse.UserInfoSearch.UserInfo;
                if (userInfoArray != null && userInfoArray.Length > 0)
                {
                    //actualizacion de fechas y nombres en la terminal

                    RecordRequest recordRequest = new RecordRequest();
                    if (userInfoArray[0].Valid != null)
                    {
                        if (from.ToString("s") != userInfoArray[0].Valid.BeginTime)
                        {
                            DateTime now = DateTime.Now.ConvertDateToMexicoCentralLocalZone();
                            DateTime dateFrom = from;
                            dateFrom = dateFrom.AddTicks(now.TimeOfDay.Ticks);
                            DateTime dateTo;

                            if (numberOfDays == 30)
                                dateTo = dateFrom.AddMonths(1);
                            else if (numberOfDays == 365)
                                dateTo = dateFrom.AddYears(1);
                            else
                                dateTo = dateFrom.AddDays(numberOfDays);

                            userInfoArray[0].Valid.BeginTime = dateFrom.ToString("s");
                            userInfoArray[0].Valid.EndTime = dateTo.ToString("s");
                        }

                        if (string.IsNullOrWhiteSpace(fullname) == false && userInfoArray[0].Name != fullname)
                            userInfoArray[0].Name = fullname;

                    }
                    recordRequest.UserInfo = userInfoArray.FirstOrDefault();

                    recordResponseModifyUser = await ModifyUserInfoRecord(recordRequest);

                }
            }
            return recordResponseModifyUser != null;
        }

        public async Task<GenericResponse> DeletePerson(string FPID) 
        {
            GenericResponse genericResponse = null;
            string resource = "/ISAPI/AccessControl/UserInfo/Delete?format=json";
            if (!string.IsNullOrWhiteSpace(FPID)) 
            {
                DeleteUserInfoRequest deleteUserInfo = new DeleteUserInfoRequest();
                deleteUserInfo.UserInfoDelCond = new Userinfodelcond();
                deleteUserInfo.UserInfoDelCond.EmployeeNoList = new Employeenolist[] { new Employeenolist { EmployeeNo = FPID } };
                StringContent stringContent = new StringContent(JsonSerializer.Serialize(deleteUserInfo), Encoding.UTF8, "application/json");
                var result = await _client.PutAsync(resource, stringContent);
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var response = JsonSerializer.Deserialize<GenericResponse>(content, _jsonOpt);
                    genericResponse = response;
                }

            }
       
            return genericResponse;   
        }

        private RecordRequest CreateRecordRequest(string FPID, string name, string usertype, DateTime from, DateTime to, string gender)
        {
            RecordRequest userInfoRequest = new RecordRequest();
            userInfoRequest.UserInfo.EmployeeNo = FPID;
            userInfoRequest.UserInfo.Name = name;
            userInfoRequest.UserInfo.Valid.BeginTime = from.ToString("s");
            userInfoRequest.UserInfo.Valid.EndTime = to.ToString("s");
            userInfoRequest.UserInfo.DoorRight = "1";
            userInfoRequest.UserInfo.UserType = usertype;
            var plan = new Rightplan()
            {
                DoorNo = 1,
                PlanTemplateNo = "1"
            };
            userInfoRequest.UserInfo.RightPlan = new Rightplan[] { plan };
            userInfoRequest.UserInfo.Gender = gender;
            return userInfoRequest;
        }

    }
}
