//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Web.Framework.Models;
using System;

namespace Nop.Web.Areas.CRM.Models.CRM
{

    public class MarDoanhThuMarketingModel : BaseNopEntityModel
    {
        public MarDoanhThuMarketingModel()
        {

        }
        public Int32? EVENT_ID { get; set; }
        public DateTime? NGAY_EVENT { get; set; }
        public Int32? LUOT_KHACH { get; set; }
        public Decimal? DOANH_THU { get; set; }
        public Int32? DOANH_NGHIEP_ID { get; set; }
        // more
        public string NgaySuKien { get; set; }
        public string DoanhThuString { get; set; }
    }
    public partial class MarDoanhThuMarketingSearchModel : BaseSearchModel
    {
        public MarDoanhThuMarketingSearchModel()
        {
        }
        public string KeySearch { get; set; }
        public int MarEventId { get; set; }
    }
    public partial class MarDoanhThuMarketingListModel : BasePagedListModel<MarDoanhThuMarketingModel>
    {

    }
}

