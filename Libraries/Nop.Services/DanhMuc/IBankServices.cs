//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 31/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.DanhMuc;


namespace Nop.Services.DanhMuc
{
    public partial interface IBankService 
    {    
    #region Bank
        IList<Bank> GetAllBanks();
        IPagedList <Bank> SearchBanks(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        Bank GetBankById(int Id);
        IList<Bank> GetBankByIds(int[] newsIds);
        void InsertBank(Bank entity);
        void UpdateBank(Bank entity);
        void DeleteBank(Bank entity);    
     #endregion
	}
}
