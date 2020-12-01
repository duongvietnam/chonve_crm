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
    public partial class MarMaGiamGiaService : IMarMaGiamGiaService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<MarMaGiamGia> _itemRepository;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        #endregion

        #region Ctor

        public MarMaGiamGiaService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<MarMaGiamGia> itemRepository,
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
        public virtual IList<MarMaGiamGia> GetAllMarMaGiamGias(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<MarMaGiamGia> SearchMarMaGiamGias(int marId, int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue, int trangThai = 0)
        {
            var query = _itemRepository.Table.Where(c => c.MARKETING_ID == marId);
            if (trangThai == 1)
            {
                query = query.Where(c => c.KHACH_HANG_ID > 0);
            }
            if (trangThai == 2)
            {
                query = query.Where(c => c.NGAY_SU_DUNG != null);
            }
            return new PagedList<MarMaGiamGia>(query.OrderByDescending(c => c.KHACH_HANG_ID).OrderByDescending(c => c.NGAY_SU_DUNG), pageIndex, pageSize);
        }

        public virtual MarMaGiamGia GetMarMaGiamGiaById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<MarMaGiamGia> GetMarMaGiamGiaByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual int GetCountByMa(string ma, int doanhNghiepId)
        {
            return _itemRepository.Table.Where(c => c.MA == ma && c.DOANH_NGHIEP_ID == doanhNghiepId).Count();
        }

        public virtual int GetSoMaGiamGiaDaGuiByMarId(int marId, bool? chuaGuiKhachHang)
        {
            var query = _itemRepository.Table.Where(c => c.MARKETING_ID == marId && c.DOANH_NGHIEP_ID == _storeContext.CurrentStore.Id);
            if (chuaGuiKhachHang != null)
            {
                if ((bool)chuaGuiKhachHang)
                {
                    query = query.Where(c => c.KHACH_HANG_ID == 0 || c.KHACH_HANG_ID == null);
                }
                else
                {
                    query = query.Where(c => c.KHACH_HANG_ID > 0);
                }
            }
            
            return query.Count();
        }

        public virtual int GetSoMaGiamGiaDaSuDungByMarId(int marId)
        {
            return _itemRepository.Table.Where(c => c.MARKETING_ID == marId && c.DOANH_NGHIEP_ID == _storeContext.CurrentStore.Id && c.NGAY_SU_DUNG != null).Count();
        }

        public virtual IList<MarMaGiamGia> GetMarMaGiamGias(int marId, bool chuaGuiKhachHang)
        {
            var query = _itemRepository.Table.Where(c => c.MARKETING_ID == marId && c.DOANH_NGHIEP_ID == _storeContext.CurrentStore.Id);
            if (chuaGuiKhachHang)
            {
                query = query.Where(c => c.KHACH_HANG_ID == 0 || c.KHACH_HANG_ID == null);
            }
            return query.ToList();
        }

        public virtual IList<int> GetListKhachHang(int marId)
        {
            return _itemRepository.Table.Where(c => c.MARKETING_ID == marId && c.KHACH_HANG_ID > 0).Select(c => (int)c.KHACH_HANG_ID).ToList();
        }

        public virtual void InsertMarMaGiamGia(MarMaGiamGia entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_TAO = DateTime.Now;
            entity.NGUOI_TAO = _workContext.CurrentCustomer.Id;
            entity.DOANH_NGHIEP_ID = _storeContext.CurrentStore.Id;
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }

        public virtual void UpdateMarMaGiamGia(MarMaGiamGia entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }

        public virtual void DeleteMarMaGiamGia(MarMaGiamGia entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            _eventPublisher.EntityDeleted(entity);
        }

        public virtual void DeleteMarMaGiamGiaByMarId(int marId)
        {
            var listItem = _itemRepository.Table.Where(c => c.MARKETING_ID == marId);
            if (listItem != null && listItem.Count() > 0)
            {
                _itemRepository.Delete(listItem);
            }
        }
        #endregion
    }
}

