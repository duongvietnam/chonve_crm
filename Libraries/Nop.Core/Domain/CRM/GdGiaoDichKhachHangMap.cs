//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace Nop.Core.Domain.CRM
{
    public partial class GdGiaoDichKhachHangMap : BaseEntity
    {
        public Int32? KHACH_HANG_ID { get; set; }
        public Int32? GIAO_DICH_ID { get; set; }
        public Int32? DICH_VU_ID { get; set; }
        public Int32? KHACH_HANG_CHINH { get; set; }
        public Int32? DOANH_NGHIEP_ID { get; set; }
        public String DIEM_DON { get; set; }
        public String DIEM_TRA { get; set; }
        public Int32? CHUYEN_DI_ID { get; set; }
    }
}



