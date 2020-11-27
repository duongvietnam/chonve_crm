using System.Collections.Generic;

namespace Nop.Web.Areas.CRM.Models.HeThongNguon
{
    public class ResponseApi<T>
    {
        public string code { get; set; }
        public string message { get; set; }
        public string idRecord { get; set; }
        public T objectInfo { get; set; }
        public string isSuccess { get; set; }
    }
}
