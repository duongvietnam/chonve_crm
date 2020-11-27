using System;

namespace Nop.Web.Api.Models.CRM
{
    public class GiaoDichModel
    {
        public int HangKhachHang { get; set; }
        public Decimal DonGia { get; set; }
        public DateTime NgayGiaoDich { get; set; }
        public int DichVuId { get; set; }
    }
}
