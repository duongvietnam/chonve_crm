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
    public partial interface IDvNhomDichVuModelFactory 
    {    
        #region DvNhomDichVu
    
        DvNhomDichVuSearchModel PrepareDvNhomDichVuSearchModel(DvNhomDichVuSearchModel searchModel);
        
        DvNhomDichVuListModel PrepareDvNhomDichVuListModel(DvNhomDichVuSearchModel searchModel);
        
        DvNhomDichVuModel PrepareDvNhomDichVuModel(DvNhomDichVuModel model, DvNhomDichVu item);
        
        void PrepareDvNhomDichVu(DvNhomDichVuModel model, DvNhomDichVu item);

        IList<SelectListItem> PrepareSelectListNhomDichVu(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn nhóm dịch vụ --", string valueFirstRow = "");

        bool CheckTenExist(string ten);
        #endregion
    }
}
