//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IDvNhatKyGiaModelFactory 
    {    
        #region DvNhatKyGia
    
        DvNhatKyGiaSearchModel PrepareDvNhatKyGiaSearchModel(DvNhatKyGiaSearchModel searchModel);
        
        DvNhatKyGiaListModel PrepareDvNhatKyGiaListModel(DvNhatKyGiaSearchModel searchModel);
        
        DvNhatKyGiaModel PrepareDvNhatKyGiaModel(DvNhatKyGiaModel model, DvNhatKyGia item);
        
        void PrepareDvNhatKyGia(DvNhatKyGiaModel model, DvNhatKyGia item);
        
        #endregion        
	}
}
