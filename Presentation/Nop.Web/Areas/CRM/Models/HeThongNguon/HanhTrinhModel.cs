using System;

namespace Nop.Web.Areas.CRM.Models.HeThongNguon
{
    public class HanhTrinhModel
    {
        public int Id { get; set; }
        public String MoTa { get; set; }
        public String MaHanhTrinh { get; set; }
        public int? TongKhoangCach { get; set; }
    }
}
