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
    public partial class DvDichVuService : IDvDichVuService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DvDichVu> _itemRepository;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        #endregion

        #region Ctor

        public DvDichVuService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<DvDichVu> itemRepository,
            IWorkContext workContext,
            IStoreContext storeContext
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
            this._storeContext = storeContext;
        }

        #endregion

        #region Methods
        public virtual IList<DvDichVu> GetAllDvDichVus(bool isCombo, int StoreId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.IS_COMBO == Convert.ToInt32(isCombo));
            return query.ToList();
        }

        public virtual IPagedList<DvDichVu> SearchDvDichVus(bool isCombo, int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table.Where(c => Convert.ToBoolean(c.IS_COMBO) == isCombo);
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
            return new PagedList<DvDichVu>(query, pageIndex, pageSize);
        }

        public virtual DvDichVu GetDvDichVuById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual DvDichVu GetDvDichVuByIdNguon(int IdNguon, int DoanhNghiepId)
        {
            return _itemRepository.Table.FirstOrDefault(c => c.ID_NGUON == IdNguon && c.DOANH_NGHIEP_ID == DoanhNghiepId);
        }

        public virtual IList<DvDichVu> GetDvDichVuByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public int CountDichVu(string TenDichVu = null, int isCombo = -1)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(TenDichVu))
            {
                query = query.Where(c => c.TEN == TenDichVu);
            }
            if (isCombo > -1)
            {
                query = query.Where(c => c.IS_COMBO == isCombo);
            }
            return query.Count();
        }

        public virtual void InsertDvDichVu(DvDichVu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_TAO = DateTime.Now;
            entity.NGUOI_TAO = _workContext.CurrentCustomer.Id;
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDvDichVu(DvDichVu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteDvDichVu(DvDichVu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.TRANG_THAI = (int)EnumTrangThaiDichVu.DaXoa;
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }

        #endregion
    }
}

