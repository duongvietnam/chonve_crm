//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
using System.Collections.Generic;

namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IMarMarketingModelFactory 
    {    
        #region MarMarketing
    
        MarMarketingSearchModel PrepareMarMarketingSearchModel(MarMarketingSearchModel searchModel);
        
        MarMarketingListModel PrepareMarMarketingListModel(MarMarketingSearchModel searchModel);
        
        MarMarketingModel PrepareMarMarketingModel(MarMarketingModel model, MarMarketing item);
        
        void PrepareMarMarketing(MarMarketingModel model, MarMarketing item);

        IList<SelectListItem> PrepareSelectListMarketing(int valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn Marketing --", string valueFirstRow = "");
        #endregion
    }
}
