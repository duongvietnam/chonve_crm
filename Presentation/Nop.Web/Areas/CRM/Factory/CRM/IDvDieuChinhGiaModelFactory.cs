//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IDvDieuChinhGiaModelFactory 
    {    
        #region DvDieuChinhGia
    
        DvDieuChinhGiaSearchModel PrepareDvDieuChinhGiaSearchModel(DvDieuChinhGiaSearchModel searchModel);
        
        DvDieuChinhGiaListModel PrepareDvDieuChinhGiaListModel(DvDieuChinhGiaSearchModel searchModel);
        
        DvDieuChinhGiaModel PrepareDvDieuChinhGiaModel(DvDieuChinhGiaModel model, DvDieuChinhGia item);
        
        void PrepareDvDieuChinhGia(DvDieuChinhGiaModel model, DvDieuChinhGia item);
        
        #endregion        
	}
}
