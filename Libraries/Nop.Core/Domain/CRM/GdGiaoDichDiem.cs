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
	public partial class GdGiaoDichDiem :BaseEntity
	{
		public Int32? GIAO_DICH_ID {get;set;}
		public Int32? DIEM_DICH_VU {get;set;}
		public Int32? DIEM_TICH_LUY {get;set;}
		public Int32? KHACH_HANG_ID {get;set;}
		public Int32? CONG_TRU {get;set;}
		
	}
}



