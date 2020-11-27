//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace Nop.Core.Domain.CRM
{
    public partial class MarMarketingDieuKien : BaseEntity
    {
        public Int32? MARKETING_ID { get; set; }
        public Int32? DICH_VU_ID { get; set; }
        public DateTime? TU_NGAY { get; set; }
        public DateTime? DEN_NGAY { get; set; }
        public TimeSpan? TU_GIO { get; set; }
        public TimeSpan? DEN_GIO { get; set; }
        public Int32? SO_LUONG { get; set; }
        public Int32? DON_VI_TINH { get; set; }
        public Decimal? SALE { get; set; }
        public Decimal? DON_GIA { get; set; }
        public String DICH_VU_JSON { get; set; }
        public int? HANG_KHACH_HANG { get; set; }
    }
}