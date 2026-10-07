namespace PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.FDILib
{
    public class FDLibRequest
    {
        public string FaceLibType { get; set; } = FaceTerminalConstants.FaceLibType;
        public string Name { get; set; } = Guid.NewGuid().ToString();
        //public string CustomInfo { get; set; }

    }
}
