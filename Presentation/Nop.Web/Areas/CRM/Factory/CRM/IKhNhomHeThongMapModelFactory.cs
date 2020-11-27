//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IKhNhomHeThongMapModelFactory 
    {    
        #region KhNhomHeThongMap
    
        KhNhomHeThongMapSearchModel PrepareKhNhomHeThongMapSearchModel(KhNhomHeThongMapSearchModel searchModel);
        
        KhNhomHeThongMapListModel PrepareKhNhomHeThongMapListModel(KhNhomHeThongMapSearchModel searchModel);
        
        KhNhomHeThongMapModel PrepareKhNhomHeThongMapModel(KhNhomHeThongMapModel model, KhNhomHeThongMap item);
        
        void PrepareKhNhomHeThongMap(KhNhomHeThongMapModel model, KhNhomHeThongMap item);
        
        #endregion        
	}
}
