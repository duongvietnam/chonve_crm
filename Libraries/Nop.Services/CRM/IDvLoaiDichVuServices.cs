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
    public partial interface IDvLoaiDichVuService 
    {    
    #region DvLoaiDichVu
        IList<DvLoaiDichVu> GetAllDvLoaiDichVus(int StoreId=0);
        IPagedList <DvLoaiDichVu> SearchDvLoaiDichVus(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        DvLoaiDichVu GetDvLoaiDichVuById(int Id);
        IList<DvLoaiDichVu> GetDvLoaiDichVuByIds(int[] Ids);
        int CountLoaiDichVu(string tenDichVu);
        void InsertDvLoaiDichVu(DvLoaiDichVu entity);
        void UpdateDvLoaiDichVu(DvLoaiDichVu entity);
        void DeleteDvLoaiDichVu(DvLoaiDichVu entity);    
     #endregion
	}
}
