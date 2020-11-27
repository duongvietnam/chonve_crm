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
    public partial interface IDvGiaDichVuService 
    {    
    #region DvGiaDichVu
        IList<DvGiaDichVu> GetAllDvGiaDichVus(int StoreId=0);
        IPagedList <DvGiaDichVu> SearchDvGiaDichVus(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        DvGiaDichVu GetDvGiaDichVuById(int Id);
        IList<DvGiaDichVu> GetDvGiaDichVuByIds(int[] Ids);
        DvGiaDichVu GetDvGiaDichVu(int dichVuId);
        void InsertDvGiaDichVu(DvGiaDichVu entity);
        void UpdateDvGiaDichVu(DvGiaDichVu entity);
        void DeleteDvGiaDichVu(DvGiaDichVu entity);    
     #endregion
	}
}
