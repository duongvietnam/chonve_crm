//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace Nop.Core.Domain.CRM
{
    public partial class MarDoanhThuMarketing :BaseEntity
	{
		public Int32? EVENT_ID {get;set;}
		public DateTime? NGAY_EVENT {get;set;}
		public Int32? LUOT_KHACH {get;set;}
		public Decimal? DOANH_THU {get;set;}
		public Int32? DOANH_NGHIEP_ID {get;set;}
		
	}
}



