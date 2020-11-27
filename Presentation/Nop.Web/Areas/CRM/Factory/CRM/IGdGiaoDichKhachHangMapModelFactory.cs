//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IGdGiaoDichKhachHangMapModelFactory 
    {    
        #region GdGiaoDichKhachHangMap
    
        GdGiaoDichKhachHangMapSearchModel PrepareGdGiaoDichKhachHangMapSearchModel(GdGiaoDichKhachHangMapSearchModel searchModel);
        
        GdGiaoDichKhachHangMapListModel PrepareGdGiaoDichKhachHangMapListModel(GdGiaoDichKhachHangMapSearchModel searchModel);
        
        GdGiaoDichKhachHangMapModel PrepareGdGiaoDichKhachHangMapModel(GdGiaoDichKhachHangMapModel model, GdGiaoDichKhachHangMap item);
        
        void PrepareGdGiaoDichKhachHangMap(GdGiaoDichKhachHangMapModel model, GdGiaoDichKhachHangMap item);
        
        #endregion        
	}
}
