//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.CRM;
using Nop.Data;
using Nop.Services.Caching;
using Nop.Services.Caching.Extensions;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Services.CRM
{
    public partial class MarEventMarketingService : IMarEventMarketingService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<MarEventMarketing> _itemRepository;
        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public MarEventMarketingService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<MarEventMarketing> itemRepository,
            IStoreContext storeContext,
            IWorkContext workContext
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._storeContext = storeContext;
            this._workContext = workContext;
        }

        #endregion

        #region Methods
        public virtual IList<MarEventMarketing> GetAllMarEventMarketings(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<MarEventMarketing> SearchMarEventMarketings(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table;
            return new PagedList<MarEventMarketing>(query, pageIndex, pageSize);
        }

        public virtual MarEventMarketing GetMarEventMarketingById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual MarEventMarketing GetEventMarketing(int marId)
        {
            var query = _itemRepository.Table;
            if (marId > 0)
                query = query.Where(c => c.MARKETING_ID == marId);
            return query.FirstOrDefault();
        }

        public virtual IList<MarEventMarketing> GetMarEventMarketingByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual void InsertMarEventMarketing(MarEventMarketing entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.DOANH_NGHIEP_ID = _storeContext.CurrentStore.Id;
            entity.NGAY_TAO = DateTime.Now;
            entity.NGUOI_TAO = _workContext.CurrentCustomer.Id;
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateMarEventMarketing(MarEventMarketing entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteMarEventMarketing(MarEventMarketing entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            _eventPublisher.EntityDeleted(entity);
        }
        public virtual void DeleteMarEventMarketingByMarId(int marId)
        {
            var item = _itemRepository.Table.Where(c => c.MARKETING_ID == marId);
            _itemRepository.Delete(item);
        }

        #endregion
    }
}

