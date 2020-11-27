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
    public partial interface IGdGiaoDichSubService 
    {    
    #region GdGiaoDichSub
        IList<GdGiaoDichSub> GetAllGdGiaoDichSubs(int StoreId=0);
        IPagedList <GdGiaoDichSub> SearchGdGiaoDichSubs(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        GdGiaoDichSub GetGdGiaoDichSubById(int Id);
        IList<GdGiaoDichSub> GetGdGiaoDichSubByIds(int[] Ids);
        IList<GdGiaoDichSub> GetGdGiaoDichSubs(int giaoDichId = 0);
        void InsertGdGiaoDichSub(GdGiaoDichSub entity);
        void UpdateGdGiaoDichSub(GdGiaoDichSub entity);
        void DeleteGdGiaoDichSub(GdGiaoDichSub entity);    
     #endregion
	}
}
