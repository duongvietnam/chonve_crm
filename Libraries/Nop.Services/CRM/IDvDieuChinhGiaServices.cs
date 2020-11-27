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
    public partial interface IDvDieuChinhGiaService 
    {    
    #region DvDieuChinhGia
        IList<DvDieuChinhGia> GetAllDvDieuChinhGias(int StoreId=0);
        IPagedList <DvDieuChinhGia> SearchDvDieuChinhGias(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        DvDieuChinhGia GetDvDieuChinhGiaById(int Id);
        IList<DvDieuChinhGia> GetDvDieuChinhGiaByIds(int[] Ids);
        void InsertDvDieuChinhGia(DvDieuChinhGia entity);
        void UpdateDvDieuChinhGia(DvDieuChinhGia entity);
        void DeleteDvDieuChinhGia(DvDieuChinhGia entity);    
     #endregion
	}
}
