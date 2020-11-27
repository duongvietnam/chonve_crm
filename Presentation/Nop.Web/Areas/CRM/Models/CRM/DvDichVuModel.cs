//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Web.Areas.CRM.Models.CRM
{

    public class DvDichVuModel : BaseNopEntityModel
    {
        public DvDichVuModel()
        {
            DDLLoaiDichVu = new List<SelectListItem>();
            DDLNhomDichVu = new List<SelectListItem>();
            comBoDichVuModels = new List<ComBoDichVuModel>();
            DDLDonViTinh = new List<SelectListItem>();
        }
        public String TEN { get; set; }
        public Int32? DOANH_NGHIEP_ID { get; set; }
        [UIHint("Int32")]
        public Int32? DIEM_DICH_VU { get; set; }
        public Int32? TRANG_THAI { get; set; }
        public Int32? IS_COMBO { get; set; }
        public Int32? NHOM_ID { get; set; }
        public Int32? LOAI_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Int32? NGUOI_TAO { get; set; }
        public Int32 DON_VI_TINH { get; set; }
        // more
        [UIHint("Money")]
        public Decimal? GiaBanBuon { get; set; }
        [UIHint("Money")]
        public Decimal? GiaBanLe { get; set; }
        public IList<SelectListItem> DDLLoaiDichVu { get; set; }
        public IList<SelectListItem> DDLNhomDichVu { get; set; }
        public IList<SelectListItem> DDLDonViTinh { get; set; }
        public String NhomDichVuString { get; set; }
        public String LoaiDichVuString { get; set; }
        public String ListDichVuInCombo { get; set; }
        public IList<ComBoDichVuModel> comBoDichVuModels { get; set; }
        public string EnumTrangThaiDichVuValue { get; set; }
    }
    public partial class DvDichVuSearchModel : BaseSearchModel
    {
        public DvDichVuSearchModel()
        {
        }
        public string KeySearch { get; set; }
        public bool IsCombo { get; set; }
    }
    public partial class DvDichVuListModel : BasePagedListModel<DvDichVuModel>
    {

    }
    public partial class ComBoDichVuModel
    {
        public ComBoDichVuModel()
        {
            DDLDichVuCombo = new List<SelectListItem>();
        }
        public int? DichVuComboId { get; set; }
        public int? DichVuId { get; set; }
        public int? SoLuong { get; set; }
        public IList<SelectListItem> DDLDichVuCombo { get; set; }
    }
}

