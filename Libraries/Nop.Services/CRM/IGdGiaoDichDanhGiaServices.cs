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
    public partial interface IGdGiaoDichDanhGiaService 
    {    
    #region GdGiaoDichDanhGia
        IList<GdGiaoDichDanhGia> GetAllGdGiaoDichDanhGias(int StoreId=0);
        IPagedList <GdGiaoDichDanhGia> SearchGdGiaoDichDanhGias(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        GdGiaoDichDanhGia GetGdGiaoDichDanhGiaById(int Id);
        IList<GdGiaoDichDanhGia> GetGdGiaoDichDanhGiaByIds(int[] Ids);
        void InsertGdGiaoDichDanhGia(GdGiaoDichDanhGia entity);
        void UpdateGdGiaoDichDanhGia(GdGiaoDichDanhGia entity);
        void DeleteGdGiaoDichDanhGia(GdGiaoDichDanhGia entity);    
     #endregion
	}
}
