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
    public partial interface IDvDichVuService 
    {    
    #region DvDichVu
        IList<DvDichVu> GetAllDvDichVus(bool isCombo, int StoreId =0);
        IPagedList <DvDichVu> SearchDvDichVus(bool isCombo, int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        DvDichVu GetDvDichVuById(int Id);
        DvDichVu GetDvDichVuByIdNguon(int IdNguon, int DoanhNghiepId);
        IList<DvDichVu> GetDvDichVuByIds(int[] Ids);
        int CountDichVu(string TenDichVu = null, int isCombo = -1);
        void InsertDvDichVu(DvDichVu entity);
        void UpdateDvDichVu(DvDichVu entity);
        void DeleteDvDichVu(DvDichVu entity);    
     #endregion
	}
}
