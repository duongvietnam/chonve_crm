//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Razor.Language;
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
    public partial class GdGiaoDichService : IGdGiaoDichService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<GdGiaoDich> _itemRepository;
        private readonly IRepository<GdGiaoDichKhachHangMap> _giaoDichMapRepository;
        private readonly INopDataProvider _dataProvider;
        private readonly IStoreContext _storeContext;
        #endregion

        #region Ctor

        public GdGiaoDichService(
            INopDataProvider dataProvider,
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<GdGiaoDich> itemRepository,
            IRepository<GdGiaoDichKhachHangMap> giaoDichMapRepository,
            IStoreContext storeContext
            )
        {
            this._dataProvider = dataProvider;
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._giaoDichMapRepository = giaoDichMapRepository;
            this._storeContext = storeContext;
        }

        #endregion

        #region Methods
        public virtual IList<GdGiaoDich> GetAllGdGiaoDichs(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IList<GdGiaoDich> GetTopGiaoDichByTime(int StoreId, int SoGiaoDich = 8)
        {
            var query = _itemRepository.Table;
            return query.OrderByDescending(i => i.NGAY_GIAO_DICH).Where(i => i.DOANH_NGHIEP_ID==StoreId).Take(SoGiaoDich).ToList();
        }
        public virtual IPagedList<GdGiaoDich> SearchGdGiaoDichs(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table.OrderByDescending(c => c.NGAY_GIAO_DICH);
            return new PagedList<GdGiaoDich>(query, pageIndex, pageSize);
        }

        public virtual GdGiaoDich GetGdGiaoDichById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<GdGiaoDich> GetGdGiaoDichByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual GdGiaoDich GetGiaoDichByMaNguon(string MaNguon, int DoanhNghiepId)
        {
            return _itemRepository.Table.FirstOrDefault(c => c.MA_GIAO_DICH == MaNguon && c.DOANH_NGHIEP_ID == DoanhNghiepId);
        }

        public virtual Decimal GetTongSoTienDaChiTieu(int khachHangId, DateTime? TuNgay = null, DateTime? DenNgay = null)
        {
            var giaoDich = _itemRepository.Table;
            if (TuNgay != null)
            {
                giaoDich = giaoDich.Where(c => c.NGAY_GIAO_DICH >= TuNgay);
            }
            if (DenNgay != null)
            {
                giaoDich = giaoDich.Where(c => c.NGAY_GIAO_DICH <= DenNgay);
            }
            var query = from gd in giaoDich
                        join gdm in _giaoDichMapRepository.Table on gd.Id equals gdm.GIAO_DICH_ID
                        where gdm.KHACH_HANG_ID == khachHangId
                        select gd.THANH_TIEN;

            return query.Count() > 0 ? (decimal)query.Sum() : 0;
        }

        public virtual Decimal GetSoTienDaChiTieu(int khachHangId, DateTime? TuNgay, DateTime? DenNgay)
        {
            var pStoreId = SqlParameterHelper.GetInt32Parameter("StoreId", _storeContext.CurrentStore.Id);
            var pKhachHangId = SqlParameterHelper.GetInt32Parameter("KhachHangId", khachHangId);
            var pTuNgay = SqlParameterHelper.GetDateTimeParameter("FromDate", TuNgay);
            var pDenNgay = SqlParameterHelper.GetDateTimeParameter("ToDate", DenNgay);
            var item = _dataProvider.QueryProc<Decimal>("CRM_ThongKe_DoanhThuKhachHang", pStoreId, pKhachHangId, pTuNgay, pDenNgay);

            return item.Sum();
        }

        public virtual IList<GdGiaoDich> GetGiaoDichs(int khachHangId = 0)
        {
            var query = from gd in _itemRepository.Table
                        join gdm in _giaoDichMapRepository.Table on gd.Id equals gdm.GIAO_DICH_ID
                        where gdm.KHACH_HANG_ID == khachHangId
                        select gd;

            return query.ToList();
        }

        public virtual void InsertGdGiaoDich(GdGiaoDich entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateGdGiaoDich(GdGiaoDich entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteGdGiaoDich(GdGiaoDich entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            _eventPublisher.EntityDeleted(entity);
        }

        #endregion
        #region Bao c?o th?ng k?
        public virtual IList<BieuDo> GetThongKeDoanhThu(int StoreId, int Nam, int Thang, int Ngay)
        {
            var pStoreId = SqlParameterHelper.GetInt32Parameter("StoreId", StoreId);
            var pNam = SqlParameterHelper.GetInt32Parameter("Nam", Nam);
            var pThang = SqlParameterHelper.GetInt32Parameter("Thang", Thang);
            var pNgay = SqlParameterHelper.GetInt32Parameter("Ngay", Ngay);
            var items = _dataProvider.QueryProc<BieuDo>("CRM_ThongKe_DoanhThu", pStoreId, pNam, pThang, pNgay);            
            return items.ToList();
        }

        public virtual IList<BieuDo> GetThongKeChiTieuKhachHang(int StoreId, int KhId, int Nam, int Thang, int Ngay)
        {
            var pStoreId = SqlParameterHelper.GetInt32Parameter("StoreId", StoreId);
            var pKhId = SqlParameterHelper.GetInt32Parameter("KhId", KhId);
            var pNam = SqlParameterHelper.GetInt32Parameter("Nam", Nam);
            var pThang = SqlParameterHelper.GetInt32Parameter("Thang", Thang);
            var pNgay = SqlParameterHelper.GetInt32Parameter("Ngay", Ngay);
            var items = _dataProvider.QueryProc<BieuDo>("CRM_ThongKe_ChiTieuKhachHang", pStoreId, pKhId, pNam, pThang, pNgay);
            return items.ToList();
        }

        public virtual IList<BieuDo1> GetThongKeDoanhThuDichVu(int StoreId, int Nam, int Thang, int Ngay)
        {
            var pStoreId = SqlParameterHelper.GetInt32Parameter("StoreId", StoreId);
            var pNam = SqlParameterHelper.GetInt32Parameter("Nam", Nam);
            var pThang = SqlParameterHelper.GetInt32Parameter("Thang", Thang);
            var pNgay = SqlParameterHelper.GetInt32Parameter("Ngay", Ngay);
            var items = _dataProvider.QueryProc<BieuDo1>("CRM_ThongKe_DoanhThuDichVu", pStoreId, pNam, pThang, pNgay);
            return items.ToList();
        }
        public virtual IList<BieuDo> GetThongKeGiaoDich(int StoreId, int SoNam, bool Thang, bool Ngay, bool Gio)
        {
            var pStoreId = SqlParameterHelper.GetInt32Parameter("StoreId", StoreId);
            var pSoNam = SqlParameterHelper.GetInt32Parameter("SoNam", SoNam);
            var pThang = SqlParameterHelper.GetBooleanParameter("Thang", Thang);
            var pNgay = SqlParameterHelper.GetBooleanParameter("Ngay", Ngay);
            var pGio = SqlParameterHelper.GetBooleanParameter("Gio", Gio);
            var items = _dataProvider.QueryProc<BieuDo>("CRM_ThongKe_GiaoDichNgayThang", pStoreId, pSoNam, pThang, pNgay, pGio);
            return items.ToList();
        }
        public virtual IList<BieuDo> GetThongKeSoSanhGiaoDichGioNgayThang(int StoreId, int SoMoc, int Nam, int Thang, bool weekday)
        {
            var pStoreId = SqlParameterHelper.GetInt32Parameter("StoreId", StoreId);
            var pSoMoc = SqlParameterHelper.GetInt32Parameter("SoMoc", SoMoc);
            var pNam = SqlParameterHelper.GetInt32Parameter("Nam", Nam);
            var pThang = SqlParameterHelper.GetInt32Parameter("Thang", Thang);
            var pNgayTuan = SqlParameterHelper.GetBooleanParameter("NgayTuan", weekday);
            var items = _dataProvider.QueryProc<BieuDo>("CRM_ThongKe_SoSanhDoanhThuGioNgayThang", pStoreId, pSoMoc, pNam, pThang, pNgayTuan);
            return items.ToList();
        }
        public virtual IList<BieuDo> GetThongKeBaoCaoGiaoDichCungKy(int StoreId, int Nam, int Thang, int Ngay, int Tuan)
        {
            var pStoreId = SqlParameterHelper.GetInt32Parameter("StoreId", StoreId);
            var pNam = SqlParameterHelper.GetInt32Parameter("Nam", Nam);
            var pThang = SqlParameterHelper.GetInt32Parameter("Thang", Thang);
            var pNgay = SqlParameterHelper.GetInt32Parameter("Ngay", Ngay);
            var pTuan = SqlParameterHelper.GetInt32Parameter("Tuan", Tuan);
            var items = _dataProvider.QueryProc<BieuDo>("CRM_ThongKe_BaoCaoDoanhThuCungKy", pStoreId, pNam, pThang, pNgay, pTuan);
            return items.ToList();
        }
        #endregion
    }
}

