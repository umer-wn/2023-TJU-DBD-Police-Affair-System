namespace web.DTO_group2
{
    // all   作用: testController 测试请求
    public class Request
    {
        public string input { get; set; } = "";
    }

    // fhl  作用：被修改人权限的警号
    public class notification
    {
        public string temp { get; set; } = "";
    }

    //hyh fhl  作用:POLICEMEN表的相关属性
    public class Police
    {
        public string police_number { get; set; } = "";
        public string police_name { get; set; } = "";
        public string ID_number { get; set; } = "";
        public string phone_number { get; set; } = "";
        public string gender { get; set; } = "";

    }
    //hyh    作用: LoginController里用于接收前端填写的 1.登录账户 2. 登录密码
    public class LoginRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
    //hyh   作用：PasswordChangeController里用于接收前端填写的 1.警号  2.身份证号码ID 
    public class PasswordChangeRequest
    {
        public string PoliceNumber { get; set; } = "";
        public string IdNumber { get; set; } = "";
    }
    //fhl    作用: 用于修改权限 1.被修改警员的警号 2.修改人警号  3.原权限等级 4.修改权限 5.修改原因
    public class permit
    {
        
        public string h_number { get; set; } = "";
        public string s_number { get; set; } = "";
        public string F_level { get; set; } = "";
        public string L_level { get; set; } = "";
        public string status { get; set; } = "待处理";
        public string reason { get; set; } = "";
    }
}
