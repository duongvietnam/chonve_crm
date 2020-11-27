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
    public partial class KhNhomHeThongMapService : IKhNhomHeThongMapService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<KhNhomHeThongMap> _itemRepository;
        private readonly IStoreContext _storeContext;
        #endregion

        #region Ctor

        public KhNhomHeThongMapService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<KhNhomHeThongMap> itemRepository,
            IStoreContext storeContext
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._storeContext = storeContext;
        }

        #endregion

        #region Methods
        public virtual IList<KhNhomHeThongMap> GetAllKhNhomHeThongMaps(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<KhNhomHeThongMap> SearchKhNhomHeThongMaps(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table;
            return new PagedList<KhNhomHeThongMap>(query, pageIndex, pageSize); ;
        }

        public virtual KhNhomHeThongMap GetKhNhomHeThongMapById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<KhNhomHeThongMap> GetKhNhomHeThongMapByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual IList<int> GetKhNhomHeThongByNhomId(int nhomId)
        {
            return _itemRepository.Table.Where(c => c.NHOM_KHACH_HANG_ID == nhomId).Select(c => (int)c.NHOM_KHACH_HANG_HE_THONG).ToList();
        }

        public virtual IList<KhNhomHeThongMap> GetKhNhomHeThongMaps(int nhomId = 0)
        {
            var query = _itemRepository.Table;
            if (nhomId > 0)
            {
                query = query.Where(c => c.NHOM_KHACH_HANG_ID == nhomId);
            }

            return query.ToList();
        }

        public virtual void InsertKhNhomHeThongMap(KhNhomHeThongMap entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.DOANH_NGHIEP_ID = _storeContext.CurrentStore.Id;
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateKhNhomHeThongMap(KhNhomHeThongMap entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteKhNhomHeThongMap(KhNhomHeThongMap entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            _eventPublisher.EntityDeleted(entity);
        }
        public virtual void DeleteKhNhomHeThongMaps(IList<KhNhomHeThongMap> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Delete(entities);
        }
        #endregion
    }
}

