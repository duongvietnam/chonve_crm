//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 19/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.CRM;


namespace Nop.Services.CRM
{
    public partial interface IMarMarketingHeThongService 
    {    
    #region MarMarketingHeThong
        IList<MarMarketingHeThong> GetAllMarMarketingHeThongs(int StoreId=0);
        IPagedList <MarMarketingHeThong> SearchMarMarketingHeThongs(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        MarMarketingHeThong GetMarMarketingHeThongById(int Id);
        IList<MarMarketingHeThong> GetMarMarketingHeThongByIds(int[] Ids);
        MarMarketingHeThong GetMarMarketingHeThongByMa(string ma);
        void InsertMarMarketingHeThong(MarMarketingHeThong entity);
        void UpdateMarMarketingHeThong(MarMarketingHeThong entity);
        void DeleteMarMarketingHeThong(MarMarketingHeThong entity);    
     #endregion
	}
}
