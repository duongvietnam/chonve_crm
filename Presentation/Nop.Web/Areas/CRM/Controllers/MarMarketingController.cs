//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.CRM;
using Nop.Core.Domain.Localization;
using Nop.Services.CRM;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.CRM.Factories.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Web.Areas.CRM.Controllers
{
    public partial class MarMarketingController : BaseCRMController
    {
        #region Fields
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IEventPublisher _eventPublisher;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IWorkContext _workContext;
        private readonly LocalizationSettings _localizationSettings;
        private readonly INotificationService _notificationService;
        private readonly IStoreContext _storeContext;
        private readonly IMarMarketingModelFactory _itemModelFactory;
        private readonly IMarMarketingService _itemService;
        private readonly IMarMarketingHeThongModelFactory _marMarketingHeThongModelFactory;
        private readonly IDvDichVuModelFactory _dvDichVuModelFactory;
        private readonly IMarMarketingDieuKienService _marMarketingDieuKienService;
        private readonly IMarMarketingHeThongMapService _marMarketingHeThongMapService;
        private readonly IDvDonViTinhModelFactory _dvDonViTinhModelFactory;
        private readonly IChPhanHangKhachHangModelFactory _chPhanHangKhachHangModelFactory;
        private readonly IKhKhachHangModelFactory _khKhachHangModelFactory;
        private readonly IMarMarketingHeThongService _marMarketingHeThongService;
        private readonly IMarEventMarketingService _marEventMarketingService;
        private readonly IMarMaGiamGiaModelFactory _marMaGiamGiaModelFactory;
        private readonly IMarMaGiamGiaService _marMaGiamGiaService;
        private readonly IDvDonViTinhService _dvDonViTinhService;
        private readonly IKhNhomKhachHangModelFactory _khNhomKhachHangModelFactory;
        private readonly IChPhanHangKhachHangService _chPhanHangKhachHangService;
        private readonly IKhKhachHangService _khKhachHangService;
        private readonly IKhNhomKhachHangMapService _khNhomKhachHangMapService;
        #endregion

        #region Ctor
        public MarMarketingController(
            IStoreContext storeContext,
            INotificationService notificationService,
            ICustomerActivityService customerActivityService,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IWorkContext workContext,
            LocalizationSettings localizationSettings,
            IMarMarketingModelFactory itemModelFactory,
            IMarMarketingService itemService,
            IMarMarketingHeThongModelFactory marMarketingHeThongModelFactory,
            IDvDichVuModelFactory dvDichVuModelFactory,
            IMarMarketingDieuKienService marMarketingDieuKienService,
            IMarMarketingHeThongMapService marMarketingHeThongMapService,
            IDvDonViTinhModelFactory dvDonViTinhModelFactory,
            IChPhanHangKhachHangModelFactory chPhanHangKhachHangModelFactory,
            IKhKhachHangModelFactory khKhachHangModelFactory,
            IMarMarketingHeThongService marMarketingHeThongService,
            IMarEventMarketingService marEventMarketingService,
            IMarMaGiamGiaModelFactory marMaGiamGiaModelFactory,
            IMarMaGiamGiaService marMaGiamGiaService,
            IDvDonViTinhService dvDonViTinhService,
            IKhNhomKhachHangModelFactory khNhomKhachHangModelFactory,
            IChPhanHangKhachHangService chPhanHangKhachHangService,
            IKhKhachHangService khKhachHangService,
            IKhNhomKhachHangMapService khNhomKhachHangMapService
            )
        {
            this._storeContext = storeContext;
            this._notificationService = notificationService;
            this._customerActivityService = customerActivityService;
            this._eventPublisher = eventPublisher;
            this._localizationService = localizationService;
            this._permissionService = permissionService;
            this._workContext = workContext;
            this._localizationSettings = localizationSettings;
            this._itemModelFactory = itemModelFactory;
            this._itemService = itemService;
            this._marMarketingHeThongModelFactory = marMarketingHeThongModelFactory;
            this._dvDichVuModelFactory = dvDichVuModelFactory;
            this._marMarketingDieuKienService = marMarketingDieuKienService;
            this._marMarketingHeThongMapService = marMarketingHeThongMapService;
            this._dvDonViTinhModelFactory = dvDonViTinhModelFactory;
            this._chPhanHangKhachHangModelFactory = chPhanHangKhachHangModelFactory;
            this._khKhachHangModelFactory = khKhachHangModelFactory;
            this._marMarketingHeThongService = marMarketingHeThongService;
            this._marEventMarketingService = marEventMarketingService;
            this._marMaGiamGiaModelFactory = marMaGiamGiaModelFactory;
            this._marMaGiamGiaService = marMaGiamGiaService;
            this._dvDonViTinhService = dvDonViTinhService;
            this._khNhomKhachHangModelFactory = khNhomKhachHangModelFactory;
            this._chPhanHangKhachHangService = chPhanHangKhachHangService;
            this._khKhachHangService = khKhachHangService;
            this._khNhomKhachHangMapService = khNhomKhachHangMapService;
        }
        #endregion

        #region Methods

        public virtual IActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLMarketing))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new MarMarketingSearchModel();
            var model = _itemModelFactory.PrepareMarMarketingSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(MarMarketingSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLMarketing))
                return AccessDeniedDataTablesJson();
            //prepare model
            var model = _itemModelFactory.PrepareMarMarketingListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLMarketing))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareMarMarketingModel(new MarMarketingModel(), null);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Create(MarMarketingModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLMarketing))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<MarMarketing>();
                _itemService.InsertMarMarketing(item);
                foreach (var dieuKien in model.ListDieuKien)
                {
                    var DieuKien = new MarMarketingDieuKien
                    {
                        DEN_GIO = dieuKien.DenGio,
                        DEN_NGAY = dieuKien.DenNgay,
                        DICH_VU_ID = dieuKien.DichVuId,
                        DON_VI_TINH = dieuKien.DonViTinh,
                        MARKETING_ID = item.Id,
                        SALE = dieuKien.Sale,
                        SO_LUONG = dieuKien.SoLuong,
                        TU_GIO = dieuKien.TuGio,
                        TU_NGAY = dieuKien.TuNgay
                    };
                    _marMarketingDieuKienService.InsertMarMarketingDieuKien(DieuKien);
                }
                foreach (var heThong in model.MarketingHeThongs)
                {
                    var map = new MarMarketingHeThongMap
                    {
                        MARKETING_ID = item.Id,
                        MAR_HE_THONG_ID = heThong
                    };
                    _marMarketingHeThongMapService.InsertMarMarketingHeThongMap(map);
                }
                _customerActivityService.InsertActivity("AddNewMarMarketing", string.Format("Thêm mới: {0}", item.Id), item);
                _notificationService.SuccessNotification("Tạo mới dữ liệu thành công !");
                return this.JsonSuccessMessage("Tạo mới dữ liệu thành công !", item);
            }

            return this.JsonErrorMessage("Có lỗi!", ModelState.SerializeErrors());
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLMarketing))
                return AccessDeniedView();

            var item = _itemService.GetMarMarketingById(id);
            if (item == null)
                return RedirectToAction("List");
            // prepare HinhThuc
            switch (item.HINH_THUC)
            {
                case (int)EnumHinhThucMarketing.MaGiamGia:
                    return RedirectToAction("EditMaGiamGia", new { id = item.Id });
            }

            //prepare model
            var model = _itemModelFactory.PrepareMarMarketingModel(null, item);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Edit(MarMarketingModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLMarketing))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetMarMarketingById(model.Id);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareMarMarketing(model, item);
                _itemService.UpdateMarMarketing(item);
                foreach (var dieuKien in model.ListDieuKien)
                {
                    if (dieuKien.DieuKienId > 0)
                    {
                        var DieuKien = _marMarketingDieuKienService.GetMarMarketingDieuKienById(dieuKien.DieuKienId);
                        DieuKien.DEN_GIO = dieuKien.DenGio;
                        DieuKien.DEN_NGAY = dieuKien.DenNgay;
                        DieuKien.DICH_VU_ID = dieuKien.DichVuId;
                        DieuKien.DON_VI_TINH = dieuKien.DonViTinh;
                        DieuKien.SALE = dieuKien.Sale;
                        DieuKien.SO_LUONG = dieuKien.SoLuong;
                        DieuKien.TU_GIO = dieuKien.TuGio;
                        DieuKien.TU_NGAY = dieuKien.TuNgay;
                        _marMarketingDieuKienService.UpdateMarMarketingDieuKien(DieuKien);
                    }
                    else
                    {
                        var DieuKien = new MarMarketingDieuKien
                        {
                            DEN_GIO = dieuKien.DenGio,
                            DEN_NGAY = dieuKien.DenNgay,
                            DICH_VU_ID = dieuKien.DichVuId,
                            DON_VI_TINH = dieuKien.DonViTinh,
                            MARKETING_ID = item.Id,
                            SALE = dieuKien.Sale,
                            SO_LUONG = dieuKien.SoLuong,
                            TU_GIO = dieuKien.TuGio,
                            TU_NGAY = dieuKien.TuNgay
                        };
                        _marMarketingDieuKienService.InsertMarMarketingDieuKien(DieuKien);
                    }
                    _marMarketingHeThongMapService.DeleteMarMarketingHeThongMapByMarId(item.Id);
                    foreach (var heThong in model.MarketingHeThongs)
                    {
                        var map = new MarMarketingHeThongMap
                        {
                            MARKETING_ID = item.Id,
                            MAR_HE_THONG_ID = heThong
                        };
                        _marMarketingHeThongMapService.InsertMarMarketingHeThongMap(map);
                    }
                }
                _customerActivityService.InsertActivity("EditMarMarketing", string.Format("Cập nhật: {0}", item.Id), item);
                _notificationService.SuccessNotification("Cập nhật dữ liệu thành công !");
                return this.JsonSuccessMessage("Cập nhật dữ liệu thành công !", item);
            }

            return this.JsonErrorMessage("Có lỗi!", ModelState.SerializeErrors());
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLMarketing))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetMarMarketingById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                var listDieuKien = _marMarketingDieuKienService.GetMarMarketingDieuKiens(MarId: id);
                _marMarketingDieuKienService.DeleteMarMarketingDieuKiens(listDieuKien);
                _marMarketingHeThongMapService.DeleteMarMarketingHeThongMapByMarId(MarId: id);
                _marEventMarketingService.DeleteMarEventMarketingByMarId(id);
                _marMaGiamGiaService.DeleteMarMaGiamGiaByMarId(id);
                _itemService.DeleteMarMarketing(item);
                //activity log
                _customerActivityService.InsertActivity("DeleteMarMarketing", string.Format("Xóa: {0}", item.Id), item);
                _notificationService.SuccessNotification("Xoá dữ liệu thành công");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc.Message);
                return RedirectToAction("Edit", new { id = item.Id });
            }
        }

        public virtual IActionResult _DieuKienApDung()
        {
            DieuKienMarketing model = new DieuKienMarketing
            {
                DDLDichVu = _dvDichVuModelFactory.PrepareDDLDichVuCombo(isAddFirst: true, isCombo: false),
                DDLDonViTinh = _dvDonViTinhModelFactory.PrepareSelectListDonViTinh(isAddFirst: true)
            };

            return PartialView(model);
        }

        public virtual IActionResult CreateMarGiamGia()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLMarketing))
                return AccessDeniedView();
            var model = _itemModelFactory.PrepareMarMarketingModel(new MarMarketingModel(), null);

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult CreateMarGiamGia(MarMarketingModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLMarketing))
                return AccessDeniedView();
            if (ModelState.IsValid)
            {
                var mar = new MarMarketing
                {
                    TEN = model.TEN,
                    HINH_THUC = (int)EnumHinhThucMarketing.GiamGiaKhachHangPhanHang,
                    CO_THE_KET_HOP = model.CO_THE_KET_HOP
                };
                _itemService.InsertMarMarketing(mar);

                var dieuKien = new MarMarketingDieuKien
                {
                    SALE = model.Sale,
                    HANG_KHACH_HANG = model.HangKhachHangId,
                    MARKETING_ID = mar.Id,
                    DON_GIA = model.DonGia,
                    TU_NGAY = model.TuNgay,
                    DEN_NGAY = model.DenNgay
                };
                if (model.TuNgay != null)
                {
                    dieuKien.TU_GIO = ((DateTime)model.TuNgay).TimeOfDay;
                }
                if (model.DenNgay != null)
                {
                    dieuKien.DEN_GIO = ((DateTime)model.DenNgay).TimeOfDay;
                }
                _marMarketingDieuKienService.InsertMarMarketingDieuKien(dieuKien);

                var map = new MarMarketingHeThongMap
                {
                    MARKETING_ID = mar.Id,
                    MAR_HE_THONG_ID = _marMarketingHeThongService.GetMarMarketingHeThongByMa("SALE_HANG_KHACH_HANG").Id
                };
                _marMarketingHeThongMapService.InsertMarMarketingHeThongMap(map);

                var eventMar = new MarEventMarketing
                {
                    THOI_GIAN_BAT_DAU = model.TuNgay,
                    TEN = model.TEN,
                    MARKETING_ID = mar.Id,
                    THOI_GIAN_KET_THUC = model.DenNgay
                };
                _marEventMarketingService.InsertMarEventMarketing(eventMar);
                _notificationService.SuccessNotification("Tạo mới dữ liệu thành công !");
                _customerActivityService.InsertActivity("AddNewMarMarketing", string.Format("Thêm mới: {0}", mar.Id), mar);

                return RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareMarMarketingModel(model, null);
            return View(model);
        }

        public virtual IActionResult EditMarGiamGia(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLMarketing))
                return AccessDeniedView();

            var item = _itemService.GetMarMarketingById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareMarMarketingModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult EditMarGiamGia(MarMarketingModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLMarketing))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetMarMarketingById(model.Id);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                var listDieuKien = _marMarketingDieuKienService.GetMarMarketingDieuKiens(model.Id);
                foreach (var dieuKien in listDieuKien)
                {
                    dieuKien.SALE = model.Sale;
                    dieuKien.HANG_KHACH_HANG = model.HangKhachHangId;
                    dieuKien.DON_GIA = model.DonGia;
                    _marMarketingDieuKienService.UpdateMarMarketingDieuKien(dieuKien);
                }

                _itemModelFactory.PrepareMarMarketing(model, item);
                _itemService.UpdateMarMarketing(item);
                _customerActivityService.InsertActivity("EditMarGiamGia", string.Format("Cập nhật: {0}", item.Id), item);
                _notificationService.SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.Id }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareMarMarketingModel(model, item);
            return View(model);
        }

        public virtual IActionResult ChonHinhThucMarking()
        {
            return View();
        }

        public virtual IActionResult CreateMaGiamGia()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLMarketing))
                return AccessDeniedView();
            var model = _marMaGiamGiaModelFactory.PrepareMarMaGiamGiaModel(new MarMaGiamGiaModel(), null);

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult CreateMaGiamGia(MarMaGiamGiaModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLMarketing))
                return AccessDeniedView();
            if (ModelState.IsValid)
            {
                var mar = new MarMarketing
                {
                    HINH_THUC = (int)EnumHinhThucMarketing.MaGiamGia,
                    CO_THE_KET_HOP = model.CO_THE_KET_HOP,
                    TEN = "Phiếu giảm giá"
                };
                _itemService.InsertMarMarketing(mar);

                var dieuKien = new MarMarketingDieuKien
                {
                    DON_GIA = model.DonGia,
                    TU_NGAY = model.TuNgay,
                    DEN_NGAY = model.DenNgay,
                    MARKETING_ID = mar.Id,
                    SALE = model.SaleTheoPhanTram > 0 ? model.SaleTheoPhanTram : model.SaleTheoSoTien,
                    DON_VI_TINH = model.SaleTheoPhanTram > 0 ? _dvDonViTinhService.GetByMa("PhanTram").Id : _dvDonViTinhService.GetByMa("SoTienVND").Id
                };
                if (model.TuNgay != null)
                {
                    dieuKien.TU_GIO = ((DateTime)model.TuNgay).TimeOfDay;
                }
                if (model.DenNgay != null)
                {
                    dieuKien.DEN_GIO = ((DateTime)model.DenNgay).TimeOfDay;
                }
                _marMarketingDieuKienService.InsertMarMarketingDieuKien(dieuKien);

                for (int i = 0; i < model.SoPhieuTao; i++)
                {
                    var maGiamGia = new MarMaGiamGia
                    {
                        MA = _marMaGiamGiaModelFactory.PrepareMaGiamGia(),
                        MAR_DIEU_KIEN_ID = dieuKien.Id,
                        MARKETING_ID = mar.Id
                    };
                    _marMaGiamGiaService.InsertMarMaGiamGia(maGiamGia);
                }

                var eventMar = new MarEventMarketing
                {
                    MARKETING_ID = mar.Id,
                    TEN = mar.TEN,
                    THOI_GIAN_BAT_DAU = model.TuNgay,
                    THOI_GIAN_KET_THUC = model.DenNgay
                };
                _marEventMarketingService.InsertMarEventMarketing(eventMar);

                _customerActivityService.InsertActivity("AddNewMarMarketing", string.Format("Thêm mới: {0}", mar.Id), mar);
                _notificationService.SuccessNotification("Tạo mới dữ liệu thành công !");
                return RedirectToAction("List");
            }

            //prepare model
            model = _marMaGiamGiaModelFactory.PrepareMarMaGiamGiaModel(model, null);
            return View(model);
        }

        public virtual IActionResult EditMaGiamGia(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLMarketing))
                return AccessDeniedView();

            var item = _itemService.GetMarMarketingById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _marMaGiamGiaModelFactory.PrepareMaGiamGiaFromMarketing(item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult EditMaGiamGia(MarMaGiamGiaModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLMarketing))
                return AccessDeniedView();

            var item = _itemService.GetMarMarketingById(model.Id);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                var dieuKien = _marMarketingDieuKienService.GetMarMarketingDieuKiens(item.Id).FirstOrDefault();
                dieuKien.DON_GIA = model.DonGia;
                dieuKien.TU_NGAY = model.TuNgay;
                dieuKien.DEN_NGAY = model.DenNgay;
                if (model.TuNgay != null)
                {
                    dieuKien.TU_GIO = ((DateTime)model.TuNgay).TimeOfDay;
                }
                if (model.DenNgay != null)
                {
                    dieuKien.DEN_GIO = ((DateTime)model.DenNgay).TimeOfDay;
                }
                dieuKien.SALE = model.SaleTheoPhanTram > 0 ? model.SaleTheoPhanTram : model.SaleTheoSoTien;
                dieuKien.DON_VI_TINH = model.SaleTheoPhanTram > 0 ? _dvDonViTinhService.GetByMa("PhanTram").Id : _dvDonViTinhService.GetByMa("SoTienVND").Id;
                _marMarketingDieuKienService.UpdateMarMarketingDieuKien(dieuKien);

                item.CO_THE_KET_HOP = model.CO_THE_KET_HOP;
                _itemService.UpdateMarMarketing(item);

                var eventMar = _marEventMarketingService.GetEventMarketing(item.Id);
                eventMar.THOI_GIAN_BAT_DAU = model.TuNgay;
                eventMar.THOI_GIAN_KET_THUC = model.DenNgay;
                _marEventMarketingService.UpdateMarEventMarketing(eventMar);

                _customerActivityService.InsertActivity("EditMarMaGiamGia", string.Format("Cập nhật: {0}", item.Id), item);
                _notificationService.SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("EditMaGiamGia", new { id = item.Id }) : RedirectToAction("List");
            }
            //prepare model
            model = _marMaGiamGiaModelFactory.PrepareMaGiamGiaFromMarketing(item);
            return View(model);
        }

        public virtual IActionResult DetailMaGiamGia(int marId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLMarketing))
                return AccessDeniedView();
            var mar = _itemService.GetMarMarketingById(marId);
            var dieuKien = _marMarketingDieuKienService.GetMarMarketingDieuKiens(marId).FirstOrDefault();
            var model = new MarMaGiamGiaModel
            {
                SoPhieuTao = _marMaGiamGiaService.GetSoMaGiamGiaByMarId(marId),
                SoPhieuDaGui = _marMaGiamGiaService.GetSoMaGiamGiaDaGuiByMarId(marId),
                SoPhieuKhachHangDaSuDung = _marMaGiamGiaService.GetSoMaGiamGiaDaSuDungByMarId(marId),
                CO_THE_KET_HOP = mar.CO_THE_KET_HOP,
                TuNgayString = dieuKien.TU_NGAY.toDateVNString(),
                DenNgayString = dieuKien.DEN_NGAY.toDateVNString(),
                MarketingId = marId
            };

            return View(model);
        }

        public virtual IActionResult _TangTheoKhachHang(int marId)
        {
            return PartialView();
        }

        public virtual IActionResult _TangTheoHangKhachHang(int marId)
        {
            var model = new MarMaGiamGiaModel();
            model.DDLHangKhachHang = _khKhachHangModelFactory.PrepareMultiSelectHangKhachHang();
            model.Random = true;
            model.MarketingId = marId;

            return PartialView(model);
        }

        public virtual IActionResult _TangTheoNhomKhachHang(int marId)
        {
            var model = new MarMaGiamGiaModel();
            model.DDLNhomKhachHang = _khNhomKhachHangModelFactory.PrepareMultiSelectListNhomKhachHang(valSelected: model.NhomKhachHangSelectedId, storeId: _storeContext.CurrentStore.Id);
            model.Random = true;
            model.MarketingId = marId;

            return PartialView(model);
        }

        [HttpPost]
        public virtual IActionResult _TangTheoHangKhachHang(MarMaGiamGiaModel model)
        {
            var cauHinh = _chPhanHangKhachHangService.GetChPhanHangKhachHangActive(_storeContext.CurrentStore.Id);
            var listKhachHang = _khKhachHangService.GetQueryableKhachHang(_storeContext.CurrentStore.Id);
            int soPhieu = _marMaGiamGiaService.GetSoMaGiamGiaDaGuiByMarId(model.MarketingId, true);
            var listKhachHangDaGui = _marMaGiamGiaService.GetListKhachHang(marId: model.MarketingId);
            listKhachHang = listKhachHang.Where(c => !listKhachHangDaGui.Contains(c.Id));
            foreach (var hangKhachHang in model.ListHangKhachHangSelectedId)
            {
                if (hangKhachHang == 1)
                {
                    listKhachHang = listKhachHang.Where(c => c.DIEM_PHAN_HANG >= (int)cauHinh.GIA_TRI_CAP_1 && c.DIEM_PHAN_HANG <= (int)cauHinh.GIA_TRI_CAP_2);
                }
                if (hangKhachHang == 2)
                {
                    listKhachHang = listKhachHang.Where(c => c.DIEM_PHAN_HANG >= (int)cauHinh.GIA_TRI_CAP_2 && c.DIEM_PHAN_HANG <= (int)cauHinh.GIA_TRI_CAP_3);
                }
                if (hangKhachHang == 3)
                {
                    listKhachHang = listKhachHang.Where(c => c.DIEM_PHAN_HANG >= (int)cauHinh.GIA_TRI_CAP_3);
                }
            }

            if (!model.Random && listKhachHang.Count() > soPhieu)
            {
                return this.JsonErrorMessage("Số phiếu (" + soPhieu + ") không đủ cho số khách hàng (" + listKhachHang.Count() + ")", ModelState.SerializeErrors());
            }

            var listKhachHangId = listKhachHang.Select(c => c.Id).ToList();
            var listPhieuGiamGia = _marMaGiamGiaService.GetMarMaGiamGias(model.MarketingId, true);
            int sttPhieu = 0;
            foreach (var khachHangId in listKhachHangId)
            {
                if (!listKhachHangDaGui.Contains(khachHangId))
                {
                    var phieuGiamGia = listPhieuGiamGia[sttPhieu];
                    phieuGiamGia.KHACH_HANG_ID = khachHangId;
                    _marMaGiamGiaService.UpdateMarMaGiamGia(phieuGiamGia);
                }
            }

            return this.JsonSuccessMessage("Thành công !");
        }

        [HttpPost]
        public virtual IActionResult _TangTheoNhomKhachHang(MarMaGiamGiaModel model)
        {
            var listKhachHangId = _khNhomKhachHangMapService.GetKhachHangIdsByNhomId(model.NhomKhachHangSelectedId);
            int soPhieu = _marMaGiamGiaService.GetSoMaGiamGiaDaGuiByMarId(model.marMaGiamGiaSearchModel.MarketingId, true);

            if (!model.Random && listKhachHangId.Count() > soPhieu)
            {
                return this.JsonErrorMessage("Số phiếu (" + soPhieu + ") không đủ cho số khách hàng (" + listKhachHangId.Count() + ")", ModelState.SerializeErrors());
            }

            var listPhieuGiamGia = _marMaGiamGiaService.GetMarMaGiamGias(model.marMaGiamGiaSearchModel.MarketingId, true);
            int sttPhieu = 0;
            foreach (var khachHangId in listKhachHangId)
            {
                var phieuGiamGia = listPhieuGiamGia[sttPhieu];
                phieuGiamGia.KHACH_HANG_ID = khachHangId;
                _marMaGiamGiaService.UpdateMarMaGiamGia(phieuGiamGia);
            }

            return this.JsonSuccessMessage("Thành công !");
        }
        #endregion
    }
}

