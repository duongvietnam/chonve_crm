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
    public partial interface IKhNhomKhachHangModelFactory 
    {    
        #region KhNhomKhachHang
    
        KhNhomKhachHangSearchModel PrepareKhNhomKhachHangSearchModel(KhNhomKhachHangSearchModel searchModel);
        
        KhNhomKhachHangListModel PrepareKhNhomKhachHangListModel(KhNhomKhachHangSearchModel searchModel);
        
        KhNhomKhachHangModel PrepareKhNhomKhachHangModel(KhNhomKhachHangModel model, KhNhomKhachHang item);
        
        void PrepareKhNhomKhachHang(KhNhomKhachHangModel model, KhNhomKhachHang item);

        bool CheckTenExist(string ten);

        IList<SelectListItem> PrepareSelectListNhomKhachHang(int valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn nhóm khách hàng --", string valueFirstRow = "", int storeId = 0);

        IList<SelectListItem> PrepareMultiSelectListNhomKhachHang(IList<int> valSelected, int storeId = 0);

        #endregion
    }
}
