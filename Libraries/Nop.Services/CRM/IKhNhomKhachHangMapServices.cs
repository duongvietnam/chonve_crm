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
    public partial interface IKhNhomKhachHangMapService 
    {    
    #region KhNhomKhachHangMap
        IList<KhNhomKhachHangMap> GetAllKhNhomKhachHangMaps(int StoreId=0);
        IPagedList <KhNhomKhachHangMap> SearchKhNhomKhachHangMaps(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        KhNhomKhachHangMap GetKhNhomKhachHangMapById(int Id);
        IList<KhNhomKhachHangMap> GetKhNhomKhachHangMapByIds(int[] Ids);
        IList<KhNhomKhachHangMap> GetKhNhomKhachHangMapByIdKhachHang(int IdKhachHang);
        IList<BieuDo> ThongKeSoLuongKhByNhomKH(int StoreId);
        IList<int> GetKhachHangIdsByNhomId(IList<int> listNhomId);
        void InsertKhNhomKhachHangMap(KhNhomKhachHangMap entity);
        void UpdateKhNhomKhachHangMap(KhNhomKhachHangMap entity);
        void DeleteKhNhomKhachHangMap(KhNhomKhachHangMap entity);    
     #endregion
	}
}
