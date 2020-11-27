//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.BaoCao;
using Nop.Core.Domain.CRM;


namespace Nop.Services.CRM
{
    public partial interface IGdGiaoDichService 
    {    

    #region GdGiaoDich
        IList<GdGiaoDich> GetAllGdGiaoDichs(int StoreId=0);
        IPagedList <GdGiaoDich> SearchGdGiaoDichs(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        GdGiaoDich GetGdGiaoDichById(int Id);
        GdGiaoDich GetGiaoDichByMaNguon(string MaNguon, int DoanhNghiepId);
        IList<GdGiaoDich> GetGdGiaoDichByIds(int[] Ids);
        Decimal GetTongSoTienDaChiTieu(int khachHangId, DateTime? TuNgay = null, DateTime? DenNgay = null);
        IList<GdGiaoDich> GetGiaoDichs(int khachHangId = 0);
        Decimal GetSoTienDaChiTieu(int khachHangId, DateTime? TuNgay, DateTime? DenNgay);
        IList<GdGiaoDich> GetTopGiaoDichByTime(int StoreId,int SoGiaoDich = 8);
        IList<BieuDo> GetThongKeGiaoDich(int StoreId, int SoNam, bool Thang, bool Ngay, bool Gio);
        IList<BieuDo> GetThongKeSoSanhGiaoDichGioNgayThang(int StoreId, int SoMoc, int Nam, int Thang, bool weekday);
        IList<BieuDo> GetThongKeBaoCaoGiaoDichCungKy(int StoreId, int Nam, int Thang, int Ngay, int Tuan);
        void InsertGdGiaoDich(GdGiaoDich entity);
        void UpdateGdGiaoDich(GdGiaoDich entity);
        void DeleteGdGiaoDich(GdGiaoDich entity);
        #endregion
        #region Bao c?o th?ng k?
        IList<BieuDo> GetThongKeDoanhThu(int StoreId,int Nam,int Thang, int Ngay);
        IList<BieuDo> GetThongKeChiTieuKhachHang(int StoreId,int KhId,int Nam,int Thang, int Ngay);
        IList<BieuDo1> GetThongKeDoanhThuDichVu(int StoreId, int Nam, int Thang, int Ngay);
        #endregion
    }
}
