namespace PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.UserInfo
{
    public class SearchResponse
    {
        public Userinfosearch UserInfoSearch { get; set; }
    }

    public class Userinfosearch
    {
        public string SearchID { get; set; }
        public string ResponseStatusStrg { get; set; }
        public int NumOfMatches { get; set; }
        public int TotalMatches { get; set; }
        public Userinfo[] UserInfo { get; set; }
    }
}
