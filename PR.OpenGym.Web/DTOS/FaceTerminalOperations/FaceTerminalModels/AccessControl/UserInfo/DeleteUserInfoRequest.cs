using static PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.UserInfo.DeleteUserInfoRequest;

namespace PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.UserInfo
{
    public class DeleteUserInfoRequest
    {
        public Userinfodelcond UserInfoDelCond { get; set; }
    }
    public class Employeenolist
    {
        public string EmployeeNo { get; set; }
    }
    public class Userinfodelcond
    {
        public Employeenolist[] EmployeeNoList { get; set; }
    }
}
