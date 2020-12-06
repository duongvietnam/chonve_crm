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
    public partial interface IMarMarketingService 
    {    
    #region MarMarketing
        IList<MarMarketing> GetAllMarMarketings(int StoreId=0);
        IPagedList <MarMarketing> SearchMarMarketings(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        MarMarketing GetMarMarketingById(int Id);
        IList<MarMarketing> GetMarMarketingByIds(int[] Ids);
        LocDieuKienMarketing LocDieuKienMarketing(int HangKhachHang, Decimal DonGia, DateTime NgayGiaoDich, int DichVuId);
        LocDieuKienMaGiamGia CheckMaGiamGia(string ma, int doanhNgiepId, decimal donGia, DateTime ngayGiaoDich);
        string ApDungMaGiamGia(IList<string> listMa, int doanhNgiepId, int khachHangId, int giaoDichId, DateTime ngayGiaoDich);
        void InsertMarMarketing(MarMarketing entity);
        void UpdateMarMarketing(MarMarketing entity);
        void DeleteMarMarketing(MarMarketing entity);    
     #endregion
	}
}
