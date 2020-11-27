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
using Nop.Core.Domain.BaoCao;

namespace Nop.Services.CRM
{
    public partial class KhNhomKhachHangMapService : IKhNhomKhachHangMapService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<KhNhomKhachHangMap> _itemRepository;
        private readonly INopDataProvider _dataProvider;
        #endregion

        #region Ctor

        public KhNhomKhachHangMapService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<KhNhomKhachHangMap> itemRepository,
            INopDataProvider dataProvider
            )
        {
            this._eventPublisher = eventPublisher;
            this._cacheKeyService = cacheKeyService;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._dataProvider = dataProvider;
        }

        #endregion

        #region Methods
        public virtual IList<KhNhomKhachHangMap> GetAllKhNhomKhachHangMaps(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<KhNhomKhachHangMap> SearchKhNhomKhachHangMaps(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table;
            return new PagedList<KhNhomKhachHangMap>(query, pageIndex, pageSize); ;
        }

        public virtual KhNhomKhachHangMap GetKhNhomKhachHangMapById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<KhNhomKhachHangMap> GetKhNhomKhachHangMapByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual IList<KhNhomKhachHangMap> GetKhNhomKhachHangMapByIdKhachHang(int IdKhachHang)
        {
            var query = _itemRepository.Table;
            return query.Where(n => n.KHACH_HANG_ID.Equals(IdKhachHang)).ToList();
        }

        public virtual IList<int> GetKhachHangIdsByNhomId(IList<int> listNhomId)
        {
            return _itemRepository.Table.Where(c => listNhomId.Contains((int)c.NHOM_KHACH_HANG_ID)).Select(c => (int)c.KHACH_HANG_ID).ToList();
        }

        public virtual void InsertKhNhomKhachHangMap(KhNhomKhachHangMap entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }

        public virtual void UpdateKhNhomKhachHangMap(KhNhomKhachHangMap entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }

        public virtual void DeleteKhNhomKhachHangMap(KhNhomKhachHangMap entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            _eventPublisher.EntityDeleted(entity);
        }

        #endregion
        #region Thong ke

        public virtual IList<BieuDo> ThongKeSoLuongKhByNhomKH(int StoreId)
        {
            var pStoreId = SqlParameterHelper.GetInt32Parameter("StoreId", StoreId);
            var item = _dataProvider.QueryProc<BieuDo>("CRM_ThongKe_SoLuongKhachHangTheoNhom", pStoreId);
            return item.ToList();
        }
        #endregion
    }
}

