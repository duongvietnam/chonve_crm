//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IMarMarketingDieuKienModelFactory 
    {    
        #region MarMarketingDieuKien
    
        MarMarketingDieuKienSearchModel PrepareMarMarketingDieuKienSearchModel(MarMarketingDieuKienSearchModel searchModel);
        
        MarMarketingDieuKienListModel PrepareMarMarketingDieuKienListModel(MarMarketingDieuKienSearchModel searchModel);
        
        MarMarketingDieuKienModel PrepareMarMarketingDieuKienModel(MarMarketingDieuKienModel model, MarMarketingDieuKien item);
        
        void PrepareMarMarketingDieuKien(MarMarketingDieuKienModel model, MarMarketingDieuKien item);
        
        #endregion        
	}
}
