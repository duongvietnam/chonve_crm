//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 21/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
using System.Collections.Generic;

namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IDvDonViTinhModelFactory 
    {    
        #region DvDonViTinh
    
        DvDonViTinhSearchModel PrepareDvDonViTinhSearchModel(DvDonViTinhSearchModel searchModel);
        
        DvDonViTinhListModel PrepareDvDonViTinhListModel(DvDonViTinhSearchModel searchModel);
        
        DvDonViTinhModel PrepareDvDonViTinhModel(DvDonViTinhModel model, DvDonViTinh item);
        
        void PrepareDvDonViTinh(DvDonViTinhModel model, DvDonViTinh item);

        IList<SelectListItem> PrepareSelectListDonViTinh(int valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn đơn vị tính --", string valueFirstRow = "");

        #endregion
    }
}
