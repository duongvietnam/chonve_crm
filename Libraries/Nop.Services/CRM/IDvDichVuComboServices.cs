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
    public partial interface IDvDichVuComboService 
    {    
    #region DvDichVuCombo
        IList<DvDichVuCombo> GetAllDvDichVuCombos(int StoreId=0);
        IPagedList <DvDichVuCombo> SearchDvDichVuCombos(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue);
        DvDichVuCombo GetDvDichVuComboById(int Id);
        IList<DvDichVuCombo> GetDvDichVuComboByIds(int[] Ids);
        IList<int> GetDichVuTrongCombo(int ComboId);
        IList<DvDichVuCombo> GetDichVuCombos(int comBoId = 0);
        void InsertDvDichVuCombo(DvDichVuCombo entity);
        void UpdateDvDichVuCombo(DvDichVuCombo entity);
        void DeleteDvDichVuCombo(DvDichVuCombo entity);
        void DeleteDvDichVuCombos(IList<DvDichVuCombo> entity);
     #endregion
    }
}
