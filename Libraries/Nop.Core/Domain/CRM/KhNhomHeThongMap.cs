//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace Nop.Core.Domain.CRM
{
    public partial class KhNhomHeThongMap :BaseEntity
	{
		public Int32? NHOM_KHACH_HANG_ID {get;set;}
		public Int32? NHOM_KHACH_HANG_HE_THONG {get;set;}
		public Int32? DOANH_NGHIEP_ID { get; set; }
	}
}



