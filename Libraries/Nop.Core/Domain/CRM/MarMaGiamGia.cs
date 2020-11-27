//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace Nop.Core.Domain.CRM
{
    public partial class MarMaGiamGia : BaseEntity
    {
        public String MA { get; set; }
        public DateTime? NGAY_SU_DUNG { get; set; }
        public Int32? KHACH_HANG_ID { get; set; }
        public Int32? DOANH_NGHIEP_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Int32? NGUOI_TAO { get; set; }
        public Int32? MAR_DIEU_KIEN_ID { get; set; }
        public Int32? MARKETING_ID { get; set; }
    }
}



