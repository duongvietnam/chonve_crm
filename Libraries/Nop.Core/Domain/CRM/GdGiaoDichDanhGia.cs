//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace Nop.Core.Domain.CRM
{
    public partial class GdGiaoDichDanhGia :BaseEntity
	{
		public Int32? GIAO_DICH_ID {get;set;}
		public Int32? SO_DIEM {get;set;}
		public Int32? HANG_MUC {get;set;}
		public Int32? DOANH_NGHIEP_ID {get;set;}
		public String GHI_CHU {get;set;}
		public int KHACH_HANG_ID { get; set; }
	}
}



