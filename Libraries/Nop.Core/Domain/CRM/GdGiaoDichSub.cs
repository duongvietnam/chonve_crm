//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace Nop.Core.Domain.CRM
{
    public partial class GdGiaoDichSub :BaseEntity
	{
		public Int32? GIAO_DICH_ID {get;set;}
		public Decimal? SO_LUONG {get;set;}
		public Int32? DICH_VU_ID {get;set;}
		public Int32? DON_VI_TINH {get;set;}
		public Decimal? SO_TIEN {get;set;}
		public Int32? DIEM_DICH_VU {get;set;}
        public Int32 DOANH_NGHIEP_ID { get; set; }
    }
}



