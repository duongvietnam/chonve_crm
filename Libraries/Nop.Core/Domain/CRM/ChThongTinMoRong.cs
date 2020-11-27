//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 4/11/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace Nop.Core.Domain.CRM
{
    public partial class ChThongTinMoRong : BaseEntity
    {
        public String TEN { get; set; }
        public String MA { get; set; }
        public Int32 DOANH_NGHIEP_ID { get; set; }
        public String MO_TA { get; set; }
        public Int32? KIEU_DU_LIEU { get; set; }

    }

    public enum EnumKieuDuLieu
    {
        String = 1,
        Int = 2,
        DateTime = 3
    }
}



