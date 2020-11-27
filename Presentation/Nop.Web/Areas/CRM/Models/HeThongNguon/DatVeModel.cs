namespace Nop.Web.Areas.CRM.Models.HeThongNguon
{
    public class DatVeModel
    {
        public string Ma { get; set; }
        public int? KhachHangId { get; set; }
        public string TenKhachHang { get; set; }
        public string DienThoai { get; set; }
        public string NgayDi { get; set; }
        public string GioDi { get; set; }
        public string ThoiGianDi { get; set; }
        public string ThoiGianDen { get; set; }
        public int? LichTrinhId { get; set; }
        public string TenLichTrinh { get; set; }
        public int? HanhTrinhId { get; set; }
        public string TenHanhTrinh { get; set; }
        public int? ChuyenDiId { get; set; }
        public string TenChuyenDi { get; set; }
        public int? SoDoGheId { get; set; }
        public string sodoghekyhieu { get; set; }
        public int? TrangThaiId { get; set; }
        public int? trangthai { get; set; }
        public string TrangThaiText { get; set; }
        public bool isDonTaxi { get; set; }
        public bool isLenhDonTaXi { get; set; }
        public string MaTaXi { get; set; }
        public int? DiemDonId { get; set; }
        public string TenDiemDon { get; set; }
        public string DiaChiNha { get; set; }
        public string GhiChu { get; set; }
        public int? NguoiTaoId { get; set; }
        public string TenNguoiTao { get; set; }
        public string NgayTao { get; set; }
        public decimal? GiaTien { get; set; }
        public bool isEdit { get; set; }
        public bool isThanhToan { get; set; }
        public bool isKhachHuy { get; set; }
        public bool isNoiBai { get; set; }
        public bool isDaXacNhan { get; set; }
        public int? VeChuyenDenId { get; set; }
        public string TenDiemTra { get; set; }
        public int? Id { get; set; }
    }
}
