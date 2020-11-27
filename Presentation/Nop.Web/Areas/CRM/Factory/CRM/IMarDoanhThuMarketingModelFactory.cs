//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IMarDoanhThuMarketingModelFactory 
    {    
        #region MarDoanhThuMarketing
    
        MarDoanhThuMarketingSearchModel PrepareMarDoanhThuMarketingSearchModel(MarDoanhThuMarketingSearchModel searchModel);
        
        MarDoanhThuMarketingListModel PrepareMarDoanhThuMarketingListModel(MarDoanhThuMarketingSearchModel searchModel);
        
        MarDoanhThuMarketingModel PrepareMarDoanhThuMarketingModel(MarDoanhThuMarketingModel model, MarDoanhThuMarketing item);
        
        void PrepareMarDoanhThuMarketing(MarDoanhThuMarketingModel model, MarDoanhThuMarketing item);
        
        #endregion        
	}
}
