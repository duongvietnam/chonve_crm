//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 19/10/2020
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
    public partial class MarMarketingHeThongService : IMarMarketingHeThongService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<MarMarketingHeThong> _itemRepository;
        #endregion

        #region Ctor

        public MarMarketingHeThongService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<MarMarketingHeThong> itemRepository
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion

        #region Methods
        public virtual IList<MarMarketingHeThong> GetAllMarMarketingHeThongs(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<MarMarketingHeThong> SearchMarMarketingHeThongs(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table;
            return new PagedList<MarMarketingHeThong>(query, pageIndex, pageSize);
        }

        public virtual MarMarketingHeThong GetMarMarketingHeThongById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<MarMarketingHeThong> GetMarMarketingHeThongByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual MarMarketingHeThong GetMarMarketingHeThongByMa(string ma)
        {
            return _itemRepository.Table.FirstOrDefault(c => c.MA == ma);
        }

        public virtual void InsertMarMarketingHeThong(MarMarketingHeThong entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateMarMarketingHeThong(MarMarketingHeThong entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteMarMarketingHeThong(MarMarketingHeThong entity)
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

