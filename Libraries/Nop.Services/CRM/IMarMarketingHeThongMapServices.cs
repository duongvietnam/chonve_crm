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
    public partial interface IMarMarketingHeThongMapService 
    {    
    #region MarMarketingHeThongMap
        IList<MarMarketingHeThongMap> GetAllMarMarketingHeThongMaps(int StoreId=0);
        IPagedList <MarMarketingHeThongMap> SearchMarMarketingHeThongMaps(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        MarMarketingHeThongMap GetMarMarketingHeThongMapById(int Id);
        IList<MarMarketingHeThongMap> GetMarMarketingHeThongMapByIds(int[] Ids);
        IList<MarMarketingHeThongMap> GetMarketingHeThongMaps(int MarId = 0);
        void InsertMarMarketingHeThongMap(MarMarketingHeThongMap entity);
        void UpdateMarMarketingHeThongMap(MarMarketingHeThongMap entity);
        void DeleteMarMarketingHeThongMap(MarMarketingHeThongMap entity);
        void DeleteMarMarketingHeThongMapByMarId(int MarId);
     #endregion
    }
}
