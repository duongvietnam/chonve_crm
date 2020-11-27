//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 4/11/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IChGiaTriThongTinMoRongModelFactory 
    {    
        #region ChGiaTriThongTinMoRong
    
        ChGiaTriThongTinMoRongSearchModel PrepareChGiaTriThongTinMoRongSearchModel(ChGiaTriThongTinMoRongSearchModel searchModel);
        
        ChGiaTriThongTinMoRongListModel PrepareChGiaTriThongTinMoRongListModel(ChGiaTriThongTinMoRongSearchModel searchModel);
        
        ChGiaTriThongTinMoRongModel PrepareChGiaTriThongTinMoRongModel(ChGiaTriThongTinMoRongModel model, ChGiaTriThongTinMoRong item);
        
        void PrepareChGiaTriThongTinMoRong(ChGiaTriThongTinMoRongModel model, ChGiaTriThongTinMoRong item);
        
        #endregion        
	}
}
