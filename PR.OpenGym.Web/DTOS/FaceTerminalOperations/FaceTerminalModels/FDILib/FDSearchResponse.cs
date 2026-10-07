namespace PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.FDILib
{
    public class FDSearchResponse : GenericResponse
    {
        public string ResponseStatusStrg { get; set; }
        public int NumOfMatches { get; set; }
        public int TotalMatches { get; set; }
        public Matchlist[] MatchList { get; set; }

    }
    public class Matchlist
    {
        public string FPID { get; set; }
        public string FaceURL { get; set; }
        public string ModelData { get; set; }
    }
}
