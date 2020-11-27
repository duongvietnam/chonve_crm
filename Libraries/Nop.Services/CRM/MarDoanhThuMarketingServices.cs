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
    public partial class MarDoanhThuMarketingService : IMarDoanhThuMarketingService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<MarDoanhThuMarketing> _itemRepository;
        #endregion

        #region Ctor

        public MarDoanhThuMarketingService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<MarDoanhThuMarketing> itemRepository
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion

        #region Methods
        public virtual IList<MarDoanhThuMarketing> GetAllMarDoanhThuMarketings(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<MarDoanhThuMarketing> SearchMarDoanhThuMarketings(int MarEventId, int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table.Where(c => c.EVENT_ID == MarEventId && c.DOANH_NGHIEP_ID == StoreId);
            return new PagedList<MarDoanhThuMarketing>(query.OrderBy(c => c.NGAY_EVENT), pageIndex, pageSize);
        }

        public virtual MarDoanhThuMarketing GetMarDoanhThuMarketingById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<MarDoanhThuMarketing> GetMarDoanhThuMarketingByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual IList<MarDoanhThuMarketing> GetMarDoanhThuMarketings(int EventMarId = 0)
        {
            var query = _itemRepository.Table;
            if (EventMarId > 0)
                query = query.Where(c => c.EVENT_ID == EventMarId);
            return query.ToList();
        }

        public virtual void InsertMarDoanhThuMarketing(MarDoanhThuMarketing entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateMarDoanhThuMarketing(MarDoanhThuMarketing entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteMarDoanhThuMarketing(MarDoanhThuMarketing entity)
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

