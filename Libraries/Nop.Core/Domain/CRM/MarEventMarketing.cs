//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace Nop.Core.Domain.CRM
{
    public partial class MarEventMarketing : BaseEntity
    {
        public Int32? MARKETING_ID { get; set; }
        public Int32? DOANH_NGHIEP_ID { get; set; }
        public DateTime? THOI_GIAN_BAT_DAU { get; set; }
        public DateTime? THOI_GIAN_KET_THUC { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Int32? NGUOI_TAO { get; set; }
        public String TEN { get; set; }
    }
}
