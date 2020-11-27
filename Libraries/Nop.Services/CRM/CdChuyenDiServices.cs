//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 26/11/2020
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
    public partial class CdChuyenDiService : ICdChuyenDiService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<CdChuyenDi> _itemRepository;
        #endregion

        #region Ctor

        public CdChuyenDiService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<CdChuyenDi> itemRepository
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion

        #region Methods
        public virtual IList<CdChuyenDi> GetAllCdChuyenDis(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<CdChuyenDi> SearchCdChuyenDis(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table;
            return new PagedList<CdChuyenDi>(query, pageIndex, pageSize); ;
        }

        public virtual CdChuyenDi GetCdChuyenDiById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<CdChuyenDi> GetCdChuyenDiByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual CdChuyenDi GetChuyenDi(string ma, int doanhNghiepId)
        {
            var query = _itemRepository.Table.Where(c => c.DOANH_NGHIEP_ID == doanhNghiepId);

            if (!string.IsNullOrEmpty(ma))
                query = query.Where(c => c.MA == ma);

            return query.FirstOrDefault();
        }

        public virtual void InsertCdChuyenDi(CdChuyenDi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateCdChuyenDi(CdChuyenDi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteCdChuyenDi(CdChuyenDi entity)
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

