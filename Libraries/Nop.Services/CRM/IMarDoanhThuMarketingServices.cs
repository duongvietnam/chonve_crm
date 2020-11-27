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
    public partial interface IMarDoanhThuMarketingService
    {
        #region MarDoanhThuMarketing
        IList<MarDoanhThuMarketing> GetAllMarDoanhThuMarketings(int StoreId = 0);
        IPagedList<MarDoanhThuMarketing> SearchMarDoanhThuMarketings(int MarEventId, int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue);
        MarDoanhThuMarketing GetMarDoanhThuMarketingById(int Id);
        IList<MarDoanhThuMarketing> GetMarDoanhThuMarketingByIds(int[] Ids);
        IList<MarDoanhThuMarketing> GetMarDoanhThuMarketings(int EventMarId = 0);
        void InsertMarDoanhThuMarketing(MarDoanhThuMarketing entity);
        void UpdateMarDoanhThuMarketing(MarDoanhThuMarketing entity);
        void DeleteMarDoanhThuMarketing(MarDoanhThuMarketing entity);
        #endregion
    }
}
