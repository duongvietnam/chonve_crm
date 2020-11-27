//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 10/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.CRM;


namespace Nop.Services.CRM
{
    public partial interface IKhNhomHeThongService 
    {    
    #region KhNhomHeThong
        IList<KhNhomHeThong> GetAllKhNhomHeThongs(int StoreId=0);
        IPagedList <KhNhomHeThong> SearchKhNhomHeThongs(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        KhNhomHeThong GetKhNhomHeThongById(int Id);
        IList<KhNhomHeThong> GetKhNhomHeThongByIds(int[] Ids);
        void InsertKhNhomHeThong(KhNhomHeThong entity);
        void UpdateKhNhomHeThong(KhNhomHeThong entity);
        void DeleteKhNhomHeThong(KhNhomHeThong entity);    
     #endregion
	}
}
