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
    public partial interface IDvLoaiDichVuModelFactory 
    {    
        #region DvLoaiDichVu
    
        DvLoaiDichVuSearchModel PrepareDvLoaiDichVuSearchModel(DvLoaiDichVuSearchModel searchModel);
        
        DvLoaiDichVuListModel PrepareDvLoaiDichVuListModel(DvLoaiDichVuSearchModel searchModel);
        
        DvLoaiDichVuModel PrepareDvLoaiDichVuModel(DvLoaiDichVuModel model, DvLoaiDichVu item);
        
        void PrepareDvLoaiDichVu(DvLoaiDichVuModel model, DvLoaiDichVu item);

        IList<SelectListItem> PrepareSelectListLoaiDichVu(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn loại dịch vụ --", string valueFirstRow = "");

        bool CheckTenExist(string ten);

        #endregion
    }
}
