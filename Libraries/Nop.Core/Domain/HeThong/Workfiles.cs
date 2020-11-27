//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 10/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using Nop.Core.Domain.Customers;

namespace Nop.Core.Domain.HeThong
{
	public partial class WorkFiles :BaseEntity
	{
		public Guid Guid {get;set;}
		public String Ten {get;set;}
		public Int32 LoaiFileId {get;set;}
		public Int32 CustomerId {get;set;}
		public DateTime NgayTao {get;set;}
		public String DuoiFile {get;set;}
		public String DataJson {get;set;}
		public Boolean IsDeleted {get;set;}
		public decimal KichThuoc { get;set;}
		public String LoaiNoiDung { get; set; }

	}
}



