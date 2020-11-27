//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IGdGiaoDichModelFactory 
    {    
        #region GdGiaoDich
    
        GdGiaoDichSearchModel PrepareGdGiaoDichSearchModel(GdGiaoDichSearchModel searchModel);
        
        GdGiaoDichListModel PrepareGdGiaoDichListModel(GdGiaoDichSearchModel searchModel);
        
        GdGiaoDichModel PrepareGdGiaoDichModel(GdGiaoDichModel model, GdGiaoDich item);
        
        void PrepareGdGiaoDich(GdGiaoDichModel model, GdGiaoDich item);
        
        #endregion        
	}
}
