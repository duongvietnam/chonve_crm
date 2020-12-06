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

    public class MarMarketingModel : BaseNopEntityModel
    {
        public MarMarketingModel()
        {
            DDLMarketingHeThong = new List<SelectListItem>();
            ListDieuKien = new List<DieuKienMarketing>();
            DDLHangKhachHang = new List<SelectListItem>();
        }
        public String TEN { get; set; }
        public Int32? DOANH_NGHIEP_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Int32? NGUOI_TAO { get; set; }
        public int? CO_THE_KET_HOP { get; set; }
        public int? HINH_THUC { get; set; }
        // more
        public IList<SelectListItem> DDLMarketingHeThong { get; set; }
        public IList<int> MarketingHeThongs { get; set; }
        public IList<DieuKienMarketing> ListDieuKien { get; set; }
        public IList<SelectListItem> DDLHangKhachHang { get; set; }
        public int HangKhachHangId { get; set; }
        [UIHint("Money")]
        public Decimal? Sale { get; set; }
        [UIHint("Money")]
        public Decimal? DonGia { get; set; }
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
        public EnumHinhThucMarketing hinhThucMarketing
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
        public string HinhThucMarketingString { get; set; }
        [UIHint("Date")]
        public DateTime? TuNgay { get; set; }
        [UIHint("Date")]
        public DateTime? DenNgay { get; set; }
        public string SaleString { get; set; }
        public string TuNgayString { get; set; }
        public string DenNgayString { get; set; }
        [UIHint("Money")]
        public decimal SaleTheoSoTien { get; set; }
        [UIHint("NumberCustomize")]
        public decimal SaleTheoPhanTram { get; set; }
        [UIHint("TimeNullable")]
        public DateTime? TuGio { get; set; }
        [UIHint("TimeNullable")]
        public DateTime? DenGio { get; set; }
    }
    public partial class MarMarketingSearchModel : BaseSearchModel
    {
        public MarMarketingSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class MarMarketingListModel : BasePagedListModel<MarMarketingModel>
    {

    }
    public class DieuKienMarketing
    {
        public DieuKienMarketing()
        {
            DDLDichVu = new List<SelectListItem>();
            DDLDonViTinh = new List<SelectListItem>();
        }
        public int DieuKienId { get; set; }
        public int? DichVuId { get; set; }
        public IList<SelectListItem> DDLDichVu { get; set; }
        public IList<SelectListItem> DDLDonViTinh { get; set; }
        [UIHint("DateNullable")]
        public DateTime? TuNgay { get; set; }
        [UIHint("DateNullable")]
        public DateTime? DenNgay { get; set; }
        [UIHint("TimeNullable")]
        public TimeSpan? TuGio { get; set; }
        [UIHint("TimeNullable")]
        public TimeSpan? DenGio { get; set; }
        [UIHint("Int32")]
        public int? SoLuong { get; set; }
        [UIHint("Int32")]
        public int? DonViTinh { get; set; }
        [UIHint("Money")]
        public Decimal? Sale { get; set; }
        [UIHint("Money")]
        public Decimal? DonGia { get; set; }
    }
}

