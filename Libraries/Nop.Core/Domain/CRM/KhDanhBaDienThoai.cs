//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace Nop.Core.Domain.CRM
{
    public partial class KhDanhBaDienThoai :BaseEntity
	{
		public Int32? KHACH_HANG_ID {get;set;}
		public String SO_DIEN_THOAI {get;set;}
		public Int32? NHOM_DANH_BA {get;set;}
		public Int32? TRANG_THAI {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public Int32? NGUOI_TAO {get;set;}
	}

	public enum EnumNhomDanhBa
	{
		[Description("Tất cả")]
		TatCa = 0,
		[Description("Số chính")]
		SoChinh = 1,
		[Description("Số phụ")]
		SoPhu = 2
    }

	public enum EnumTrangThaiDanhBa
	{
		[Description("Tất cả")]
		TatCa = 0,
		[Description("Hoạt động")]
		HoatDong = 1
    }
}



