//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IMarMaGiamGiaModelFactory 
    {    
        #region MarMaGiamGia
    
        MarMaGiamGiaSearchModel PrepareMarMaGiamGiaSearchModel(MarMaGiamGiaSearchModel searchModel);
        
        MarMaGiamGiaListModel PrepareMarMaGiamGiaListModel(MarMaGiamGiaSearchModel searchModel);
        
        MarMaGiamGiaModel PrepareMarMaGiamGiaModel(MarMaGiamGiaModel model, MarMaGiamGia item);
        
        void PrepareMarMaGiamGia(MarMaGiamGiaModel model, MarMaGiamGia item);

        MarMaGiamGiaModel PrepareMaGiamGiaFromMarketing(MarMarketing item);

        string PrepareMaGiamGia();

        #endregion
    }
}
