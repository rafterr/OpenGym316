namespace PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.CardInfo
{
    public class CIRecordRequest
    {
        public Cardinfo CardInfo { get; set; } = new Cardinfo();
    }

    public class Cardinfo
    {
        public string EmployeeNo { get; set; }
        public string CardNo { get; set; }
        public bool DeleteCard { get; set; } = true;
        public string CardType { get; set; } = "normalCard";
        public string LeaderCard { get; set; } = string.Empty;
        public bool CheckCardNo { get; set; }
        public bool CheckEmployeeNo { get; set; } = true;
        public bool AddCard { get; set; } 
    }
}
