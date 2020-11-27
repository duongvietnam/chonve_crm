//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace Nop.Core.Domain.CRM
{
    public partial class KhThongTinMoRong :BaseEntity
	{
		public Int32? KHACH_HANG_ID {get;set;}
		public Int32? CAU_HINH_ID {get;set;}
		public String VALUE {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public Int32? NGUOI_TAO {get;set;}
		
	}
}



