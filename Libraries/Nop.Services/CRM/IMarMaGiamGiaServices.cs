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
    public partial interface IMarMaGiamGiaService 
    {    
    #region MarMaGiamGia
        IList<MarMaGiamGia> GetAllMarMaGiamGias(int StoreId=0);
        IPagedList <MarMaGiamGia> SearchMarMaGiamGias(int marId, int StoreId =0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        MarMaGiamGia GetMarMaGiamGiaById(int Id);
        IList<MarMaGiamGia> GetMarMaGiamGiaByIds(int[] Ids);
        int GetSoMaGiamGiaByMarId(int marId);
        int GetCountByMa(string ma, int doanhNghiepId);
        int GetSoMaGiamGiaDaGuiByMarId(int marId, bool chuaGuiKhachHang = false);
        int GetSoMaGiamGiaDaSuDungByMarId(int marId);
        IList<MarMaGiamGia> GetMarMaGiamGias(int marId, bool chuaGuiKhachHang);
        IList<int> GetListKhachHang(int marId);
        void InsertMarMaGiamGia(MarMaGiamGia entity);
        void UpdateMarMaGiamGia(MarMaGiamGia entity);
        void DeleteMarMaGiamGia(MarMaGiamGia entity);
        void DeleteMarMaGiamGiaByMarId(int marId);
     #endregion
    }
}
