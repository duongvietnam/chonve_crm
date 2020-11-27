//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IKhCauHinhMoRongModelFactory 
    {    
        #region KhCauHinhMoRong
    
        KhCauHinhMoRongSearchModel PrepareKhCauHinhMoRongSearchModel(KhCauHinhMoRongSearchModel searchModel);
        
        KhCauHinhMoRongListModel PrepareKhCauHinhMoRongListModel(KhCauHinhMoRongSearchModel searchModel);
        
        KhCauHinhMoRongModel PrepareKhCauHinhMoRongModel(KhCauHinhMoRongModel model, KhCauHinhMoRong item);
        
        void PrepareKhCauHinhMoRong(KhCauHinhMoRongModel model, KhCauHinhMoRong item);

        IList<SelectListItem> PrepareSelectListCauHinhMoRong(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn cấu hình --", string valueFirstRow = "");

        bool CheckTenExist(string ten);

        #endregion
    }
}
