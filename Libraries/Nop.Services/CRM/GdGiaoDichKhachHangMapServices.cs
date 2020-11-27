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
using System.Linq.Dynamic.Core;

namespace Nop.Services.CRM
{
    public partial class GdGiaoDichKhachHangMapService : IGdGiaoDichKhachHangMapService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<GdGiaoDichKhachHangMap> _itemRepository;
        private readonly IRepository<GdGiaoDich> _gdGiaoDichRepository;
        #endregion

        #region Ctor

        public GdGiaoDichKhachHangMapService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<GdGiaoDichKhachHangMap> itemRepository,
            IRepository<GdGiaoDich> gdGiaoDichRepository
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._gdGiaoDichRepository = gdGiaoDichRepository;
        }

        #endregion

        #region Methods
        public virtual IList<GdGiaoDichKhachHangMap> GetAllGdGiaoDichKhachHangMaps(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<GdGiaoDichKhachHangMap> SearchGdGiaoDichKhachHangMaps(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table;
            return new PagedList<GdGiaoDichKhachHangMap>(query, pageIndex, pageSize);
        }

        public virtual GdGiaoDichKhachHangMap GetGdGiaoDichKhachHangMapById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<GdGiaoDichKhachHangMap> GetGdGiaoDichKhachHangMapByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual IQueryable<GdGiaoDichKhachHangMap> GetGdGiaoDichKhachHangMaps(int khachHangId = 0, DateTime? TuNgay = null, DateTime? DenNgay = null)
        {
            var query = _itemRepository.Table;
            if (khachHangId > 0)
            {
                query = query.Where(c => c.KHACH_HANG_ID == khachHangId);
            }
            if (TuNgay != null)
            {
                query = from q in query
                        join gd in _gdGiaoDichRepository.Table on q.GIAO_DICH_ID equals gd.Id
                        where gd.NGAY_GIAO_DICH >= TuNgay
                        select q;
            }
            if (DenNgay != null)
            {
                query = from q in query
                        join gd in _gdGiaoDichRepository.Table on q.GIAO_DICH_ID equals gd.Id
                        where gd.NGAY_GIAO_DICH <= DenNgay
                        select q;
            }
            return query;
        }

        public virtual string SoLanSuDungDichVuTrongNam(int khachHangId, DateTime TuNgay, DateTime DenNgay)
        {
            var query = from m in _itemRepository.Table
                        join g in _gdGiaoDichRepository.Table on m.GIAO_DICH_ID equals g.Id
                        where m.KHACH_HANG_ID == khachHangId
                        select new
                        {
                            g.NGAY_GIAO_DICH
                        };

            return query.Where(c => c.NGAY_GIAO_DICH >= TuNgay && c.NGAY_GIAO_DICH <= DenNgay).Count() + " / " + query.Count();
        }

        public virtual IList<GdGiaoDichKhachHangMap> GetGdGiaoDichKhachHangMapByGiaoDichId(int giaoDichId, int doanhNghiepId)
        {
            return _itemRepository.Table.Where(c => c.GIAO_DICH_ID == giaoDichId && c.DOANH_NGHIEP_ID == doanhNghiepId).ToList();
        }

        public virtual IList<int> GetListKhachHangId(int giaoDichId)
        {
            return _itemRepository.Table.Where(c => c.GIAO_DICH_ID == giaoDichId).Select(c => (int)c.KHACH_HANG_ID).ToList();
        }

        public virtual IList<GdGiaoDichKhachHangMap> GetKHsByGDIds(int[] GiaodichIds )
        {
            return _itemRepository.Table.Where( t => t.GIAO_DICH_ID!=null && GiaodichIds.Contains(t.GIAO_DICH_ID.Value))?.ToList();
        }
        public virtual void InsertGdGiaoDichKhachHangMap(GdGiaoDichKhachHangMap entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateGdGiaoDichKhachHangMap(GdGiaoDichKhachHangMap entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteGdGiaoDichKhachHangMap(GdGiaoDichKhachHangMap entity)
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

