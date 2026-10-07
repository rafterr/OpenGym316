namespace PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.AcsEvent
{
    public class AcsEventRequest
    {
        public Acseventcond AcsEventCond { get; set; } = new Acseventcond();

    }

    public class Acseventcond
    {
        public string SearchID { get; set; } = "1";
        public int SearchResultPosition { get; set; } = 0;
        public int MaxResults { get; set; } = 30;
        public int Major { get; set; } 
        public int Minor { get; set; }
        public string StartTime { get; set; } 
        public string EndTime { get; set; }
        public string CardNo { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool PicEnable { get; set; }
        public bool TimeReverseOrder { get; set; } 
        public bool IsAbnomalTemperature { get; set; }
        public string TemperatureSearchCond { get; set; } = string.Empty;
    }
}
