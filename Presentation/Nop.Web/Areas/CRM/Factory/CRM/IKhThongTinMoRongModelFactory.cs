//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IKhThongTinMoRongModelFactory 
    {    
        #region KhThongTinMoRong
    
        KhThongTinMoRongSearchModel PrepareKhThongTinMoRongSearchModel(KhThongTinMoRongSearchModel searchModel);
        
        KhThongTinMoRongListModel PrepareKhThongTinMoRongListModel(KhThongTinMoRongSearchModel searchModel);
        
        KhThongTinMoRongModel PrepareKhThongTinMoRongModel(KhThongTinMoRongModel model, KhThongTinMoRong item);
        
        void PrepareKhThongTinMoRong(KhThongTinMoRongModel model, KhThongTinMoRong item);
        
        #endregion        
	}
}
