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
    public partial interface IDvDichVuModelFactory 
    {    
        #region DvDichVu
    
        DvDichVuSearchModel PrepareDvDichVuSearchModel(DvDichVuSearchModel searchModel);
        
        DvDichVuListModel PrepareDvDichVuListModel(DvDichVuSearchModel searchModel);
        
        DvDichVuModel PrepareDvDichVuModel(DvDichVuModel model, DvDichVu item, bool isComBo = false);
        
        void PrepareDvDichVu(DvDichVuModel model, DvDichVu item);

        bool CheckTenExist(string tenDichVu, int isCombo);

        IList<SelectListItem> PrepareDDLDichVuCombo(int valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn dịch vụ --", string valueFirstRow = "", bool isCombo = false);

        IList<SelectListItem> PrepareMultiSelectDichVuCombo(IList<int> valSelected = null, bool isAddFirst = false, string strFirstRow = "-- Chọn loại dịch vụ --", string valueFirstRow = "", bool isCombo = false);
        #endregion
    }
}
