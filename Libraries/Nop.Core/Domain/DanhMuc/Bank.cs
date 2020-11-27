//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 31/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace Nop.Core.Domain.DanhMuc
{
	public partial class Bank :BaseEntity
	{
		public String Code {get;set;}
		public String Name {get;set;}
		public String ShortName { get; set; }		
		public Int32 OrderDisplay {get;set;}
		public String CodePlus {get;set;}
		
	}
}
