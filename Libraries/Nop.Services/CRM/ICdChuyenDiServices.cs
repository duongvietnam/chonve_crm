//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 26/11/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core;
using Nop.Core.Domain.CRM;
using System.Collections.Generic;


namespace Nop.Services.CRM
{
    public partial interface ICdChuyenDiService
    {
        #region CdChuyenDi
        IList<CdChuyenDi> GetAllCdChuyenDis(int StoreId = 0);
        IPagedList<CdChuyenDi> SearchCdChuyenDis(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue);
        CdChuyenDi GetCdChuyenDiById(int Id);
        IList<CdChuyenDi> GetCdChuyenDiByIds(int[] Ids);
        CdChuyenDi GetChuyenDi(string ma, int doanhNghiepId);
        void InsertCdChuyenDi(CdChuyenDi entity);
        void UpdateCdChuyenDi(CdChuyenDi entity);
        void DeleteCdChuyenDi(CdChuyenDi entity);
        #endregion
    }
}
