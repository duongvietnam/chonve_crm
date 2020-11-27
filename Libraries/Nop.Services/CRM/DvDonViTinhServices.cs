//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 21/9/2020
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
    public partial class DvDonViTinhService : IDvDonViTinhService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DvDonViTinh> _itemRepository;
        private readonly IStoreContext _storeContext;
        #endregion

        #region Ctor

        public DvDonViTinhService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<DvDonViTinh> itemRepository,
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
        public virtual IList<DvDonViTinh> GetAllDvDonViTinhs(int StoreId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.DOANH_NGHIEP_ID == _storeContext.CurrentStore.Id);
            return query.ToList();
        }

        public virtual IPagedList<DvDonViTinh> SearchDvDonViTinhs(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table.Where(c => c.DOANH_NGHIEP_ID == _storeContext.CurrentStore.Id);
            return new PagedList<DvDonViTinh>(query, pageIndex, pageSize);
        }

        public virtual DvDonViTinh GetDvDonViTinhById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<DvDonViTinh> GetDvDonViTinhByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual DvDonViTinh GetByMa(string ma)
        {
            return _itemRepository.Table.FirstOrDefault(c => c.MA == ma && c.DOANH_NGHIEP_ID == _storeContext.CurrentStore.Id);
        }

        public virtual void InsertDvDonViTinh(DvDonViTinh entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.DOANH_NGHIEP_ID = _storeContext.CurrentStore.Id;
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDvDonViTinh(DvDonViTinh entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteDvDonViTinh(DvDonViTinh entity)
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

