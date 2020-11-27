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

    public class MarEventMarketingModel : BaseNopEntityModel
    {
        public MarEventMarketingModel()
        {
            DDLMarketing = new List<SelectListItem>();
            doanhThuSearchModel = new MarDoanhThuMarketingSearchModel();
        }
        public Int32? MARKETING_ID { get; set; }
        public Int32? DOANH_NGHIEP_ID { get; set; }
        [UIHint("DateTime")]
        public DateTime? THOI_GIAN_BAT_DAU { get; set; }
        [UIHint("DateTime")]
        public DateTime? THOI_GIAN_KET_THUC { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Int32? NGUOI_TAO { get; set; }
        public String TEN { get; set; }
        // more
        public IList<SelectListItem> DDLMarketing { get; set; }
        public String TenMarketing { get; set; }
        public String BatDau { get; set; }
        public String KetThuc { get; set; }
        public MarDoanhThuMarketingSearchModel doanhThuSearchModel { get; set; }
    }
    public partial class MarEventMarketingSearchModel : BaseSearchModel
    {
        public MarEventMarketingSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class MarEventMarketingListModel : BasePagedListModel<MarEventMarketingModel>
    {

    }
}

