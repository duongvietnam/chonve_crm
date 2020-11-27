//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 26/11/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace Nop.Core.Domain.CRM
{
    public partial class CdChuyenDi : BaseEntity
    {
        public String MA { get; set; }
        public String BIEN_SO_XE { get; set; }
        public Int32? DICH_VU_ID { get; set; }
        public String TEN_LOAI_XE { get; set; }
        public String TEN_LAI_XE { get; set; }
        public Int32? SO_KHACH { get; set; }
        public Int32? SO_GHE { get; set; }
        public Int32? DOANH_NGHIEP_ID { get; set; }
        public String NGAY_DI { get; set; }
    }
}



