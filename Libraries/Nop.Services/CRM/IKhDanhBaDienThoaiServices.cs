//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.CRM;


namespace Nop.Services.CRM
{
    public partial interface IKhDanhBaDienThoaiService
    {
        #region KhDanhBaDienThoai
        IList<KhDanhBaDienThoai> GetAllKhDanhBaDienThoais(int StoreId = 0);
        IPagedList<KhDanhBaDienThoai> SearchKhDanhBaDienThoais(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue);
        KhDanhBaDienThoai GetKhDanhBaDienThoaiById(int Id);
        KhDanhBaDienThoai GetKhDanhBaDienThoaiBySoDienThoai(string soDienThoai, int DoanhNghiepId);
        IList<KhDanhBaDienThoai> GetKhDanhBaDienThoaiByIds(int[] Ids);
        IList<KhDanhBaDienThoai> GetKhDanhBaDienThoaiByKhachHang(int khachHangId, EnumTrangThaiDanhBa trangThai = EnumTrangThaiDanhBa.HoatDong);
        void InsertKhDanhBaDienThoai(KhDanhBaDienThoai entity);
        void UpdateKhDanhBaDienThoai(KhDanhBaDienThoai entity);
        void DeleteKhDanhBaDienThoai(KhDanhBaDienThoai entity);
        #endregion
    }
}
