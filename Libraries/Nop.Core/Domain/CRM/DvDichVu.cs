//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace Nop.Core.Domain.CRM
{
    public partial class DvDichVu :BaseEntity
	{
		public String TEN {get;set;}
		public Int32? DOANH_NGHIEP_ID {get;set;}
		public Int32? DIEM_DICH_VU {get;set;}
		public Int32? TRANG_THAI {get;set;}
		public Int32? IS_COMBO {get;set;}
		public Int32? NHOM_ID {get;set;}
		public Int32? LOAI_ID {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public Int32? NGUOI_TAO {get;set;}
        public Int32 DON_VI_TINH { get; set; }
        public Int32 ID_NGUON { get; set; }
    }

	public enum EnumTrangThaiDichVu
	{
		[Description("Tất cả")]
		TatCa = 0,
		[Description("Hoạt động")]
		HoatDong = 1,
		[Description("Đã xóa")]
		DaXoa = 2
    }
}



