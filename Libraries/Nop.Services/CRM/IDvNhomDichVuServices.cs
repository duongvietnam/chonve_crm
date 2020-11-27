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
    public partial interface IDvNhomDichVuService 
    {    
    #region DvNhomDichVu
        IList<DvNhomDichVu> GetAllDvNhomDichVus(int StoreId=0);
        IPagedList <DvNhomDichVu> SearchDvNhomDichVus(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        DvNhomDichVu GetDvNhomDichVuById(int Id);
        IList<DvNhomDichVu> GetDvNhomDichVuByIds(int[] Ids);
        int GetCountNhomDichVu(string ten);
        void InsertDvNhomDichVu(DvNhomDichVu entity);
        void UpdateDvNhomDichVu(DvNhomDichVu entity);
        void DeleteDvNhomDichVu(DvNhomDichVu entity);    
     #endregion
	}
}
