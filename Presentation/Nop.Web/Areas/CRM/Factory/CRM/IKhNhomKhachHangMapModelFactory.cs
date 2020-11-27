//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IKhNhomKhachHangMapModelFactory 
    {    
        #region KhNhomKhachHangMap
    
        KhNhomKhachHangMapSearchModel PrepareKhNhomKhachHangMapSearchModel(KhNhomKhachHangMapSearchModel searchModel);
        
        KhNhomKhachHangMapListModel PrepareKhNhomKhachHangMapListModel(KhNhomKhachHangMapSearchModel searchModel);
        
        KhNhomKhachHangMapModel PrepareKhNhomKhachHangMapModel(KhNhomKhachHangMapModel model, KhNhomKhachHangMap item);
        
        void PrepareKhNhomKhachHangMap(KhNhomKhachHangMapModel model, KhNhomKhachHangMap item);
        
        #endregion        
	}
}
