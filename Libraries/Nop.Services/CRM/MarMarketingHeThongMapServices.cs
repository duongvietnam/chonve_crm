//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Data;
using Nop.Services.Events;
using Nop.Services.Caching;
using Nop.Services.Caching.Extensions;
using Nop.Core.Domain.CRM;

namespace Nop.Services.CRM
{
    public partial class MarMarketingHeThongMapService : IMarMarketingHeThongMapService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<MarMarketingHeThongMap> _itemRepository;
        #endregion

        #region Ctor

        public MarMarketingHeThongMapService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<MarMarketingHeThongMap> itemRepository
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion

        #region Methods
        public virtual IList<MarMarketingHeThongMap> GetAllMarMarketingHeThongMaps(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<MarMarketingHeThongMap> SearchMarMarketingHeThongMaps(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table;
            return new PagedList<MarMarketingHeThongMap>(query, pageIndex, pageSize); ;
        }

        public virtual MarMarketingHeThongMap GetMarMarketingHeThongMapById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<MarMarketingHeThongMap> GetMarMarketingHeThongMapByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual IList<MarMarketingHeThongMap> GetMarketingHeThongMaps(int MarId = 0)
        {
            var query = _itemRepository.Table;
            if (MarId > 0)
            {
                query = query.Where(c => c.MARKETING_ID == MarId);
            }
            return query.ToList();
        }

        public virtual void InsertMarMarketingHeThongMap(MarMarketingHeThongMap entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateMarMarketingHeThongMap(MarMarketingHeThongMap entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteMarMarketingHeThongMap(MarMarketingHeThongMap entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            _eventPublisher.EntityDeleted(entity);
        }
        public virtual void DeleteMarMarketingHeThongMapByMarId(int MarId)
        {
            if (MarId > 0)
                _itemRepository.Delete(_itemRepository.Table.Where(c => c.MARKETING_ID == MarId));
        }

        #endregion
    }
}

