//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.BaoCao;
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
    public partial class KhKhachHangService : IKhKhachHangService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<KhKhachHang> _itemRepository;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IChPhanHangKhachHangService _chPhanHangKhachHangService;
        private readonly IRepository<KhNhomKhachHangMap> _khNhomKhachHangMapRepository;
        private readonly IRepository<KhThongKeSuDungDichVu> _khThongKeSuDungRepository;
        private readonly IRepository<GdGiaoDichKhachHangMap> _gdGiaoDichKhachHangMapRepository;
        private readonly IRepository<GdGiaoDich> _gdGiaoDichRepository;
        private readonly INopDataProvider _dataProvider;
        #endregion

        #region Ctor

        public KhKhachHangService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<KhKhachHang> itemRepository,
            IWorkContext workContext,
            IStoreContext storeContext,
            IChPhanHangKhachHangService chPhanHangKhachHangService,
            IRepository<KhNhomKhachHangMap> khNhomKhachHangMapRepository,
            IRepository<KhThongKeSuDungDichVu> khThongKeSuDungRepository,
            IRepository<GdGiaoDichKhachHangMap> gdGiaoDichKhachHangMapRepository,
            INopDataProvider dataProvider,
            IRepository<GdGiaoDich> gdGiaoDichRepository
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._chPhanHangKhachHangService = chPhanHangKhachHangService;
            this._khNhomKhachHangMapRepository = khNhomKhachHangMapRepository;
            this._khThongKeSuDungRepository = khThongKeSuDungRepository;
            this._gdGiaoDichKhachHangMapRepository = gdGiaoDichKhachHangMapRepository;
            this._dataProvider = dataProvider;
            this._gdGiaoDichRepository = gdGiaoDichRepository;
        }

        #endregion

        #region Methods
        public virtual IList<KhKhachHang> GetAllKhKhachHangs(int StoreId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI != (int)EnumTrangThaiKhachHang.DaXoa);
            return query.ToList();
        }

        public virtual IPagedList<KhKhachHang> SearchKhKhachHangs(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue, string email = null,Int32?gender=null, Int32?floor=null, Int32?top=null,Int32?type=null,Int32?daytime=null )
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI != (int)EnumTrangThaiKhachHang.DaXoa);
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
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(c => c.EMAIL.Equals(email));
            }
            if (gender != null && gender.Value!=0) 
            {
                query = query.Where(c => c.GIOI_TINH == gender.Value);
            }
            if (floor != null) 
            {
                query = query.Where(c => c.DIEM_DICH_VU >= floor.Value);
            }
            if (top != null)
            {
                query = query.Where(c => c.DIEM_DICH_VU <= top.Value);
            }
            if (type != null && type.Value!=0) 
            {
                query = query.Where(c => c.LOAI_KHACH_HANG == type.Value);
            }
            if (daytime != null && daytime.Value!=0) 
            {
                var timespan = DateTime.Now;
                var timelimit = timespan.AddDays(-daytime.Value);
                var gdIds = from gd in _gdGiaoDichRepository.Table where gd.NGAY_GIAO_DICH != null && gd.NGAY_GIAO_DICH.Value >= timelimit select gd.Id;
                var gmap = from g in _gdGiaoDichKhachHangMapRepository.Table where g.GIAO_DICH_ID != null && gdIds.Contains(g.GIAO_DICH_ID.Value) select g.KHACH_HANG_ID;
                //query = query.Where(c => gmap!=null && gmap.Contains(c.Id));
                query = from q in query where !gmap.Contains(q.Id) select q;
            }
            return new PagedList<KhKhachHang>(query, pageIndex, pageSize);
        }

        public virtual IPagedList<KhKhachHang> SearchPhanTichKhachHang(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue, bool isHangKhachHang = false, bool isTangDan = true, int HangKhachHang = 0, bool isNhomKhachHang = false, int nhomKhachHang = 0, bool isThoiQuen = false, int TuDiem = 0, int DenDiem = 0, int dichVuId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI != (int)EnumTrangThaiKhachHang.DaXoa);
            var cauHinh = _chPhanHangKhachHangService.GetChPhanHangKhachHangActive(StoreId);
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
            if (isHangKhachHang)
            {
                if (HangKhachHang > 0)
                {
                    switch (HangKhachHang)
                    {
                        case 1:
                            query = query.Where(c => c.DIEM_PHAN_HANG > cauHinh.GIA_TRI_CAP_1 && c.DIEM_PHAN_HANG < cauHinh.GIA_TRI_CAP_2);
                            break;
                        case 2:
                            query = query.Where(c => c.DIEM_PHAN_HANG > cauHinh.GIA_TRI_CAP_2 && c.DIEM_PHAN_HANG < cauHinh.GIA_TRI_CAP_3);
                            break;
                        case 3:
                            query = query.Where(c => c.DIEM_PHAN_HANG > cauHinh.GIA_TRI_CAP_3);
                            break;
                    }
                }

                if (isTangDan)
                {
                    query = query.OrderBy(c => c.DIEM_PHAN_HANG);
                }
                else
                {
                    query = query.OrderByDescending(c => c.DIEM_PHAN_HANG);
                }
            }

            if (isNhomKhachHang)
            {
                if (nhomKhachHang > 0)
                {
                    query = from q in query
                            join n in _khNhomKhachHangMapRepository.Table on q.Id equals n.KHACH_HANG_ID
                            where n.NHOM_KHACH_HANG_ID == nhomKhachHang
                            select q;
                }
                else
                {
                    query = from q in query
                            join n in _khNhomKhachHangMapRepository.Table on q.Id equals n.KHACH_HANG_ID
                            select q;
                }
                if (TuDiem > 0)
                {
                    query = from q in query
                            join n in _khNhomKhachHangMapRepository.Table on q.Id equals n.KHACH_HANG_ID
                            where n.DIEM_DANH_GIA >= TuDiem
                            select q;
                }
                if (DenDiem > 0)
                {
                    query = from q in query
                            join n in _khNhomKhachHangMapRepository.Table on q.Id equals n.KHACH_HANG_ID
                            where n.DIEM_DANH_GIA <= DenDiem
                            select q;
                }
                if (isTangDan)
                {
                    query = from q in query
                            join n in _khNhomKhachHangMapRepository.Table on q.Id equals n.KHACH_HANG_ID
                            orderby n.DIEM_DANH_GIA
                            select q;
                }
                else
                {
                    query = from q in query
                            join n in _khNhomKhachHangMapRepository.Table on q.Id equals n.KHACH_HANG_ID
                            orderby n.DIEM_DANH_GIA descending
                            select q;
                }
            }

            if (isThoiQuen)
            {
                query = from q in query
                        join t in _khThongKeSuDungRepository.Table on q.Id equals t.KHACH_HANG_ID
                        select q;
                if (dichVuId > 0)
                {
                    query = from q in query
                            join m in _gdGiaoDichKhachHangMapRepository.Table on q.Id equals m.KHACH_HANG_ID
                            where m.DICH_VU_ID == dichVuId
                            select q;
                }
            }

            return new PagedList<KhKhachHang>(query, pageIndex, pageSize);
        }

        public virtual KhKhachHang GetKhKhachHangById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<KhKhachHang> GetKhKhachHangByIds(int[] Ids)
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI != (int)EnumTrangThaiKhachHang.DaXoa);
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual IQueryable<KhKhachHang> GetQueryableKhachHang(int doanhNgiepId)
        {
            return _itemRepository.Table.Where(c => c.DOANH_NGHIEP_ID == doanhNgiepId);
        }

        public virtual IList<KhKhachHang> GetKhachHangByCount(int count, string ten)
        {
            return _itemRepository.Table.Where(c => c.TEN.Contains(ten)).Take(count).ToList();
        }

        public virtual void InsertKhKhachHang(KhKhachHang entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_TAO = DateTime.Now;
            entity.NGUOI_TAO = _workContext.CurrentCustomer?.Id??0;
            entity.DOANH_NGHIEP_ID = _storeContext.CurrentStore.Id;
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }

        public virtual void UpdateKhKhachHang(KhKhachHang entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }

        public virtual void DeleteKhKhachHang(KhKhachHang entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.TRANG_THAI = (int)EnumTrangThaiKhachHang.DaXoa;
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }

        #endregion
        #region Bao Cao Thong Ke
        public virtual IList<BieuDo> GetThongKeKhachHangTheoPhanHang(int StoreId, int IdTieuChiPH )
        {
            var pStoreId = SqlParameterHelper.GetInt32Parameter("StoreId", StoreId);
            var pIdTieuChiPH = SqlParameterHelper.GetInt32Parameter("PHId", IdTieuChiPH);
            var items = _dataProvider.QueryProc<BieuDo>("CRM_ThongKe_KhachHangTheoPhanHang", pStoreId, pIdTieuChiPH);
            return items.ToList();
        }
        #endregion
    }
}

