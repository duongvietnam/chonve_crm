//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 2/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IChPhanHangKhachHangModelFactory 
    {    
        #region ChPhanHangKhachHang
    
        ChPhanHangKhachHangSearchModel PrepareChPhanHangKhachHangSearchModel(ChPhanHangKhachHangSearchModel searchModel);
        
        ChPhanHangKhachHangListModel PrepareChPhanHangKhachHangListModel(ChPhanHangKhachHangSearchModel searchModel);
        
        ChPhanHangKhachHangModel PrepareChPhanHangKhachHangModel(ChPhanHangKhachHangModel model, ChPhanHangKhachHang item);
        
        void PrepareChPhanHangKhachHang(ChPhanHangKhachHangModel model, ChPhanHangKhachHang item);

        IList<SelectListItem> PrepareDDLPhanHangKhachHang(int valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn hạnh khách hàng --", string valueFirstRow = "");

        IList<SelectListItem> PrepareMultiSelectPhanHangKhachHang(IList<int> valSelected);

        #endregion
    }
}
