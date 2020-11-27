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
	public partial class KhNhomKhachHangMap :BaseEntity
	{
		public Int32? KHACH_HANG_ID {get;set;}
		public Int32? NHOM_KHACH_HANG_ID {get;set;}
		public Int32? DIEM_DANH_GIA {get;set;}
		
	}
}



