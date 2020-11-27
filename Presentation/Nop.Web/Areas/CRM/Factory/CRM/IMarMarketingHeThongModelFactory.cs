//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 19/10/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
using System.Collections.Generic;

namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IMarMarketingHeThongModelFactory 
    {    
        #region MarMarketingHeThong
    
        MarMarketingHeThongSearchModel PrepareMarMarketingHeThongSearchModel(MarMarketingHeThongSearchModel searchModel);
        
        MarMarketingHeThongListModel PrepareMarMarketingHeThongListModel(MarMarketingHeThongSearchModel searchModel);
        
        MarMarketingHeThongModel PrepareMarMarketingHeThongModel(MarMarketingHeThongModel model, MarMarketingHeThong item);
        
        void PrepareMarMarketingHeThong(MarMarketingHeThongModel model, MarMarketingHeThong item);

        IList<SelectListItem> PrepareMultiSelectMarHethong(IList<int> valSelected = null, bool isAddFirst = false, string strFirstRow = "-- Chọn marketing hệ thống --", string valueFirstRow = "");
        #endregion
    }
}
