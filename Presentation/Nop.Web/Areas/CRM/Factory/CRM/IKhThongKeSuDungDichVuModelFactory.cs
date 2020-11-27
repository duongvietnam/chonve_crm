//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 2/10/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public partial interface IKhThongKeSuDungDichVuModelFactory 
    {    
        #region KhThongKeSuDungDichVu
    
        KhThongKeSuDungDichVuSearchModel PrepareKhThongKeSuDungDichVuSearchModel(KhThongKeSuDungDichVuSearchModel searchModel);
        
        KhThongKeSuDungDichVuListModel PrepareKhThongKeSuDungDichVuListModel(KhThongKeSuDungDichVuSearchModel searchModel);
        
        KhThongKeSuDungDichVuModel PrepareKhThongKeSuDungDichVuModel(KhThongKeSuDungDichVuModel model, KhThongKeSuDungDichVu item);
        
        void PrepareKhThongKeSuDungDichVu(KhThongKeSuDungDichVuModel model, KhThongKeSuDungDichVu item);
        
        #endregion        
	}
}
