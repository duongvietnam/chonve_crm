//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IDvDichVuComboModelFactory 
    {    
        #region DvDichVuCombo
    
        DvDichVuComboSearchModel PrepareDvDichVuComboSearchModel(DvDichVuComboSearchModel searchModel);
        
        DvDichVuComboListModel PrepareDvDichVuComboListModel(DvDichVuComboSearchModel searchModel);
        
        DvDichVuComboModel PrepareDvDichVuComboModel(DvDichVuComboModel model, DvDichVuCombo item);
        
        void PrepareDvDichVuCombo(DvDichVuComboModel model, DvDichVuCombo item);
        
        #endregion        
	}
}
