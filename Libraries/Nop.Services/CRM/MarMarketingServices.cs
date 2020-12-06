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
        private readonly IRepository<MarMaGiamGia> _marMaGiamGiaRepository;
        private readonly IRepository<DvDonViTinh> _donViTinhRepository;
        #endregion

        #region Ctor

        public MarMarketingService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<MarMarketing> itemRepository,
            IStoreContext storeContext,
            IWorkContext workContext,
            IRepository<MarMarketingDieuKien> marketingDieuKienRepository,
            IRepository<MarMaGiamGia> marMaGiamGiaRepository,
            IRepository<DvDonViTinh> donViTinhRepository
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._marketingDieuKienRepository = marketingDieuKienRepository;
            this._marMaGiamGiaRepository = marMaGiamGiaRepository;
            this._donViTinhRepository = donViTinhRepository;
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

        public virtual LocDieuKienMaGiamGia CheckMaGiamGia(string ma, int doanhNgiepId, decimal donGia, DateTime ngayGiaoDich)
        {
            var maGiamGia = _marMaGiamGiaRepository.Table.FirstOrDefault(c => c.MA == ma && c.DOANH_NGHIEP_ID == doanhNgiepId);
            var mar = _itemRepository.GetById(maGiamGia.MARKETING_ID);
            var dieuKien = _marketingDieuKienRepository.GetById(maGiamGia.MAR_DIEU_KIEN_ID);
            var item = new LocDieuKienMaGiamGia
            {
                MaGiamGia = ma,
                CoTheKetHop = mar.CO_THE_KET_HOP == 1,
                DonViTinh = _donViTinhRepository.GetById(dieuKien.DON_VI_TINH).TEN,
                Sale = (decimal)dieuKien.SALE,
                TrangThai = "Còn hiệu lực",
                DenNgay = dieuKien.DEN_NGAY,
                TuNgay = dieuKien.TU_NGAY,
                DonGia = dieuKien.DON_GIA > 0 ? (decimal)dieuKien.DON_GIA : 0
            };
            if (dieuKien.TU_NGAY != null && dieuKien.TU_NGAY > ngayGiaoDich)
                item.TrangThai = "Mã hết hạn";
            if (dieuKien.DEN_NGAY != null && dieuKien.DEN_NGAY < ngayGiaoDich)
                item.TrangThai = "Mã chưa đến hạn";
            if (donGia > 0 && dieuKien.DON_GIA > donGia)
                item.TrangThai = "Đơn giá thấp hơn yêu cầu";
            if (maGiamGia.NGAY_SU_DUNG != null)
                item.TrangThai = "Mã đã được sử dụng";

            return item;
        }

        public virtual string ApDungMaGiamGia(IList<string> listMa, int doanhNgiepId, int khachHangId, int giaoDichId, DateTime ngayGiaoDich)
        {
            try
            {
                if (listMa.Count() > 1)
                {
                    var listMarId = _marMaGiamGiaRepository.Table.Where(c => listMa.Contains(c.MA)).Select(c => c.MARKETING_ID);
                    var listMarketing = _itemRepository.Table.Where(c => listMarId.Contains(c.Id));
                    if (listMarketing.Any(c => c.CO_THE_KET_HOP == 0))
                    {
                        IList<int> listMarKhongHopLeId = listMarketing.Where(c => c.CO_THE_KET_HOP == 0).Select(c => c.Id).ToList();
                        string maKhongPhuHop = string.Join(", ", _marMaGiamGiaRepository.Table.Where(c => listMarKhongHopLeId.Contains((int)c.MARKETING_ID)).Select(c => c.MA));

                        return "Mã " + maKhongPhuHop + " không được sử dụng kết hợp";
                    }
                }

                foreach (string ma in listMa)
                {
                    var maGiamGia = _marMaGiamGiaRepository.Table.FirstOrDefault(c => c.MA == ma && c.DOANH_NGHIEP_ID == doanhNgiepId);
                    if(khachHangId > 0)
                    {
                        maGiamGia.KHACH_HANG_ID = khachHangId;
                    }
                    maGiamGia.NGAY_SU_DUNG = ngayGiaoDich;
                    maGiamGia.GIAO_DICH_AP_DUNG = giaoDichId;

                    _marMaGiamGiaRepository.Update(maGiamGia);
                }

                return "Áp dụng mã thành công";
            }
            catch (Exception ex)
            {
                return "Có lỗi trong quá trình xử lý: " + ex;
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

