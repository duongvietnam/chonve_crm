//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core;
using Nop.Core.Domain.CRM;
using System.Collections.Generic;


namespace Nop.Services.CRM
{
    public partial interface IMarEventMarketingService
    {
        #region MarEventMarketing
        IList<MarEventMarketing> GetAllMarEventMarketings(int StoreId = 0);
        IPagedList<MarEventMarketing> SearchMarEventMarketings(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue);
        MarEventMarketing GetMarEventMarketingById(int Id);
        IList<MarEventMarketing> GetMarEventMarketingByIds(int[] Ids);
        MarEventMarketing GetEventMarketing(int marId);
        void InsertMarEventMarketing(MarEventMarketing entity);
        void UpdateMarEventMarketing(MarEventMarketing entity);
        void DeleteMarEventMarketing(MarEventMarketing entity);
        void DeleteMarEventMarketingByMarId(int marId);
        #endregion
    }
}
