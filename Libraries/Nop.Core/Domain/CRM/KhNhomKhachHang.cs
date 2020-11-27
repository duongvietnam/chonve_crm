//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace Nop.Core.Domain.CRM
{
	public partial class KhNhomKhachHang :BaseEntity
	{
		public String TEN {get;set;}
		public Int32? DOANH_NGHIEP_ID {get;set;}
		
	}
}



