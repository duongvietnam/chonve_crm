//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Common;
using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Web.Areas.CRM.Models.CRM
{

    public class MarMaGiamGiaModel : BaseNopEntityModel
    {
        public MarMaGiamGiaModel()
        {
            marMaGiamGiaSearchModel = new MarMaGiamGiaSearchModel();
            NhomKhachHangSelectedId = new List<int>();
            DDLNhomKhachHang = new List<SelectListItem>();
            ListHangKhachHangSelectedId = new List<int>();
            DDLHangKhachHang = new List<SelectListItem>();
            KhachHangSelectedId = new List<int>();
            DDLKhachHang = new List<SelectListItem>();
        }
        public String MA { get; set; }
        public DateTime? NGAY_SU_DUNG { get; set; }
        public Int32? KHACH_HANG_ID { get; set; }
        public Int32? DOANH_NGHIEP_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Int32? NGUOI_TAO { get; set; }
        public Int32? MAR_DIEU_KIEN_ID { get; set; }
        public int? CO_THE_KET_HOP { get; set; }
        //more
        [UIHint("Money")]
        public decimal SaleTheoSoTien { get; set; }
        [UIHint("NumberCustomize")]
        public decimal SaleTheoPhanTram { get; set; }
        public SelectList DDLDuocKetHop { get; set; }
        public EnumCoKhong CoKhong
        {
            get
            {
                return (EnumCoKhong)CO_THE_KET_HOP.GetValueOrDefault(0);
            }
            set
            {
                CO_THE_KET_HOP = (int)value;
            }
        }
        [UIHint("DateTime")]
        public DateTime? TuNgay { get; set; }
        [UIHint("DateTime")]
        public DateTime? DenNgay { get; set; }
        [UIHint("Money")]
        public Decimal? DonGia { get; set; }
        public string DonViTinhText { get; set; }
        public string SaleText { get; set; }
        public string DonGiaText { get; set; }
        [UIHint("NumberCustomize")]
        public Decimal SoPhieuTao { get; set; }
        public int SoPhieuDaGui { get; set; }
        public int SoPhieuKhachHangDaSuDung { get; set; }
        public string TuNgayString { get; set; }
        public string DenNgayString { get; set; }
        public MarMaGiamGiaSearchModel marMaGiamGiaSearchModel { get; set; }
        public string TenKhachHang { get; set; }
        public string TrangThaiPhieu { get; set; }
        public IList<int> NhomKhachHangSelectedId { get; set; }
        public IList<SelectListItem> DDLNhomKhachHang { get; set; }
        public IList<int> ListHangKhachHangSelectedId { get; set; }
        public IList<SelectListItem> DDLHangKhachHang { get; set; }
        public IList<int> KhachHangSelectedId { get; set; }
        public IList<SelectListItem> DDLKhachHang { get; set; }
        public bool Random { get; set; }
        public int MarketingId { get; set; }
    }
    public partial class MarMaGiamGiaSearchModel : BaseSearchModel
    {
        public MarMaGiamGiaSearchModel()
        {
        }
        public string KeySearch { get; set; }
        public int MarketingId { get; set; }
        public int TrangThaiPhieu { get; set; }
    }
    public partial class MarMaGiamGiaListModel : BasePagedListModel<MarMaGiamGiaModel>
    {

    }
}

