//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IKhDanhBaDienThoaiModelFactory 
    {    
        #region KhDanhBaDienThoai
    
        KhDanhBaDienThoaiSearchModel PrepareKhDanhBaDienThoaiSearchModel(KhDanhBaDienThoaiSearchModel searchModel);
        
        KhDanhBaDienThoaiListModel PrepareKhDanhBaDienThoaiListModel(KhDanhBaDienThoaiSearchModel searchModel);
        
        KhDanhBaDienThoaiModel PrepareKhDanhBaDienThoaiModel(KhDanhBaDienThoaiModel model, KhDanhBaDienThoai item);
        
        void PrepareKhDanhBaDienThoai(KhDanhBaDienThoaiModel model, KhDanhBaDienThoai item);
        
        #endregion        
	}
}
