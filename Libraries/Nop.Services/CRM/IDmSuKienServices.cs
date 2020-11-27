//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 2/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.CRM;


namespace Nop.Services.CRM
{
    public partial interface IDmSuKienService 
    {    
    #region DmSuKien
        IList<DmSuKien> GetAllDmSuKiens(int StoreId=0);
        IPagedList <DmSuKien> SearchDmSuKiens(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        DmSuKien GetDmSuKienById(int Id);
        IList<DmSuKien> GetDmSuKienByIds(int[] Ids);
        void InsertDmSuKien(DmSuKien entity);
        void UpdateDmSuKien(DmSuKien entity);
        void DeleteDmSuKien(DmSuKien entity);    
     #endregion
	}
}
