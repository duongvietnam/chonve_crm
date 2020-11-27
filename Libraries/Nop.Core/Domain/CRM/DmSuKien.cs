//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 2/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace Nop.Core.Domain.CRM
{
	public partial class DmSuKien :BaseEntity
	{
		public String TEN {get;set;}
		public String MA {get;set;}
		public DateTime? NGAY_BAT_DAU {get;set;}
		public DateTime? NGAY_KET_THUC {get;set;}
		public Int32? DOANH_NGHIEP_ID {get;set;}
		
	}
}



