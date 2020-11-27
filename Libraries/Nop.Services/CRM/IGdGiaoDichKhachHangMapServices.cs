//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Domain.CRM;


namespace Nop.Services.CRM
{
    public partial interface IGdGiaoDichKhachHangMapService 
    {    
    #region GdGiaoDichKhachHangMap
        IList<GdGiaoDichKhachHangMap> GetAllGdGiaoDichKhachHangMaps(int StoreId=0);
        IPagedList <GdGiaoDichKhachHangMap> SearchGdGiaoDichKhachHangMaps(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        GdGiaoDichKhachHangMap GetGdGiaoDichKhachHangMapById(int Id);
        IList<GdGiaoDichKhachHangMap> GetGdGiaoDichKhachHangMapByIds(int[] Ids);
        IQueryable<GdGiaoDichKhachHangMap> GetGdGiaoDichKhachHangMaps(int khachHangId = 0, DateTime? TuNgay = null, DateTime? DenNgay = null);
        IList<GdGiaoDichKhachHangMap> GetGdGiaoDichKhachHangMapByGiaoDichId(int giaoDichId, int doanhNghiepId);
        string SoLanSuDungDichVuTrongNam(int khachHangId, DateTime TuNgay, DateTime DenNgay);
        IList<int> GetListKhachHangId(int giaoDichId);
        void InsertGdGiaoDichKhachHangMap(GdGiaoDichKhachHangMap entity);
        void UpdateGdGiaoDichKhachHangMap(GdGiaoDichKhachHangMap entity);
        void DeleteGdGiaoDichKhachHangMap(GdGiaoDichKhachHangMap entity);
        IList<GdGiaoDichKhachHangMap> GetKHsByGDIds(int[] GiaodichIds);
     #endregion
    }
}
