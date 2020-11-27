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
	public partial class DvDichVuCombo :BaseEntity
	{
		public Int32? DICH_VU_COMBO {get;set;}
		public Int32? DICH_VU_ID {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public Int32? NGUOI_TAO {get;set;}
		public Int32? SO_LUONG { get; set; }
	}
}



