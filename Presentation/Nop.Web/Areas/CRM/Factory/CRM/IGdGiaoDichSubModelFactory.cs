//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IGdGiaoDichSubModelFactory 
    {    
        #region GdGiaoDichSub
    
        GdGiaoDichSubSearchModel PrepareGdGiaoDichSubSearchModel(GdGiaoDichSubSearchModel searchModel);
        
        GdGiaoDichSubListModel PrepareGdGiaoDichSubListModel(GdGiaoDichSubSearchModel searchModel);
        
        GdGiaoDichSubModel PrepareGdGiaoDichSubModel(GdGiaoDichSubModel model, GdGiaoDichSub item);
        
        void PrepareGdGiaoDichSub(GdGiaoDichSubModel model, GdGiaoDichSub item);
        
        #endregion        
	}
}
