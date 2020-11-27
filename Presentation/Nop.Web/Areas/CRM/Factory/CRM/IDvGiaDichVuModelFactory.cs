//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IDvGiaDichVuModelFactory 
    {    
        #region DvGiaDichVu
    
        DvGiaDichVuSearchModel PrepareDvGiaDichVuSearchModel(DvGiaDichVuSearchModel searchModel);
        
        DvGiaDichVuListModel PrepareDvGiaDichVuListModel(DvGiaDichVuSearchModel searchModel);
        
        DvGiaDichVuModel PrepareDvGiaDichVuModel(DvGiaDichVuModel model, DvGiaDichVu item);
        
        void PrepareDvGiaDichVu(DvGiaDichVuModel model, DvGiaDichVu item);
        
        #endregion        
	}
}
