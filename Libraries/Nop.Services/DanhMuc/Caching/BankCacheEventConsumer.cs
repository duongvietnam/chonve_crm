using Nop.Core.Domain.DanhMuc;
using Nop.Services.Caching;
using Nop.Services.Directory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Services.DanhMuc.Caching
{
    public partial class  BankCacheEventConsumer : CacheEventConsumer<Bank>
    {
        /// <summary>
        /// Clear cache data
        /// </summary>
        /// <param name="entity">Entity</param>
        protected override void ClearCache(Bank entity)
        {
            RemoveByPrefix(NopDanhMucDefaults.BankPrefixCacheKey);
        }
    }
}
