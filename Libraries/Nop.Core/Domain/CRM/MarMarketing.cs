//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace Nop.Core.Domain.CRM
{
    public partial class MarMarketing : BaseEntity
    {
        public String TEN { get; set; }
        public Int32? DOANH_NGHIEP_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Int32? NGUOI_TAO { get; set; }
        public int? HINH_THUC { get; set; }
        public int? CO_THE_KET_HOP { get; set; }
        public EnumHinhThucMarketing HinhThucMarketing
        {
            get
            {
                return (EnumHinhThucMarketing)HINH_THUC.GetValueOrDefault(0);
            }
            set
            {
                HINH_THUC = (int)value;
            }
        }
    }

    public enum EnumHinhThucMarketing
    {
        TatCa = 0,
        GiamGiaKhachHangPhanHang = 1,
        MaGiamGia = 2,
        ChuongTrinhKhuyenMai = 3
    }

    public class LocDieuKienMarketing
    {
        public string Ten { get; set; }
        public Decimal Sale { get; set; }
    }

    public class LocDieuKienMaGiamGia
    {
        public string MaGiamGia { get; set; }
        public Decimal Sale { get; set; }
        public string TrangThai { get; set; }
        public bool CoTheKetHop { get; set; }
        public string DonViTinh { get; set; }
        public decimal DonGia { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
    }
}
