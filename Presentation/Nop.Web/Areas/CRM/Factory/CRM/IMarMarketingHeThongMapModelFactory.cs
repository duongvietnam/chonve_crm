//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IMarMarketingHeThongMapModelFactory 
    {    
        #region MarMarketingHeThongMap
    
        MarMarketingHeThongMapSearchModel PrepareMarMarketingHeThongMapSearchModel(MarMarketingHeThongMapSearchModel searchModel);
        
        MarMarketingHeThongMapListModel PrepareMarMarketingHeThongMapListModel(MarMarketingHeThongMapSearchModel searchModel);
        
        MarMarketingHeThongMapModel PrepareMarMarketingHeThongMapModel(MarMarketingHeThongMapModel model, MarMarketingHeThongMap item);
        
        void PrepareMarMarketingHeThongMap(MarMarketingHeThongMapModel model, MarMarketingHeThongMap item);
        
        #endregion        
	}
}
