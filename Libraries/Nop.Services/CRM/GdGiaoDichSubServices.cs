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
    public partial class GdGiaoDichSubService : IGdGiaoDichSubService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<GdGiaoDichSub> _itemRepository;
        #endregion

        #region Ctor

        public GdGiaoDichSubService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<GdGiaoDichSub> itemRepository
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion

        #region Methods
        public virtual IList<GdGiaoDichSub> GetAllGdGiaoDichSubs(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<GdGiaoDichSub> SearchGdGiaoDichSubs(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table;
            return new PagedList<GdGiaoDichSub>(query, pageIndex, pageSize);
        }

        public virtual GdGiaoDichSub GetGdGiaoDichSubById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<GdGiaoDichSub> GetGdGiaoDichSubByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual IList<GdGiaoDichSub> GetGdGiaoDichSubs(int giaoDichId = 0)
        {
            var query = _itemRepository.Table;
            if (giaoDichId > 0)
            {
                query = query.Where(c => c.GIAO_DICH_ID == giaoDichId);
            }

            return query.ToList();
        }

        public virtual void InsertGdGiaoDichSub(GdGiaoDichSub entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateGdGiaoDichSub(GdGiaoDichSub entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteGdGiaoDichSub(GdGiaoDichSub entity)
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

