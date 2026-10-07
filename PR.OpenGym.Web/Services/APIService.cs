using Newtonsoft.Json;
using PR.OpenGym.Data;
using PR.OpenGym.Data.DTOS;
using PR.OpenGym.Utilities.ExtensionMethods;
using System.Net.Http.Headers;
using System.Text;

namespace PR.OpenGym.Web.Services
{
    public class APIService : IApiService
    {
        private readonly HttpClient _httpClient;

        public APIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<AssociateGetDTO>> GetAssociates()
        {
            var resource = "associate";
            var response = await _httpClient.GetAsync(resource);
            var body = await response.Content.ReadAsStringAsync();
            var associates = JsonConvert.DeserializeObject<IEnumerable<AssociateGetDTO>>(body);
            return associates ?? new List<AssociateGetDTO>();
        }

        public async Task<IEnumerable<AssociateGetDTO>> GetAssociatesByName(string associateName)
        {
            var resource = "associate/search/" + associateName;
            var response = await _httpClient.GetAsync(resource);
            var body = await response.Content.ReadAsStringAsync();
            var associates = JsonConvert.DeserializeObject<IEnumerable<AssociateGetDTO>>(body);
            return associates ?? new List<AssociateGetDTO>();
        }

        public async Task<AssociateGetDTO> GetAssociate(int id)
        {
            var resource = "associate/" + id;
            var response = await _httpClient.GetAsync(resource);
            var body = await response.Content.ReadAsStringAsync();
            var associates = JsonConvert.DeserializeObject<AssociateGetDTO>(body);
            return associates ?? new AssociateGetDTO();
        }

        public async Task<AssociateDetails> GetAssociateDetails(int associateId)
        {
            var resource = "AssociateDetails/Associate/" + associateId;
            var response = await _httpClient.GetAsync(resource);
            var body = await response.Content.ReadAsStringAsync();
            var associates = JsonConvert.DeserializeObject<AssociateDetails>(body);
            return associates ?? new AssociateDetails();
        }


        public async Task<IEnumerable<CheckIn>> GetCheckIns()
        {
            var resource = "associate/checkin?dateTime=" + DateTime.Now.ToString("yyyy-MM-dd");
            var response = await _httpClient.GetAsync(resource);
            var body = await response.Content.ReadAsStringAsync() ?? "{}";
            var checkins = JsonConvert.DeserializeObject<IEnumerable<CheckIn>>(body);
            return checkins ?? new List<CheckIn>();
        }

        public Task<Product> GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProducts(bool isMembership)
        {
            var resource = "product?isMembership=" + isMembership;
            var response = await _httpClient.GetAsync(resource);
            var body = await response.Content.ReadAsStringAsync() ?? "{}";
            IEnumerable<Product> procuts = JsonConvert.DeserializeObject<IEnumerable<Product>>(body);
            return procuts;
        }

        public async Task<AssociateGetDTO> PostAssociate(AssociatePostDTO associate, IFormFile formFile)
        {
            var resource = "associate";
            var body = string.Empty;
            using (var multipartComtent = new MultipartFormDataContent())
            {
                //var fileStreamContent = new StreamContent(formFile.OpenReadStream());
                //fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                //multipartComtent.Add(fileStreamContent, "ImgFile", "photo.jpg");
                //multipartComtent.Add(new StringContent(associate.Id.ToString()), "Id");
                if (associate.FirstName != null)
                    multipartComtent.Add(new StringContent(associate.FirstName), "FirstName");
                if (associate.LastName != null)
                    multipartComtent.Add(new StringContent(associate.LastName), "LastName");
                if (associate.Age != null)
                    multipartComtent.Add(new StringContent(associate.Age.ToString()), "Age");
                if (associate.BranchId != null)
                    multipartComtent.Add(new StringContent(associate.BranchId.ToString()), "BranchId");
                if (associate.Gender != null)
                    multipartComtent.Add(new StringContent(((int)associate.Gender).ToString()), "Gender");
                if (associate.Email != null)
                    multipartComtent.Add(new StringContent(associate.Email), "Email");
                if (associate.Facebook != null)
                    multipartComtent.Add(new StringContent(associate.Facebook), "Facebook");
                if (associate.Phone != null)
                    multipartComtent.Add(new StringContent(associate.Phone), "Phone");

                if (associate.AssociateMembership != null)
                {
                    multipartComtent.Add(new StringContent(associate.AssociateMembership.MembershipId.ToString()), "AssociateMembership.MembershipId");
                    multipartComtent.Add(new StringContent(MembershipStatus.Active.ToString()), "AssociateMembership.MembershipStatus");
                    var membershipFrom = associate.AssociateMembership?.From;
                    var now = DateTime.Now.ConvertDateToMexicoCentralLocalZone();
                    if (membershipFrom == null || membershipFrom.Value == DateTime.MinValue)
                    {
                        associate.AssociateMembership.From = now;
                    }
                    else
                    {
                        associate.AssociateMembership.From = associate.AssociateMembership.From.Value.AddTicks(now.TimeOfDay.Ticks);
                    }
                    multipartComtent.Add(new StringContent(associate.AssociateMembership.From.Value.ToString("s")), "AssociateMembership.From");
                }

                //multipartComtent.Add(new StringContent(DateTime.UtcNow.ToString()), "CreatedOn");
                //multipartComtent.Add(new StringContent(DateTime.UtcNow.ToString()), "ModifiedOn");


                var response = await _httpClient.PostAsync(resource, multipartComtent);
                if (!response.IsSuccessStatusCode)
                    return null;
                body = await response.Content.ReadAsStringAsync();
            }
            var associateGet = JsonConvert.DeserializeObject<AssociateGetDTO>(body);
            return associateGet;
        }

