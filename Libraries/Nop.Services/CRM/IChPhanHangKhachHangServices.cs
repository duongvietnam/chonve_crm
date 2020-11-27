//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 2/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.CRM;


namespace Nop.Services.CRM
{
    public partial interface IChPhanHangKhachHangService 
    {    
    #region ChPhanHangKhachHang
        IList<ChPhanHangKhachHang> GetAllChPhanHangKhachHangs(int StoreId=0);
        IPagedList <ChPhanHangKhachHang> SearchChPhanHangKhachHangs(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        ChPhanHangKhachHang GetChPhanHangKhachHangById(int Id);
        IList<ChPhanHangKhachHang> GetChPhanHangKhachHangByIds(int[] Ids);
        ChPhanHangKhachHang GetChPhanHangKhachHangActive(int DoanhNghiepId);
        void InsertChPhanHangKhachHang(ChPhanHangKhachHang entity);
        void UpdateChPhanHangKhachHang(ChPhanHangKhachHang entity);
        void DeleteChPhanHangKhachHang(ChPhanHangKhachHang entity);    
     #endregion
	}
}
