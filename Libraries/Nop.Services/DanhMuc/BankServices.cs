//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 31/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Data;
using Nop.Services.Events;
using Nop.Core.Domain.DanhMuc;
using Nop.Services.Caching;
using Nop.Services.Caching.Extensions;

namespace Nop.Services.DanhMuc
{
    public partial class BankService : IBankService
    {

        #region Fieldsg
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<Bank> _itemRepository;
        #endregion

        #region Ctor

        public BankService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<Bank> itemRepository
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }
        #endregion

        #region Methods
        public virtual IList<Bank> GetAllBanks()
        {
            var key = _cacheKeyService.PrepareKeyForDefaultCache(NopDanhMucDefaults.BankAllCacheKey);
            return _staticCacheManager.Get(key, () =>
            {
                var query = _itemRepository.Table.OrderBy(c => c.OrderDisplay);
                return query.ToList();
            });
        }

        public virtual IPagedList<Bank> SearchBanks(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(Keysearch))
            {
                query = query.Where(c => c.Name.Contains(Keysearch) || c.Code.Contains(Keysearch) || c.ShortName.Contains(Keysearch) || c.CodePlus.Contains(Keysearch));
            }
            query = query.OrderBy(c => c.OrderDisplay);
            return new PagedList<Bank>(query, pageIndex, pageSize); ;
        }

        public virtual Bank GetBankById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<Bank> GetBankByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual void InsertBank(Bank entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateBank(Bank entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteBank(Bank entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            _eventPublisher.EntityDeleted(entity);
        }

        #endregion
    }
}		