        public async Task<AssociateDetails> PostAssociateDetails(AssociateDetails associateDetails)
        {
            var resource = "associatedetails";
            var content = new StringContent(JsonConvert.SerializeObject(associateDetails), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(resource, content);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                associateDetails = JsonConvert.DeserializeObject<AssociateDetails>(body);
            else
                return null;

            return associateDetails;
        }

        public async Task<AssociateMembership> GetAssociateMembership(int associateId)
        {
            var resource = "Associate/associatemembership?asscoiateId=" + associateId;
            var response = await _httpClient.GetAsync(resource);
            var body = await response.Content.ReadAsStringAsync() ?? "{}";
            var checkins = JsonConvert.DeserializeObject<AssociateMembership>(body);
            return checkins ?? new AssociateMembership();
        }

        public async Task<IEnumerable<Payment>> GetTodayPayments()
        {
            var resource = "payment/TodayPayments";
            var response = await _httpClient.GetAsync(resource);
            var body = await response.Content.ReadAsStringAsync() ?? "{}";
            var checkins = JsonConvert.DeserializeObject<IEnumerable<Payment>>(body);
            return checkins ?? new List<Payment>();
        }

        public async Task<AssociateMembership> PostAssociateMembership(AssociateMembership associateMembership)
        {

            var resource = "associate/associatemembership";
            var content = new StringContent(JsonConvert.SerializeObject(associateMembership), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(resource, content);
            var body = await response.Content.ReadAsStringAsync() ?? "{}";
            if (response.IsSuccessStatusCode)
            {
                associateMembership = JsonConvert.DeserializeObject<AssociateMembership>(body);
            }
            return associateMembership;

        }

        public async Task<bool> UpdateAssociate(int associateId, AssociatePostDTO associate)
        {
            var resource = "Associate/" + associateId;
            var now = DateTime.Now.ConvertDateToMexicoCentralLocalZone();
            if (associate.AssociateMembership?.From == null || associate.AssociateMembership.From == DateTime.MinValue)
            {
                associate.AssociateMembership.From = now;
            }
            else
            {
                associate.AssociateMembership.From = associate.AssociateMembership.From.Value.AddTicks(now.TimeOfDay.Ticks);
            }
            associate.Status = Status.Active;
            var content = new StringContent(JsonConvert.SerializeObject(associate), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(resource, content);
            var body = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAssociateDetails(int associateDetailsId, AssociateDetails associateDetails)
        {
            var resource = "AssociateDetails/" + associateDetailsId;
            var content = new StringContent(JsonConvert.SerializeObject(associateDetails), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(resource, content);
            var body = await response.Content.ReadAsStringAsync();
            //var associates = JsonConvert.DeserializeObject<Associate>(body);
            return response.IsSuccessStatusCode;
        }

        public async Task<Membership> GetMembershipById(int membershipId)

        {
            var resource = "Product/membership/" + membershipId;
            var response = await _httpClient.GetAsync(resource);
            var body = await response.Content.ReadAsStringAsync();
            var associates = JsonConvert.DeserializeObject<Membership>(body);
            return associates;

        }

        public async Task<bool> PostMembership(Membership membership)
        {
            var resource = "Product/membership";
            var content = new StringContent(JsonConvert.SerializeObject(membership), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(resource, content);
            var body = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateMembership(Membership membership)
        {
            var resource = "Product/membership";
            var content = new StringContent(JsonConvert.SerializeObject(membership), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(resource, content);
            var body = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteMembership(int mebershipId)
        {
            var resource = "Product/" + mebershipId;
            var response = await _httpClient.DeleteAsync(resource);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> CreatePayment(CreatePaymentDTO createPaymentDTO)
        {
            var resource = $"Payment/PayMembership";
            var content = new StringContent(JsonConvert.SerializeObject(createPaymentDTO), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(resource, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAssociate(int associateId)
        {
            var resource = $"Associate/" + associateId;
            var response = await _httpClient.DeleteAsync(resource);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByAssociateId(int associateId)
        {
            var resource = "payment/PaymentsByAssociate/"+associateId;
            var response = await _httpClient.GetAsync(resource);
            var body = await response.Content.ReadAsStringAsync() ?? "[]";
            var checkins = JsonConvert.DeserializeObject<IEnumerable<Payment>>(body);
            return checkins ?? new List<Payment>();
        }

    }
}
