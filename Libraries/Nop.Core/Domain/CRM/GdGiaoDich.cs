//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace Nop.Core.Domain.CRM
{
    public partial class GdGiaoDich :BaseEntity
	{
		public Int32? LOAI_ID {get;set;}
		public Int32? MA_GIAM_GIA_ID {get;set;}
		public Int32? EVENT_ID {get;set;}
		public Decimal? THANH_TIEN {get;set;}
		public DateTime? NGAY_GIAO_DICH {get;set;}
		public DateTime? NGAY_BAT_DAU {get;set;}
		public DateTime? NGAY_KET_THUC {get;set;}
		public Int32? HINH_THUC_THANH_TOAN {get;set;}
		public Int32? TRANG_THAI {get;set;}
		public Int32? DOANH_NGHIEP_ID {get;set;}
		public String GHI_CHU {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public Int32? NGUOI_TAO {get;set; }
		public String MA_GIAO_DICH { get; set; }
	}

	public enum EnumLoaiGiaoDich
    {
		[Description("Tất cả")]
		TatCa = 0,
		[Description("Giao dịch thanh toán")]
		GiaoDichThanhToan = 1,
		[Description("Giao dịch điểm")]
		GiaoDichThayDoiDiem = 2
    }

	public enum EnumTrangThaiGiaoDich
	{
		[Description("Tất cả")]
		TatCa = 0,
		[Description("Đã book")]
		Booking = 1,
		[Description("Đang sử dụng")]
		DangSuDung = 2,
		[Description("Sử dụng xong")]
		SuDungXong = 3,
		[Description("Đã thanh toán")]
		DaThanhToan = 4,
		[Description("Đã hủy")]
		DaHuy = 5
    }

	public enum EnumHinhThucThanhToan
	{
		[Description("Tất cả")]
		TatCa = 0,
		[Description("Tiền mặt")]
		TienMat = 1,
		[Description("Chuyển khoản")]
		ChuyenKhoan = 2,
		[Description("Quẹt thẻ")]
		QuetThe = 3,
		[Description("Điểm tích lũy")]
		DiemTichLuy = 4
    }
}



