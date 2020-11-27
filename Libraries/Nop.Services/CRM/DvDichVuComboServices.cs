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
    public partial class DvDichVuComboService : IDvDichVuComboService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DvDichVuCombo> _itemRepository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public DvDichVuComboService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<DvDichVuCombo> itemRepository,
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
        public virtual IList<DvDichVuCombo> GetAllDvDichVuCombos(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<DvDichVuCombo> SearchDvDichVuCombos(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table;
            return new PagedList<DvDichVuCombo>(query, pageIndex, pageSize); ;
        }

        public virtual DvDichVuCombo GetDvDichVuComboById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<DvDichVuCombo> GetDvDichVuComboByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual IList<int> GetDichVuTrongCombo(int ComboId)
        {
            return _itemRepository.Table.Where(c => c.DICH_VU_COMBO == ComboId).Select(c => (int)c.DICH_VU_ID).ToList();
        }

        public virtual IList<DvDichVuCombo> GetDichVuCombos(int comBoId = 0)
        {
            var query = _itemRepository.Table;
            if (comBoId > 0)
            {
                query = query.Where(c => c.DICH_VU_COMBO == comBoId);
            }
            return query.ToList();
        }

        public virtual void InsertDvDichVuCombo(DvDichVuCombo entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_TAO = DateTime.Now;
            entity.NGUOI_TAO = _workContext.CurrentCustomer.Id;
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDvDichVuCombo(DvDichVuCombo entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteDvDichVuCombo(DvDichVuCombo entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            _eventPublisher.EntityDeleted(entity);
        }
        public virtual void DeleteDvDichVuCombos(IList<DvDichVuCombo> entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
        }
        #endregion
    }
}

