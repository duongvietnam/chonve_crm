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
    public partial interface IMarMarketingDieuKienService
    {
        #region MarMarketingDieuKien
        IList<MarMarketingDieuKien> GetAllMarMarketingDieuKiens(int StoreId = 0);
        IPagedList<MarMarketingDieuKien> SearchMarMarketingDieuKiens(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue);
        MarMarketingDieuKien GetMarMarketingDieuKienById(int Id);
        IList<MarMarketingDieuKien> GetMarMarketingDieuKienByIds(int[] Ids);
        IList<MarMarketingDieuKien> GetMarMarketingDieuKiens(int MarId = 0);
        void InsertMarMarketingDieuKien(MarMarketingDieuKien entity);
        void UpdateMarMarketingDieuKien(MarMarketingDieuKien entity);
        void DeleteMarMarketingDieuKien(MarMarketingDieuKien entity);
        void DeleteMarMarketingDieuKiens(IList<MarMarketingDieuKien> entity);
        #endregion
    }
}
