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
    public partial interface IDvNhatKyGiaService 
    {    
    #region DvNhatKyGia
        IList<DvNhatKyGia> GetAllDvNhatKyGias(int StoreId=0);
        IPagedList <DvNhatKyGia> SearchDvNhatKyGias(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        DvNhatKyGia GetDvNhatKyGiaById(int Id);
        IList<DvNhatKyGia> GetDvNhatKyGiaByIds(int[] Ids);
        void InsertDvNhatKyGia(DvNhatKyGia entity);
        void UpdateDvNhatKyGia(DvNhatKyGia entity);
        void DeleteDvNhatKyGia(DvNhatKyGia entity);    
     #endregion
	}
}
