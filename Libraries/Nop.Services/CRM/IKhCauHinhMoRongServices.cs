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
    public partial interface IKhCauHinhMoRongService
    {
        #region KhCauHinhMoRong
        IList<KhCauHinhMoRong> GetAllKhCauHinhMoRongs(int StoreId = 0);
        IPagedList<KhCauHinhMoRong> SearchKhCauHinhMoRongs(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue);
        KhCauHinhMoRong GetKhCauHinhMoRongById(int Id);
        IList<KhCauHinhMoRong> GetKhCauHinhMoRongByIds(int[] Ids);
        IList<KhCauHinhMoRong> GetCauHinhMoRongs(int DoanhNghiepId);
        int GetCountCauHinhMoRong(string ten = null);
        void InsertKhCauHinhMoRong(KhCauHinhMoRong entity);
        void UpdateKhCauHinhMoRong(KhCauHinhMoRong entity);
        void DeleteKhCauHinhMoRong(KhCauHinhMoRong entity);
        #endregion
    }
}
