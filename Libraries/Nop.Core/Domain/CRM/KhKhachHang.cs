//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.ComponentModel;
using Nop.Core.Domain.Common;
namespace Nop.Core.Domain.CRM
{
    public partial class KhKhachHang :BaseEntity
	{
		public String TEN {get;set;}
		public DateTime? NGAY_SINH {get;set;}
		public Int32? GIOI_TINH {get;set;}
		public EnumGioiTinh gioiTinh
		{
			get
			{
				return (EnumGioiTinh)GIOI_TINH.GetValueOrDefault(0);

			}
			set
			{
				GIOI_TINH = (int)value;

			}
		}
		public String EMAIL {get;set;}
		public String FACEBOOK {get;set;}
		public String ZALO {get;set;}
		public Int32? LOAI_KHACH_HANG {get;set;}
		public EnumLoaiKhachHang loaiKhachHang
        {
			get
            {
				return (EnumLoaiKhachHang)LOAI_KHACH_HANG.GetValueOrDefault(0);

			}
			set
            {
				LOAI_KHACH_HANG = (int)value;

			}
        }
		public Int32? DOANH_NGHIEP_ID {get;set;}
		public Int32? DIEM_DICH_VU {get;set;}
		public Int32? DIEM_TICH_LUY {get;set;}
        public Int32 DIEM_PHAN_HANG { get; set; }
        public DateTime? NGAY_TAO {get;set;}
		public Int32? NGUOI_TAO {get;set;}
		public Int32? TRANG_THAI { get; set; }
		public EnumTrangThaiKhachHang trangThaiKhachHang
		{
			get
			{
				return (EnumTrangThaiKhachHang)TRANG_THAI.GetValueOrDefault(0);

			}
			set
			{
				TRANG_THAI = (int)value;

			}
		}
		public Int32? DA_QUET { get; set; }
	}

	public enum EnumLoaiKhachHang
	{
		TatCa = 0,
		CaNhan = 1,
		ToChuc = 2,
		DoanhNghiep = 3
    }

	public enum EnumTrangThaiKhachHang
	{
		TatCa = 0,
		DaXoa = 1,
		HoatDong = 2
    }

	public enum EnumSapXepHangKhachHang
    {
		SapXepGiamDan = 0,
		SapXepTangDan = 1
	}
}



