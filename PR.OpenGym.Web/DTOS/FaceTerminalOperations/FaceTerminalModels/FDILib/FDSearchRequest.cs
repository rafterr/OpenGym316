namespace PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.FDILib
{
    public class FDSearchRequest
    {

        public int SearchResultPosition { get; set; }
        public int MaxResults { get; set; }
        public string FaceLibType { get; set; } = FaceTerminalConstants.FaceLibType;
        public string FDID { get; set; } = FaceTerminalConstants.FDID;
        public string FPID { get; set; }
        public string StartTime { get; set; } = string.Empty; 
        public string EndTime { get; set; } = string.Empty; 
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty; 
        public string City { get; set; } = string.Empty;
        public string CertificateType { get; set; } = string.Empty; 
        public string CertificateNumber { get; set; } = string.Empty;
        public string IsInLibrary { get; set; } = "yes";
        public bool IsDisplayCaptureNum { get; set; } =true;
        public string RowKey { get; set; } = string.Empty;
        public bool Transfer { get; set; } =true;

    }
}
