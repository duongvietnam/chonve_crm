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
    public partial interface IKhNhomKhachHangService 
    {    
    #region KhNhomKhachHang
        IList<KhNhomKhachHang> GetAllKhNhomKhachHangs(int StoreId=0);
        IPagedList <KhNhomKhachHang> SearchKhNhomKhachHangs(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        KhNhomKhachHang GetKhNhomKhachHangById(int Id);
        IList<KhNhomKhachHang> GetKhNhomKhachHangByIds(int[] Ids);
        int GetCountNhomKhachHang(string ten);
        void InsertKhNhomKhachHang(KhNhomKhachHang entity);
        void UpdateKhNhomKhachHang(KhNhomKhachHang entity);
        void DeleteKhNhomKhachHang(KhNhomKhachHang entity);    
     #endregion
	}
}
