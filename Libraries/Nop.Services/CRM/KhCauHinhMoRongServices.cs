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
    public partial class KhCauHinhMoRongService : IKhCauHinhMoRongService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<KhCauHinhMoRong> _itemRepository;
        private readonly IStoreContext _storeContext;
        #endregion

        #region Ctor

        public KhCauHinhMoRongService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<KhCauHinhMoRong> itemRepository,
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
        public virtual IList<KhCauHinhMoRong> GetAllKhCauHinhMoRongs(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<KhCauHinhMoRong> SearchKhCauHinhMoRongs(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
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
            return new PagedList<KhCauHinhMoRong>(query, pageIndex, pageSize); ;
        }

        public virtual KhCauHinhMoRong GetKhCauHinhMoRongById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<KhCauHinhMoRong> GetKhCauHinhMoRongByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual IList<KhCauHinhMoRong> GetCauHinhMoRongs(int DoanhNghiepId)
        {
            var query = _itemRepository.Table.Where(c => c.DOANH_NGHIEP_ID == DoanhNghiepId);

            return query.ToList();
        }

        public virtual int GetCountCauHinhMoRong(string ten = null)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(ten))
            {
                query = query.Where(c => c.TEN == ten);
            }
            return query.Count();
        }

        public virtual void InsertKhCauHinhMoRong(KhCauHinhMoRong entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.DOANH_NGHIEP_ID = _storeContext.CurrentStore.Id;
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateKhCauHinhMoRong(KhCauHinhMoRong entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteKhCauHinhMoRong(KhCauHinhMoRong entity)
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

