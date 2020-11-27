//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 26/11/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface ICdChuyenDiModelFactory 
    {    
        #region CdChuyenDi
    
        CdChuyenDiSearchModel PrepareCdChuyenDiSearchModel(CdChuyenDiSearchModel searchModel);
        
        CdChuyenDiListModel PrepareCdChuyenDiListModel(CdChuyenDiSearchModel searchModel);
        
        CdChuyenDiModel PrepareCdChuyenDiModel(CdChuyenDiModel model, CdChuyenDi item);
        
        void PrepareCdChuyenDi(CdChuyenDiModel model, CdChuyenDi item);
        
        #endregion        
	}
}
