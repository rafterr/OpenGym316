namespace PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.FDILib
{
    public class FaceDataRecordRequest
    {
        public string FaceLibType { get; set; } = FaceTerminalConstants.FaceLibType;
        public string FDID { get; set; } = FaceTerminalConstants.FDID;


        /// <summary>
        /// ID de la persona a reconocer
        /// </summary>
        public string FPID { get; set; }

    }
}
