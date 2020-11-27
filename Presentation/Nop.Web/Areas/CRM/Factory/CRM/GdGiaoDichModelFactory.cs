//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.CRM;
using Nop.Services.CRM;
using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.CRM.Models.CRM;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public class GdGiaoDichModelFactory : IGdGiaoDichModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IGdGiaoDichService _itemService;
        private readonly IKhKhachHangService _khKhachHangService;
        private readonly IGdGiaoDichKhachHangMapService _gdGiaoDichKhachHangMapService;
        #endregion

        #region Ctor

        public GdGiaoDichModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IGdGiaoDichService itemService,
            IKhKhachHangService khKhachHangService,
            IGdGiaoDichKhachHangMapService gdGiaoDichKhachHangMapService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._khKhachHangService = khKhachHangService;
            this._gdGiaoDichKhachHangMapService = gdGiaoDichKhachHangMapService;
        }
        #endregion

        #region GdGiaoDich
        public GdGiaoDichSearchModel PrepareGdGiaoDichSearchModel(GdGiaoDichSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public GdGiaoDichListModel PrepareGdGiaoDichListModel(GdGiaoDichSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchGdGiaoDichs(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new GdGiaoDichListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<GdGiaoDichModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<GdGiaoDichModel>();
                    var listKhachHangId = _gdGiaoDichKhachHangMapService.GetListKhachHangId(item.Id);
                    itemModel.KhachHangSrting = string.Join(", ", _khKhachHangService.GetKhKhachHangByIds(listKhachHangId.ToArray()).Select(c => c.TEN));
                    itemModel.ThanhTienString = item.THANH_TIEN.ToVNStringPrice();
                    itemModel.NgayGiaoDich = item.NGAY_GIAO_DICH.toDateVNString();
                    itemModel.EnumHinhThucThanhToanValue = CommonHelper.GetEnumDescription((EnumHinhThucThanhToan)item.HINH_THUC_THANH_TOAN);
                    itemModel.EnumTrangThaiGiaoDichValue = CommonHelper.GetEnumDescription((EnumTrangThaiGiaoDich)item.TRANG_THAI);
                    ls.Add(itemModel);
                }
                return ls;

            });

            return model;
        }
        public GdGiaoDichModel PrepareGdGiaoDichModel(GdGiaoDichModel model, GdGiaoDich item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<GdGiaoDichModel>();
            }
            //more

            return model;
        }
        public void PrepareGdGiaoDich(GdGiaoDichModel model, GdGiaoDich item)
        {
            item.LOAI_ID = model.LOAI_ID;
            item.MA_GIAM_GIA_ID = model.MA_GIAM_GIA_ID;
            item.EVENT_ID = model.EVENT_ID;
            item.THANH_TIEN = model.THANH_TIEN;
            item.NGAY_GIAO_DICH = model.NGAY_GIAO_DICH;
            item.NGAY_BAT_DAU = model.NGAY_BAT_DAU;
            item.NGAY_KET_THUC = model.NGAY_KET_THUC;
            item.HINH_THUC_THANH_TOAN = model.HINH_THUC_THANH_TOAN;
            item.TRANG_THAI = model.TRANG_THAI;
            item.DOANH_NGHIEP_ID = model.DOANH_NGHIEP_ID;
            item.GHI_CHU = model.GHI_CHU;
            item.NGAY_TAO = model.NGAY_TAO;
            item.NGUOI_TAO = model.NGUOI_TAO;

        }
        #endregion
    }
}

