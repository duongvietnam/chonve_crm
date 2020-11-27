//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IGdGiaoDichDanhGiaModelFactory 
    {    
        #region GdGiaoDichDanhGia
    
        GdGiaoDichDanhGiaSearchModel PrepareGdGiaoDichDanhGiaSearchModel(GdGiaoDichDanhGiaSearchModel searchModel);
        
        GdGiaoDichDanhGiaListModel PrepareGdGiaoDichDanhGiaListModel(GdGiaoDichDanhGiaSearchModel searchModel);
        
        GdGiaoDichDanhGiaModel PrepareGdGiaoDichDanhGiaModel(GdGiaoDichDanhGiaModel model, GdGiaoDichDanhGia item);
        
        void PrepareGdGiaoDichDanhGia(GdGiaoDichDanhGiaModel model, GdGiaoDichDanhGia item);
        
        #endregion        
	}
}
