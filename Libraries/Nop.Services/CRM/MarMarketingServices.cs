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
    public partial class MarMarketingService : IMarMarketingService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<MarMarketing> _itemRepository;
        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;
        private readonly IRepository<MarMarketingDieuKien> _marketingDieuKienRepository;
        #endregion

        #region Ctor

        public MarMarketingService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<MarMarketing> itemRepository,
            IStoreContext storeContext,
            IWorkContext workContext,
            IRepository<MarMarketingDieuKien> marketingDieuKienRepository
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._marketingDieuKienRepository = marketingDieuKienRepository;
        }

        #endregion

        #region Methods
        public virtual IList<MarMarketing> GetAllMarMarketings(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<MarMarketing> SearchMarMarketings(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table;
            return new PagedList<MarMarketing>(query, pageIndex, pageSize); ;
        }

        public virtual MarMarketing GetMarMarketingById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<MarMarketing> GetMarMarketingByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual LocDieuKienMarketing LocDieuKienMarketing(int HangKhachHang, Decimal DonGia, DateTime NgayGiaoDich, int DichVuId)
        {
            var query = from m in _itemRepository.Table
                        join dk in _marketingDieuKienRepository.Table on m.Id equals dk.MARKETING_ID
                        where (!(dk.DON_GIA > 0) || dk.DON_GIA <= DonGia)
                        && (!(dk.HANG_KHACH_HANG > 0) || dk.HANG_KHACH_HANG == HangKhachHang)
                        && (!(dk.DICH_VU_ID > 0) || dk.DICH_VU_ID == DichVuId)
                        && (dk.TU_NGAY == null || dk.TU_NGAY <= NgayGiaoDich)
                        && (dk.DEN_NGAY == null || dk.DEN_NGAY >= NgayGiaoDich)
                        && (dk.TU_GIO == null || dk.TU_GIO <= NgayGiaoDich.TimeOfDay)
                        && (dk.DEN_GIO == null || dk.DEN_GIO >= NgayGiaoDich.TimeOfDay)
                        select new
                        {
                            m.TEN,
                            dk.SALE,
                            m.CO_THE_KET_HOP
                        };
            if (query != null && query.Count() > 0)
            {
                var SaleCoTheKetHop = query.Where(c => c.CO_THE_KET_HOP == 1).Sum(c => c.SALE);
                var KhongTheKetHop = query.Where(c => c.CO_THE_KET_HOP == 0).Max(c => c.SALE);

                if (SaleCoTheKetHop >= KhongTheKetHop)
                {
                    return new LocDieuKienMarketing
                    {
                        Sale = (decimal)SaleCoTheKetHop,
                        Ten = string.Join(", ", query.Where(c => c.CO_THE_KET_HOP == 1).Select(c => c.TEN))
                    };
                }
                else
                {
                    return new LocDieuKienMarketing
                    {
                        Sale = (decimal)KhongTheKetHop,
                        Ten = query.Where(c => c.CO_THE_KET_HOP == 1).FirstOrDefault(c => c.SALE == KhongTheKetHop).TEN
                    };
                }
            }
            else
            {
                return new LocDieuKienMarketing();
            }
        }

        public virtual void InsertMarMarketing(MarMarketing entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.DOANH_NGHIEP_ID = _storeContext.CurrentStore.Id;
            entity.NGAY_TAO = DateTime.Now;
            entity.NGUOI_TAO = _workContext.CurrentCustomer.Id;
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }

        public virtual void UpdateMarMarketing(MarMarketing entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }

        public virtual void DeleteMarMarketing(MarMarketing entity)
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

