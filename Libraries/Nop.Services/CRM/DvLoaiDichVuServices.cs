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
    public partial class DvLoaiDichVuService : IDvLoaiDichVuService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DvLoaiDichVu> _itemRepository;
        private readonly IStoreContext _storeContext;
        private readonly CachingSettings _cachingSettings;
        #endregion

        #region Ctor

        public DvLoaiDichVuService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<DvLoaiDichVu> itemRepository,
            IStoreContext storeContext,
            CachingSettings cachingSettings
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._storeContext = storeContext;
            this._cachingSettings = cachingSettings;
        }

        #endregion

        #region Methods
        public virtual IList<DvLoaiDichVu> GetAllDvLoaiDichVus(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<DvLoaiDichVu> SearchDvLoaiDichVus(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table;
            if (StoreId > 0)
            {
                query = query.Where(c => c.DOANH_NGHIEP_ID == StoreId);
            }
            else
            {
                query = query.Where(c => c.DOANH_NGHIEP_ID == _storeContext.CurrentStore.Id);
            }
            if (!string.IsNullOrEmpty(Keysearch))
            {
                query = query.Where(c => c.TEN.Contains(Keysearch));
            }
            return new PagedList<DvLoaiDichVu>(query, pageIndex, pageSize); ;
        }

        public virtual DvLoaiDichVu GetDvLoaiDichVuById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id, _cachingSettings.ShortTermCacheTime);
        }

        public virtual IList<DvLoaiDichVu> GetDvLoaiDichVuByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual int CountLoaiDichVu(string tenDichVu)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(tenDichVu))
            {
                query = query.Where(c => c.TEN == tenDichVu);
            }
            return query.Count();
        }

        public virtual void InsertDvLoaiDichVu(DvLoaiDichVu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.DOANH_NGHIEP_ID = _storeContext.CurrentStore.Id;
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDvLoaiDichVu(DvLoaiDichVu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteDvLoaiDichVu(DvLoaiDichVu entity)
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

