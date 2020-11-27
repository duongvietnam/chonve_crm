using System;

namespace Nop.Web.Areas.CRM.Models.HeThongNguon
{
    public class ChuyenDiModel
    {
        public String Ma { get; set; }
        public String NgayDi { get; set; }
        public String NgayDiThuc { get; set; }
        public String ThoiGianDi { get; set; }
        public String ThoiGianDen { get; set; }
        public int? LichTrinhId { get; set; }
        public String TenLichTrinh { get; set; }
        public int? LaiXeId { get; set; }
        public String TenLaiXe { get; set; }
        public String TenLaiXeRutGon { get; set; }
        public String DienThoaiLaiXe { get; set; }
        public int? XeVanChuyenId { get; set; }
        public String BienSoXe { get; set; }
        public String BienSoXe3So { get; set; }
        public int? HanhTrinhId { get; set; }
        public String TenHanhTrinh { get; set; }
        public String NgayTao { get; set; }
        public int? NguoiTaoId { get; set; }
        public String TenNguoiTao { get; set; }
        public int? TrangThaiId { get; set; }
        public int? trangthai { get; set; }
        public String TrangThaiText { get; set; }
        public String GhiChu { get; set; }
        public bool isEdit { get; set; }
        public int? SoKhach { get; set; }
        public int? SoGhe { get; set; }
        public String TenLoaiXe { get; set; }
        public int? LichTrinhLoaiXeId { get; set; }
        public int? LoaiXeId { get; set; }
        public decimal? GiaVe { get; set; }
        public decimal? GiaVeGoc { get; set; }
        public int? Id { get; set; }
    }

    public class ParameterChuyenDiModel
    {
        public int HanhTrinhId { get; set; }
        public String TuNgay { get; set; }
        public String DenNgay { get; set; }
    }
}
