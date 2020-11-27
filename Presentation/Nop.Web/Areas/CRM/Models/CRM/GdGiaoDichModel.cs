//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.CRM;
using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

namespace Nop.Web.Areas.CRM.Models.CRM
{

    public class GdGiaoDichModel : BaseNopEntityModel
    {
        public GdGiaoDichModel()
        {
            ListGiaoDichSubModels = new List<GiaoDichSubModel>();
        }
        public Int32? LOAI_ID { get; set; }
        public Int32? MA_GIAM_GIA_ID { get; set; }
        public Int32? EVENT_ID { get; set; }
        public Decimal? THANH_TIEN { get; set; }
        [UIHint("Date")]
        public DateTime? NGAY_GIAO_DICH { get; set; }
        public DateTime? NGAY_BAT_DAU { get; set; }
        public DateTime? NGAY_KET_THUC { get; set; }
        public Int32? HINH_THUC_THANH_TOAN { get; set; }
        public Int32? TRANG_THAI { get; set; }
        public Int32? DOANH_NGHIEP_ID { get; set; }
        public String GHI_CHU { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Int32? NGUOI_TAO { get; set; }
        // more
        public IList<GiaoDichSubModel> ListGiaoDichSubModels { get; set; }
        public string KhachHangSrting { get; set; }
        public string ThanhTienString { get; set; }
        public string NgayGiaoDich { get; set; }
        public string EnumHinhThucThanhToanValue { get; set; }
        public string EnumTrangThaiGiaoDichValue { get; set; }
    }
    public partial class GdGiaoDichSearchModel : BaseSearchModel
    {
        public GdGiaoDichSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class GdGiaoDichListModel : BasePagedListModel<GdGiaoDichModel>
    {

    }
    public class GiaoDichSubModel
    {
        public GiaoDichSubModel()
        {
            DDLDichVu = new List<SelectListItem>();
        }
        public int DichVuId { get; set; }
        public decimal SoLuong { get; set; }
        public string ThanhTienSub { get; set; }
        public string DonViTinh { get; set; }
        public IList<SelectListItem> DDLDichVu { get; set; }
    }

    public class ChartGiaoDichModel
    {
        public string Time { get; set; }
        public Decimal Amount { get; set; }
        public Decimal Quantity { get; set; }
    }
    public class ChartDichVuModel
    {
        public string TenDichVu { get; set; }
        public int IdDichVu { get; set; }
        public Decimal DoanhThu { get; set; }
        public Decimal SoLuong { get; set; }
    }
    public class ChartGiaoDichNTModel
    {
        public string Label { get; set; }
        public List<ChartGiaoDichModel> GiaoDichChildren { get; set; }
    }
}

