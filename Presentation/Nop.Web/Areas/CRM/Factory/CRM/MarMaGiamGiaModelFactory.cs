//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
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
    public class MarMaGiamGiaModelFactory : IMarMaGiamGiaModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IMarMaGiamGiaService _itemService;
        private readonly IDvDonViTinhModelFactory _dvDonViTinhModelFactory;
        private readonly IMarMarketingDieuKienService _marMarketingDieuKienService;
        private readonly IDvDonViTinhService _dvDonViTinhService;
        private readonly IKhKhachHangService _khKhachHangService;

        #endregion

        #region Ctor

        public MarMaGiamGiaModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IMarMaGiamGiaService itemService,
            IDvDonViTinhModelFactory dvDonViTinhModelFactory,
            IMarMarketingDieuKienService marMarketingDieuKienService,
            IDvDonViTinhService dvDonViTinhService,
            IKhKhachHangService khKhachHangService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._dvDonViTinhModelFactory = dvDonViTinhModelFactory;
            this._marMarketingDieuKienService = marMarketingDieuKienService;
            this._dvDonViTinhService = dvDonViTinhService;
            this._khKhachHangService = khKhachHangService;
        }
        #endregion

        #region MarMaGiamGia
        public MarMaGiamGiaSearchModel PrepareMarMaGiamGiaSearchModel(MarMaGiamGiaSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }
        public MarMaGiamGiaListModel PrepareMarMaGiamGiaListModel(MarMaGiamGiaSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchMarMaGiamGias(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, marId: searchModel.MarketingId, trangThai: searchModel.TrangThaiPhieu);

            //prepare list model
            var model = new MarMaGiamGiaListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<MarMaGiamGiaModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<MarMaGiamGiaModel>();
                    var dieuKien = _marMarketingDieuKienService.GetMarMarketingDieuKienById((int)item.MAR_DIEU_KIEN_ID);
                    itemModel.DenNgay = dieuKien.DEN_NGAY;
                    itemModel.TuNgay = dieuKien.TU_NGAY;
                    itemModel.SaleText = dieuKien.SALE > 0 ? dieuKien.SALE.ToVNStringNumber() : "0";
                    itemModel.DonViTinhText = _dvDonViTinhService.GetDvDonViTinhById((int)dieuKien.DON_VI_TINH).TEN;
                    itemModel.TenKhachHang = item.KHACH_HANG_ID > 0 ? _khKhachHangService.GetKhKhachHangById((int)item.KHACH_HANG_ID).TEN : "";
                    itemModel.TrangThaiPhieu = "Chưa gửi khách hàng";
                    if (item.KHACH_HANG_ID > 0)
                    {
                        itemModel.TrangThaiPhieu = "Đã gửi khách hàng";
                    }
                    if (item.NGAY_SU_DUNG != null)
                    {
                        itemModel.TrangThaiPhieu = "Đã sử dụng";
                    }
                    ls.Add(itemModel);
                }
                return ls;

            });

            return model;
        }
        public MarMaGiamGiaModel PrepareMarMaGiamGiaModel(MarMaGiamGiaModel model, MarMaGiamGia item)
        {
            if (item != null)
            {
                var dieuKien = _marMarketingDieuKienService.GetMarMarketingDieuKienById((int)item.MAR_DIEU_KIEN_ID);
                //fill in model values from the entity
                model = item.ToModel<MarMaGiamGiaModel>();
                model.TuNgay = dieuKien.TU_NGAY;
                model.DenNgay = dieuKien.DEN_NGAY;
                model.DonGia = dieuKien.DON_GIA;
            }
            //more
            model.DDLDuocKetHop = ((EnumCoKhong)model.CO_THE_KET_HOP.GetValueOrDefault(0)).ToSelectList();

            return model;
        }
        public MarMaGiamGiaModel PrepareMaGiamGiaFromMarketing(MarMarketing item)
        {
            var dieuKien = _marMarketingDieuKienService.GetMarMarketingDieuKiens(item.Id).FirstOrDefault();
            var model = new MarMaGiamGiaModel
            {
                TuNgay = dieuKien.TU_NGAY,
                DenNgay = dieuKien.DEN_NGAY,
                DonGia = dieuKien.DON_GIA,
                CO_THE_KET_HOP = item.CO_THE_KET_HOP,
                SoPhieuTao = _itemService.GetSoMaGiamGiaDaGuiByMarId(item.Id, false),
                Id = item.Id
            };
            if (_dvDonViTinhService.GetDvDonViTinhById((int)dieuKien.DON_VI_TINH).MA == "PhanTram")
            {
                model.SaleTheoPhanTram = (int)dieuKien.SALE;
            }
            else
            {
                model.SaleTheoSoTien = (int)dieuKien.SALE;
            }
            model.DDLDuocKetHop = ((EnumCoKhong)model.CO_THE_KET_HOP.GetValueOrDefault(0)).ToSelectList();

            return model;
        }
        public void PrepareMarMaGiamGia(MarMaGiamGiaModel model, MarMaGiamGia item)
        {
            item.MA = model.MA;
            item.NGAY_SU_DUNG = model.NGAY_SU_DUNG;
            item.KHACH_HANG_ID = model.KHACH_HANG_ID;
        }
        public string PrepareMaGiamGia()
        {
            string ma = GenRandomNumber().ToString();
            var checkExits = _itemService.GetCountByMa(ma, _storeContext.CurrentStore.Id);
            if (checkExits > 0)
            {
                PrepareMaGiamGia();
            }

            return ma;
        }
        public int GenRandomNumber()
        {
            Random random = new Random();
            int num = random.Next(100000, 999999);
            return num;
        }
        #endregion
    }
}
