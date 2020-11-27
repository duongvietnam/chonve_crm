//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 4/11/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.CRM;


namespace Nop.Services.CRM
{
    public partial interface IChThongTinMoRongService 
    {    
    #region ChThongTinMoRong
        IList<ChThongTinMoRong> GetAllChThongTinMoRongs(int StoreId=0);
        IPagedList <ChThongTinMoRong> SearchChThongTinMoRongs(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        ChThongTinMoRong GetChThongTinMoRongById(int Id);
        IList<ChThongTinMoRong> GetChThongTinMoRongByIds(int[] Ids);
        void InsertChThongTinMoRong(ChThongTinMoRong entity);
        void UpdateChThongTinMoRong(ChThongTinMoRong entity);
        void DeleteChThongTinMoRong(ChThongTinMoRong entity);    
     #endregion
	}
}
