//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 2/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace Nop.Core.Domain.CRM
{
    public partial class KhThongKeSuDungDichVu :BaseEntity
	{
		public Int32? KHACH_HANG_ID {get;set;}
		public String TOP_DICH_VU {get;set;}
		public String TOP_DICH_VU_30DAYS {get;set;}
		public String TOP_DICH_VU_60DAYS {get;set;}
		public String TOP_DICH_VU_90DAYS {get;set;}
		public String TOP_DICH_VU_LE_TET {get;set;}
		public Int32? DOANH_NGHIEP_ID { get; set; }
	}
}



