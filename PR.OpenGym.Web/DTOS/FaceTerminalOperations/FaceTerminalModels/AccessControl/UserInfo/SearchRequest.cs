namespace PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.UserInfo
{
    public class SearchRequest
    {
        public Userinfosearchcond UserInfoSearchCond { get; set; } = new Userinfosearchcond();
    }

    public class Userinfosearchcond
    {
        public string SearchID { get; set; } = Guid.NewGuid().ToString();
        public int SearchResultPosition { get; set; } = 0;
        public int MaxResults { get; set; } = 30;
        public string FuzzySearch { get; set; } = string.Empty;
    }
}
