//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 10/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
using System.Collections.Generic;

namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IKhNhomHeThongModelFactory 
    {    
        #region KhNhomHeThong
    
        KhNhomHeThongSearchModel PrepareKhNhomHeThongSearchModel(KhNhomHeThongSearchModel searchModel);
        
        KhNhomHeThongListModel PrepareKhNhomHeThongListModel(KhNhomHeThongSearchModel searchModel);
        
        KhNhomHeThongModel PrepareKhNhomHeThongModel(KhNhomHeThongModel model, KhNhomHeThong item);
        
        void PrepareKhNhomHeThong(KhNhomHeThongModel model, KhNhomHeThong item);

        IList<SelectListItem> PrepareSelectListNhomHeThong(IList<int> valSelected = null, bool isAddFirst = false, string strFirstRow = "-- Chọn phân nhóm --", string valueFirstRow = "");
        #endregion
    }
}
