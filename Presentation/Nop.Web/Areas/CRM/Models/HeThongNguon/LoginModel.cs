namespace Nop.Web.Areas.CRM.Models.HeThongNguon
{
    public class LoginModel
    {
        public string ClientId { get; set; }
        public string CodeName { get; set; }
        public string KeyPass { get; set; }
    }

    public class ObjectInfoModel
    {
        public string ApiToken { get; set; }
        public string CodeName { get; set; }
        public string KeyPass { get; set; }
        public string ClientIP { get; set; }
        public string ClientId { get; set; }
    }
}
