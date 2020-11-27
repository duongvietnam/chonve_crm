//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 4/11/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IChThongTinMoRongModelFactory 
    {    
        #region ChThongTinMoRong
    
        ChThongTinMoRongSearchModel PrepareChThongTinMoRongSearchModel(ChThongTinMoRongSearchModel searchModel);
        
        ChThongTinMoRongListModel PrepareChThongTinMoRongListModel(ChThongTinMoRongSearchModel searchModel);
        
        ChThongTinMoRongModel PrepareChThongTinMoRongModel(ChThongTinMoRongModel model, ChThongTinMoRong item);
        
        void PrepareChThongTinMoRong(ChThongTinMoRongModel model, ChThongTinMoRong item);
        
        #endregion        
	}
}
