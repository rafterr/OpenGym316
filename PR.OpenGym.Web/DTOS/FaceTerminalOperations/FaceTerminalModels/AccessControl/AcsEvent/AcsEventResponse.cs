namespace PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.AcsEvent
{
    public class AcsEventResponse
    {
        public Acsevent AcsEvent { get; set; } = new Acsevent();
    }
    public class Acsevent
    {
        public string SearchID { get; set; }
        public int TotalMatches { get; set; }
        public string ResponseStatusStrg { get; set; }
        public int NumOfMatches { get; set; }
        public Infolist[] InfoList { get; set; }
    }

    public class Infolist
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public DateTime Time { get; set; }
        public string CardNo { get; set; }
        public int CardType { get; set; }
        public string Name { get; set; }
        public int CardReaderNo { get; set; }
        public int DoorNo { get; set; }
        public string EmployeeNoString { get; set; }
        public int Type { get; set; }
        public int SerialNo { get; set; }
        public string UserType { get; set; }
        public string CurrentVerifyMode { get; set; }
        public string Mask { get; set; }
    }
}
