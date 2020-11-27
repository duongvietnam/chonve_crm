//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 21/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.CRM;


namespace Nop.Services.CRM
{
    public partial interface IDvDonViTinhService 
    {    
    #region DvDonViTinh
        IList<DvDonViTinh> GetAllDvDonViTinhs(int StoreId=0);
        IPagedList <DvDonViTinh> SearchDvDonViTinhs(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        DvDonViTinh GetDvDonViTinhById(int Id);
        IList<DvDonViTinh> GetDvDonViTinhByIds(int[] Ids);
        DvDonViTinh GetByMa(string ma);
        void InsertDvDonViTinh(DvDonViTinh entity);
        void UpdateDvDonViTinh(DvDonViTinh entity);
        void DeleteDvDonViTinh(DvDonViTinh entity);    
     #endregion
	}
}
