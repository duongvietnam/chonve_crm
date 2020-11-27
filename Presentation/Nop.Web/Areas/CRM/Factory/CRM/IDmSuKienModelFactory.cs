//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 2/10/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IDmSuKienModelFactory 
    {    
        #region DmSuKien
    
        DmSuKienSearchModel PrepareDmSuKienSearchModel(DmSuKienSearchModel searchModel);
        
        DmSuKienListModel PrepareDmSuKienListModel(DmSuKienSearchModel searchModel);
        
        DmSuKienModel PrepareDmSuKienModel(DmSuKienModel model, DmSuKien item);
        
        void PrepareDmSuKien(DmSuKienModel model, DmSuKien item);
        
        #endregion        
	}
}
