namespace PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.UserInfo
{
    public class RecordRequest
    {
        public Userinfo UserInfo { get; set; } = new Userinfo();
    }


    public class Userinfo
    {
        public string EmployeeNo { get; set; }
        public bool DeleteUser { get; set; } = true;
        public string Name { get; set; }
        public string UserType { get; set; } = "normal";
        public bool CloseDelayEnabled { get; set; } = false;
        public Valid Valid { get; set; } = new Valid();
        public string VelongGroup { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string DoorRight { get; set; } = string.Empty;
        public Rightplan[] RightPlan { get; set; } = new Rightplan[] { new Rightplan() };
        public string UserVerifyMode { get; set; } = "";//"faceOrFpOrCardOrPw";
        public bool CheckUser { get; set; }
        public bool AddUser { get; set; }
        public string[] CallNumbers { get; set; } = new string[] { };
        public int[] FloorNumbers { get; set; } = new int[] { };
        //public string NumOfFace { get; set; } = string.Empty;
        //public string NumOfFP { get; set; } = string.Empty;
        //public string NumOfCard { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
    }

    public class Valid
    {
        public bool Enable { get; set; } = true;
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string TimeType { get; set; } = "local";
    }

    public class Rightplan
    {
        public int? DoorNo { get; set; }
        public string PlanTemplateNo { get; set; } = string.Empty;
    }

}
