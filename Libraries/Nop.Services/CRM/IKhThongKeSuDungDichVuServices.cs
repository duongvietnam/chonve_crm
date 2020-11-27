//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 2/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.CRM;


namespace Nop.Services.CRM
{
    public partial interface IKhThongKeSuDungDichVuService 
    {    
    #region KhThongKeSuDungDichVu
        IList<KhThongKeSuDungDichVu> GetAllKhThongKeSuDungDichVus(int StoreId=0);
        IPagedList <KhThongKeSuDungDichVu> SearchKhThongKeSuDungDichVus(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        KhThongKeSuDungDichVu GetKhThongKeSuDungDichVuById(int Id);
        IList<KhThongKeSuDungDichVu> GetKhThongKeSuDungDichVuByIds(int[] Ids);
        void InsertKhThongKeSuDungDichVu(KhThongKeSuDungDichVu entity);
        void UpdateKhThongKeSuDungDichVu(KhThongKeSuDungDichVu entity);
        void DeleteKhThongKeSuDungDichVu(KhThongKeSuDungDichVu entity);
        KhThongKeSuDungDichVu GetKhThongKeSuDungDichVuByIdKH(int IdKH);
     #endregion
    }
}
