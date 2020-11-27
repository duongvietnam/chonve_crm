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
    public partial class KhDanhBaDienThoaiService : IKhDanhBaDienThoaiService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<KhDanhBaDienThoai> _itemRepository;
        private readonly IWorkContext _workContext;
        private readonly IRepository<KhKhachHang> _khachHangRepository;
        #endregion

        #region Ctor

        public KhDanhBaDienThoaiService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<KhDanhBaDienThoai> itemRepository,
            IWorkContext workContext,
            IRepository<KhKhachHang> khachHangRepository
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
            this._khachHangRepository = khachHangRepository;
        }

        #endregion

        #region Methods
        public virtual IList<KhDanhBaDienThoai> GetAllKhDanhBaDienThoais(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<KhDanhBaDienThoai> SearchKhDanhBaDienThoais(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table;
            return new PagedList<KhDanhBaDienThoai>(query, pageIndex, pageSize);
        }

        public virtual KhDanhBaDienThoai GetKhDanhBaDienThoaiById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<KhDanhBaDienThoai> GetKhDanhBaDienThoaiByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual IList<KhDanhBaDienThoai> GetKhDanhBaDienThoaiByKhachHang(int khachHangId, EnumTrangThaiDanhBa trangThai = EnumTrangThaiDanhBa.HoatDong)
        {
            return _itemRepository.Table.Where(c => c.KHACH_HANG_ID == khachHangId && c.TRANG_THAI == (int)EnumTrangThaiDanhBa.HoatDong).ToList();
        }

        public virtual KhDanhBaDienThoai GetKhDanhBaDienThoaiBySoDienThoai(string soDienThoai, int DoanhNghiepId)
        {
            return _itemRepository.Table
                .FirstOrDefault(c => c.SO_DIEN_THOAI == soDienThoai && c.TRANG_THAI == (int)EnumTrangThaiDanhBa.HoatDong);
        }

        public virtual void InsertKhDanhBaDienThoai(KhDanhBaDienThoai entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_TAO = DateTime.Now;
            entity.NGUOI_TAO = _workContext.CurrentCustomer.Id;
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateKhDanhBaDienThoai(KhDanhBaDienThoai entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteKhDanhBaDienThoai(KhDanhBaDienThoai entity)
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

