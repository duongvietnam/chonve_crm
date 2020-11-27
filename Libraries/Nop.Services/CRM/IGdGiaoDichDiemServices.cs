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
    public partial interface IGdGiaoDichDiemService 
    {    
    #region GdGiaoDichDiem
        IList<GdGiaoDichDiem> GetAllGdGiaoDichDiems(int StoreId=0);
        IPagedList <GdGiaoDichDiem> SearchGdGiaoDichDiems(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        GdGiaoDichDiem GetGdGiaoDichDiemById(int Id);
        IList<GdGiaoDichDiem> GetGdGiaoDichDiemByIds(int[] Ids);
        void InsertGdGiaoDichDiem(GdGiaoDichDiem entity);
        void UpdateGdGiaoDichDiem(GdGiaoDichDiem entity);
        void DeleteGdGiaoDichDiem(GdGiaoDichDiem entity);    
     #endregion
	}
}
