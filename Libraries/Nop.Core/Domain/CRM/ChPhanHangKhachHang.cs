//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 2/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace Nop.Core.Domain.CRM
{
    public partial class ChPhanHangKhachHang :BaseEntity
	{
		public String TEN {get;set;}
		public String MA {get;set;}
		public Int32? GIA_TRI_CAP_1 {get;set;}
		public Int32? GIA_TRI_CAP_2 {get;set;}
		public Int32? GIA_TRI_CAP_3 {get;set;}
		public String TEN_CAP_1 {get;set;}
		public String TEN_CAP_2 {get;set;}
		public String TEN_CAP_3 {get;set;}
		public Int32? ACTIVE {get;set;}
		public String MO_TA {get;set;}
        public Int32? DOANH_NGHIEP { get; set; }
    }
}



