//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IMarEventMarketingModelFactory 
    {    
        #region MarEventMarketing
    
        MarEventMarketingSearchModel PrepareMarEventMarketingSearchModel(MarEventMarketingSearchModel searchModel);
        
        MarEventMarketingListModel PrepareMarEventMarketingListModel(MarEventMarketingSearchModel searchModel);
        
        MarEventMarketingModel PrepareMarEventMarketingModel(MarEventMarketingModel model, MarEventMarketing item);
        
        void PrepareMarEventMarketing(MarEventMarketingModel model, MarEventMarketing item);
        
        #endregion        
	}
}
