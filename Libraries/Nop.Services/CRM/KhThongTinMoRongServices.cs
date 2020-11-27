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

namespace Nop.Services.CRM
{
    public partial class KhThongTinMoRongService : IKhThongTinMoRongService
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheKeyService _cacheKeyService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<KhThongTinMoRong> _itemRepository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public KhThongTinMoRongService(
            IEventPublisher eventPublisher,
            ICacheKeyService cacheKeyService,
            IStaticCacheManager staticCacheManager,
            IRepository<KhThongTinMoRong> itemRepository,
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
        public virtual IList<KhThongTinMoRong> GetAllKhThongTinMoRongs(int StoreId = 0)
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<KhThongTinMoRong> SearchKhThongTinMoRongs(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _itemRepository.Table;
            return new PagedList<KhThongTinMoRong>(query, pageIndex, pageSize); ;
        }

        public virtual KhThongTinMoRong GetKhThongTinMoRongById(int Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.ToCachedGetById(Id);
        }

        public virtual IList<KhThongTinMoRong> GetKhThongTinMoRongByIds(int[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.Id)).ToList();
        }

        public virtual KhThongTinMoRong GetKhThongTinMoRong(int khachHangId, int cauHinhId)
        {
            return _itemRepository.Table.Where(c => c.KHACH_HANG_ID == khachHangId && c.CAU_HINH_ID == cauHinhId).FirstOrDefault();
        }

        public virtual void InsertKhThongTinMoRong(KhThongTinMoRong entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_TAO = DateTime.Now;
            entity.NGUOI_TAO = _workContext.CurrentCustomer.Id;
            _itemRepository.Insert(entity);
            //event notification
            _eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateKhThongTinMoRong(KhThongTinMoRong entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            _eventPublisher.EntityUpdated(entity);
        }
        public virtual void DeleteKhThongTinMoRong(KhThongTinMoRong entity)
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

