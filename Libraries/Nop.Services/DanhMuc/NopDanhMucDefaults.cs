using Nop.Core.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Services.DanhMuc
{
    public static partial class NopDanhMucDefaults
    {
        

        #region Bank
        public static CacheKey BankAllCacheKey => new CacheKey("Nop.danhmuc.bank.all", BankPrefixCacheKey);
        public static string BankPrefixCacheKey => "Nop.danhmuc.bank.";
        #endregion
        
    }
}
