//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IGdGiaoDichDiemModelFactory 
    {    
        #region GdGiaoDichDiem
    
        GdGiaoDichDiemSearchModel PrepareGdGiaoDichDiemSearchModel(GdGiaoDichDiemSearchModel searchModel);
        
        GdGiaoDichDiemListModel PrepareGdGiaoDichDiemListModel(GdGiaoDichDiemSearchModel searchModel);
        
        GdGiaoDichDiemModel PrepareGdGiaoDichDiemModel(GdGiaoDichDiemModel model, GdGiaoDichDiem item);
        
        void PrepareGdGiaoDichDiem(GdGiaoDichDiemModel model, GdGiaoDichDiem item);
        
        #endregion        
	}
}
