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
    public partial interface IChGiaTriThongTinMoRongService 
    {    
    #region ChGiaTriThongTinMoRong
        IList<ChGiaTriThongTinMoRong> GetAllChGiaTriThongTinMoRongs(int StoreId=0);
        IPagedList <ChGiaTriThongTinMoRong> SearchChGiaTriThongTinMoRongs(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        ChGiaTriThongTinMoRong GetChGiaTriThongTinMoRongById(int Id);
        IList<ChGiaTriThongTinMoRong> GetChGiaTriThongTinMoRongByIds(int[] Ids);
        void InsertChGiaTriThongTinMoRong(ChGiaTriThongTinMoRong entity);
        void UpdateChGiaTriThongTinMoRong(ChGiaTriThongTinMoRong entity);
        void DeleteChGiaTriThongTinMoRong(ChGiaTriThongTinMoRong entity);    
     #endregion
	}
}
