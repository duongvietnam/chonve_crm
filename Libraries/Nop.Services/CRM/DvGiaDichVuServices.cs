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
    public partial class DvGiaDichVuService : IDvGiaDichVuService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DvGiaDichVu> _itemRepository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public DvGiaDichVuService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<DvGiaDichVu> itemRepository,
            IWorkContext workContext
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
        }

        #endregion

        #region Methods
        public virtual IList<DvGiaDichVu> GetAllDvGiaDichVus(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<DvGiaDichVu> SearchDvGiaDichVus(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table;
            return new PagedList<DvGiaDichVu>(query, pageIndex, pageSize); ;
        }

        public virtual DvGiaDichVu GetDvGiaDichVuById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<DvGiaDichVu> GetDvGiaDichVuByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual DvGiaDichVu GetDvGiaDichVu(int dichVuId)
        {
            return _itemRepository.Table.Where(c => c.DICH_VU_ID == dichVuId).FirstOrDefault();
        }

        public virtual void InsertDvGiaDichVu(DvGiaDichVu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_TAO = DateTime.Now;
            entity.NGUOI_TAO = _workContext.CurrentCustomer.Id;
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDvGiaDichVu(DvGiaDichVu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteDvGiaDichVu(DvGiaDichVu entity)
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

