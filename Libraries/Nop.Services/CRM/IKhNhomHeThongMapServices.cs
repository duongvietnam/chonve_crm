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
    public partial interface IKhNhomHeThongMapService
    {
        #region KhNhomHeThongMap
        IList<KhNhomHeThongMap> GetAllKhNhomHeThongMaps(int StoreId = 0);
        IPagedList<KhNhomHeThongMap> SearchKhNhomHeThongMaps(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue);
        KhNhomHeThongMap GetKhNhomHeThongMapById(int Id);
        IList<KhNhomHeThongMap> GetKhNhomHeThongMapByIds(int[] Ids);
        IList<int> GetKhNhomHeThongByNhomId(int nhomId);
        IList<KhNhomHeThongMap> GetKhNhomHeThongMaps(int nhomId = 0);
        void InsertKhNhomHeThongMap(KhNhomHeThongMap entity);
        void UpdateKhNhomHeThongMap(KhNhomHeThongMap entity);
        void DeleteKhNhomHeThongMap(KhNhomHeThongMap entity);
        void DeleteKhNhomHeThongMaps(IList<KhNhomHeThongMap> entities);
        #endregion
    }
}
