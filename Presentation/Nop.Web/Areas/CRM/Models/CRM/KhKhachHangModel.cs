//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.CRM;
using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Web.Areas.CRM.Models.CRM
{

    public class KhKhachHangModel : BaseNopEntityModel
    {
        public KhKhachHangModel()
        {
            DanhBaDienThoai = new List<DanhBaDienThoaiModel>();
            ListCauHinhMoRong = new List<CauHinhMoRongModel>();
            ListGiaoDichModels = new List<GiaoDichModel>();
            NhomKhachHang = new List<NhomKhachHangModel>();
        }
        public String TEN { get; set; }
        [UIHint("Date")]
        public DateTime? NGAY_SINH { get; set; }
        public Int32? GIOI_TINH { get; set; }
        public String EMAIL { get; set; }
        public String FACEBOOK { get; set; }
        public String ZALO { get; set; }
        public Int32? LOAI_KHACH_HANG { get; set; }
        public Int32? DOANH_NGHIEP_ID { get; set; }
        public Int32? DIEM_DICH_VU { get; set; }
        public Int32? DIEM_TICH_LUY { get; set; }
        public Int32 DIEM_PHAN_HANG { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Int32? NGUOI_TAO { get; set; }
        public Int32? TRANG_THAI { get; set; }
        // more
        public SelectList DDLLoaiKhachHang { get; set; }
        public SelectList DDLGioiTinh { get; set; }
        public List<DanhBaDienThoaiModel> DanhBaDienThoai { get; set; }
        public String NgaySinhString { get; set; }
        public String SoDienThoai { get; set; }
        public List<CauHinhMoRongModel> ListCauHinhMoRong { get; set; }
        public String TongSoTienDaChiTieu { get; set; }
        public int TongSoHoaDon { get; set; }
        public int TongSoDichVu { get; set; }
        public IList<GiaoDichModel> ListGiaoDichModels { get; set; }
        public string TenNguoiTao { get; set; }
        public string GIOI_TINH_TEXT { get; set; }
        public EnumGioiTinh gioiTinh
        {
            get
            {
                return (EnumGioiTinh)GIOI_TINH.GetValueOrDefault(0);
            }
            set
            {
                GIOI_TINH = (int)value;
            }
        }
        public EnumLoaiKhachHang loaiKhachHang
        {
            get
            {
                return (EnumLoaiKhachHang)LOAI_KHACH_HANG.GetValueOrDefault(0);
            }
            set
            {
                LOAI_KHACH_HANG = (int)value;
            }
        }
        public string LOAI_KHACH_HANG_TEXT { get; set; }
        public EnumTrangThaiKhachHang trangThaiKhachHang
        {
            get
            {
                return (EnumTrangThaiKhachHang)TRANG_THAI.GetValueOrDefault(0);
            }
            set
            {
                TRANG_THAI = (int)value;
            }
        }
        public string TRANG_THAI_TEXT { get; set; }
        public List<NhomKhachHangModel> NhomKhachHang { get; set; }
        public String CapHangHienTai { get; set; }
        public String SoLanSuDungDichVu { get; set; }
        public IList<KhTopGiaoDichModel> TopGiaoDich { get; set; }
        public IList<KhTopGiaoDichModel> TopGiaoDich30days { get; set; }
        public IList<KhTopGiaoDichModel> TopGiaoDich60days { get; set; }
        public IList<KhTopGiaoDichModel> TopGiaoDich90days { get; set; }

    }
    public partial class KhKhachHangSearchModel : BaseSearchModel
    {
        public KhKhachHangSearchModel()
        {
        }
        public string KeySearch { get; set; }
        public string PhoneSearch { get; set; }
        public string EmailSearch { get; set; }
        public Int32? GenderSearch { get; set; }
        public Int32? TypeSearch { get; set; }
        public Int32? FloorPointSearch { get; set; }
        public Int32? TopPointSearch { get; set; }
        public Int32? DayTimeSearch { get; set; }
        public SelectList GenderLabels { get; set; }
        public SelectList TypeLabels { get; set; }
    }

    public partial class KhPhanTichKhachHangSearchModel : BaseSearchModel
    {
        public KhPhanTichKhachHangSearchModel()
        {
            DDLHangKhachHang = new List<SelectListItem>();
            DDLNhomKhachHang = new List<SelectListItem>();
        }
        public string KeySearch { get; set; }
        public SelectList DDLOptionOrderBy { get; set; }
        public int OptionOrderBy { get; set; }
        public EnumSapXepHangKhachHang sapXemHangKhachHang
        {
            get
            {
                return (EnumSapXepHangKhachHang)OptionOrderBy;
            }
            set
            {
                OptionOrderBy = (int)value;
            }
        }
        public string OptionOrderByText { get; set; }
        public IList<SelectListItem> DDLHangKhachHang { get; set; }
        public int HangKhachHang { get; set; }
        public IList<SelectListItem> DDLNhomKhachHang { get; set; }
        public int NhomKhachHang { get; set; }
        public int TuDiem { get; set; }
        public int DenDiem { get; set; }
        public IList<SelectListItem> DDLDichVu { get; set; }
        public int DichVu { get; set; }
    }

    public partial class KhKhachHangListModel : BasePagedListModel<KhKhachHangModel>
    {

    }
    public class DanhBaDienThoaiModel
    {
        public int SoDienThoaiId { get; set; }
        public int NHOM_DANH_BA { get; set; }
        public string SO_DIEN_THOAI { get; set; }
        public IList<SelectListItem> DDLNhomSoDienThoai { get; set; }
    }
    public class CauHinhMoRongModel
    {
        public int CauHinhId { get; set; }
        public int LoaiDuLieu { get; set; }
        public String TenCauHinh { get; set; }
        public String Value { get; set; }
    }
    public class GiaoDichModel
    {
        public String NgayGiaoDich { get; set; }
        public String ThanhTien { get; set; }
        public String DichVu { get; set; }
        public String SoTienGiam { get; set; }
        public string TenKhachHang { get; set; }
        public string MaGiaoDich { get; set; }
        public int IdGiaoDich { get; set; }
        public string EnumTrangThaiGiaoDichValue { get; set; }
        public string EnumHinhThucThanhToanValue { get; set; }
    }
    public class NhomKhachHangModel
    {
        public String TenNhom { get; set; }
        public int Diem { get; set; }
        public String TenKhachHang { get; set; }
    }

    public class KhDVTopModel
    {
        public List<KhTopGiaoDichModel> DsTopGiaoDich { get; set; }
        public string NameDVTop { get; set; }
    }
    public class KhTopGiaoDichModel
    {
        public KhTopDichVuModel TopGiaoDich { get; set; }
    }

    public class KhTopDichVuModel
    {
        public int DichVuId { get; set; }
        public int SoLan { get; set; }
        public string TenDichVu { get; set; }
    }
    public class ChartKhachHangPHModel
    {
        public string TenPhanHang { get; set; }
        public int IdPhanHang { get; set; }
        public Decimal DoanhThu { get; set; }
        public int SoLuong { get; set; }
    }
}

