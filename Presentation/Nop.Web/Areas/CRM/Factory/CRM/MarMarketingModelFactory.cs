//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.CRM;
using Nop.Services;
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
    public class MarMarketingModelFactory : IMarMarketingModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IMarMarketingService _itemService;
        private readonly IMarMarketingHeThongModelFactory _marMarketingHeThongModelFactory;
        private readonly IMarMarketingDieuKienService _marMarketingDieuKienService;
        private readonly IMarMarketingHeThongMapService _marMarketingHeThongMapService;
        private readonly IDvDichVuModelFactory _dvDichVuModelFactory;
        private readonly IDvDonViTinhModelFactory _dvDonViTinhModelFactory;
        private readonly IKhKhachHangModelFactory _khKhachHangModelFactory;
        private readonly IDvDonViTinhService _dvDonViTinhService;
        private readonly IMarMaGiamGiaService _marMaGiamGiaService;
        #endregion

        #region Ctor

        public MarMarketingModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IMarMarketingService itemService,
            IMarMarketingHeThongModelFactory marMarketingHeThongModelFactory,
            IMarMarketingDieuKienService marMarketingDieuKienService,
            IMarMarketingHeThongMapService marMarketingHeThongMapService,
            IDvDichVuModelFactory dvDichVuModelFactory,
            IDvDonViTinhModelFactory dvDonViTinhModelFactory,
            IKhKhachHangModelFactory khKhachHangModelFactory,
            IDvDonViTinhService dvDonViTinhService,
            IMarMaGiamGiaService marMaGiamGiaService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._marMarketingHeThongModelFactory = marMarketingHeThongModelFactory;
            this._marMarketingDieuKienService = marMarketingDieuKienService;
            this._marMarketingHeThongMapService = marMarketingHeThongMapService;
            this._dvDichVuModelFactory = dvDichVuModelFactory;
            this._dvDonViTinhModelFactory = dvDonViTinhModelFactory;
            this._khKhachHangModelFactory = khKhachHangModelFactory;
            this._dvDonViTinhService = dvDonViTinhService;
            this._marMaGiamGiaService = marMaGiamGiaService;
        }
        #endregion

        #region MarMarketing
        public MarMarketingSearchModel PrepareMarMarketingSearchModel(MarMarketingSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public MarMarketingListModel PrepareMarMarketingListModel(MarMarketingSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchMarMarketings(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new MarMarketingListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<MarMarketingModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<MarMarketingModel>();
                    var dieuKien = _marMarketingDieuKienService.GetMarMarketingDieuKiens(MarId: item.Id);
                    switch (item.HINH_THUC)
                    {
                        case (int)EnumHinhThucMarketing.MaGiamGia:
                            itemModel.TEN = itemModel.TEN + " (" + _marMaGiamGiaService.GetSoMaGiamGiaByMarId(item.Id) + " phiếu)";
                            itemModel.SaleString = dieuKien.FirstOrDefault().SALE.ToVNStringNumber() + _dvDonViTinhService.GetDvDonViTinhById((int)dieuKien.FirstOrDefault().DON_VI_TINH).TEN;
                            itemModel.TuNgayString = dieuKien.FirstOrDefault().TU_NGAY.toDateVNString();
                            itemModel.DenNgayString = dieuKien.FirstOrDefault().DEN_NGAY.toDateVNString();
                            break;
                        case (int)EnumHinhThucMarketing.GiamGiaKhachHangPhanHang:
                            itemModel.SaleString = dieuKien.FirstOrDefault().SALE.ToVNStringPrice();
                            itemModel.TuNgayString = dieuKien.FirstOrDefault().TU_NGAY.toDateVNString();
                            itemModel.DenNgayString = dieuKien.FirstOrDefault().DEN_NGAY.toDateVNString();
                            break;
                    }

                    ls.Add(itemModel);
                }
                return ls;
            });

            return model;
        }
        public MarMarketingModel PrepareMarMarketingModel(MarMarketingModel model, MarMarketing item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<MarMarketingModel>();
                model.MarketingHeThongs = _marMarketingHeThongMapService.GetMarketingHeThongMaps(MarId: item.Id).Select(c => (int)c.MAR_HE_THONG_ID).ToList();
                var listDieuKien = _marMarketingDieuKienService.GetMarMarketingDieuKiens(MarId: item.Id);
                foreach (var dieuKien in listDieuKien)
                {
                    model.ListDieuKien.Add(new DieuKienMarketing
                    {
                        DenGio = dieuKien.DEN_GIO,
                        DenNgay = dieuKien.DEN_NGAY,
                        DichVuId = dieuKien.DICH_VU_ID,
                        DieuKienId = dieuKien.Id,
                        DonGia = dieuKien.DON_GIA,
                        DonViTinh = dieuKien.DON_VI_TINH,
                        Sale = dieuKien.SALE,
                        SoLuong = dieuKien.SO_LUONG,
                        TuGio = dieuKien.TU_GIO,
                        TuNgay = dieuKien.TU_NGAY,
                        DDLDichVu = _dvDichVuModelFactory.PrepareDDLDichVuCombo(isAddFirst: true, isCombo: false),
                        DDLDonViTinh = _dvDonViTinhModelFactory.PrepareSelectListDonViTinh(isAddFirst: true)
                    });
                }
            }
            //more
            model.DDLMarketingHeThong = _marMarketingHeThongModelFactory.PrepareMultiSelectMarHethong(valSelected: model.MarketingHeThongs);
            model.DDLHangKhachHang = _khKhachHangModelFactory.PrepareDDLHangKhachHang();
            model.DDLDuocKetHop = ((EnumCoKhong)model.CO_THE_KET_HOP.GetValueOrDefault(0)).ToSelectList();

            return model;
        }
        public void PrepareMarMarketing(MarMarketingModel model, MarMarketing item)
        {
            item.TEN = model.TEN;
        }
        public IList<SelectListItem> PrepareSelectListMarketing(int valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn Marketing --", string valueFirstRow = "")
        {
            var _listItems = _itemService.GetAllMarMarketings();
            var selectList = _listItems.Select(c => new SelectListItem
            {
                Text = c.TEN,
                Value = c.Id.ToString(),
                Selected = valSelected == c.Id
            }).ToList();
            if (isAddFirst)
                selectList.Add(new SelectListItem
                {
                    Text = strFirstRow,
                    Value = valueFirstRow,
                });

            return selectList.OrderBy(c => c.Value).ToList();
        }
        #endregion
    }
}

