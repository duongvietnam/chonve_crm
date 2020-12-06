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
    public partial interface IKhKhachHangModelFactory 
    {    
        #region KhKhachHang
    
        KhKhachHangSearchModel PrepareKhKhachHangSearchModel(KhKhachHangSearchModel searchModel);
        
        KhKhachHangListModel PrepareKhKhachHangListModel(KhKhachHangSearchModel searchModel);
        
        KhKhachHangModel PrepareKhKhachHangModel(KhKhachHangModel model, KhKhachHang item);
        
        void PrepareKhKhachHang(KhKhachHangModel model, KhKhachHang item);

        KhKhachHangListModel PreparePhanHangKhachHangListModel(KhPhanTichKhachHangSearchModel searchModel);

        KhKhachHangListModel PrepareNhomKhachHangListModel(KhPhanTichKhachHangSearchModel searchModel);

        KhKhachHangListModel PrepareThoiQuenKhachHangListModel(KhPhanTichKhachHangSearchModel searchModel);

        KhKhachHangModel PrepareKhKhachHangDetailModel(KhKhachHangModel model, KhKhachHang item);

        KhKhachHangModel PrepareKhKhachHangTradeDetailModel(KhKhachHangModel model, KhKhachHang item);

        IList<SelectListItem> PrepareDDLHangKhachHang();

        IList<SelectListItem> PrepareMultiSelectHangKhachHang();

        IList<SelectListItem> PrepareMultiSelectKhachHang(IList<int> valSelected, string g);
        #endregion
    }
}
