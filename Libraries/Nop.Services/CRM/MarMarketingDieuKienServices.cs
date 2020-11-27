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
    public partial class MarMarketingDieuKienService : IMarMarketingDieuKienService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<MarMarketingDieuKien> _itemRepository;
        #endregion

        #region Ctor

        public MarMarketingDieuKienService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<MarMarketingDieuKien> itemRepository
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion

        #region Methods
        public virtual IList<MarMarketingDieuKien> GetAllMarMarketingDieuKiens(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<MarMarketingDieuKien> SearchMarMarketingDieuKiens(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table;
            return new PagedList<MarMarketingDieuKien>(query, pageIndex, pageSize); ;
        }

        public virtual MarMarketingDieuKien GetMarMarketingDieuKienById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<MarMarketingDieuKien> GetMarMarketingDieuKienByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual IList<MarMarketingDieuKien> GetMarMarketingDieuKiens(int MarId = 0)
        {
            var query = _itemRepository.Table;
            if (MarId > 0)
            {
                query = query.Where(c => c.MARKETING_ID == MarId);
            }

            return query.ToList();
        }

        public virtual void InsertMarMarketingDieuKien(MarMarketingDieuKien entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);
        }
        public virtual void UpdateMarMarketingDieuKien(MarMarketingDieuKien entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteMarMarketingDieuKien(MarMarketingDieuKien entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            _eventPublisher.EntityDeleted(entity);
        }
        public virtual void DeleteMarMarketingDieuKiens(IList<MarMarketingDieuKien> entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
        }

        #endregion
    }
}

