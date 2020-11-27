//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.CRM;


namespace Nop.Services.CRM
{
    public partial interface IKhThongTinMoRongService
    {
        #region KhThongTinMoRong
        IList<KhThongTinMoRong> GetAllKhThongTinMoRongs(int StoreId = 0);
        IPagedList<KhThongTinMoRong> SearchKhThongTinMoRongs(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue);
        KhThongTinMoRong GetKhThongTinMoRongById(int Id);
        IList<KhThongTinMoRong> GetKhThongTinMoRongByIds(int[] Ids);
        KhThongTinMoRong GetKhThongTinMoRong(int khachHangId, int cauHinhId);
        void InsertKhThongTinMoRong(KhThongTinMoRong entity);
        void UpdateKhThongTinMoRong(KhThongTinMoRong entity);
        void DeleteKhThongTinMoRong(KhThongTinMoRong entity);
        #endregion
    }
}
