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

namespace Nop.Web.Areas.CRM.Controllers
{
    public partial class DvDichVuController : BaseCRMController
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
        private readonly IDvDichVuModelFactory _itemModelFactory;
        private readonly IDvDichVuService _itemService;
        private readonly IDvGiaDichVuService _dvGiaDichVuService;
        private readonly IDvDichVuComboService _dvDichVuComboService;
        private readonly IDvNhatKyGiaService _dvNhatKyGiaService;
        #endregion

        #region Ctor
        public DvDichVuController(
            IStoreContext storeContext,
            INotificationService notificationService,
            ICustomerActivityService customerActivityService,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IWorkContext workContext,
            LocalizationSettings localizationSettings,
            IDvDichVuModelFactory itemModelFactory,
            IDvDichVuService itemService,
            IDvGiaDichVuService dvGiaDichVuService,
            IDvDichVuComboService dvDichVuComboService,
            IDvNhatKyGiaService dvNhatKyGiaService
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
            this._dvGiaDichVuService = dvGiaDichVuService;
            this._dvDichVuComboService = dvDichVuComboService;
            this._dvNhatKyGiaService = dvNhatKyGiaService;
        }
        #endregion

        #region Methods

        public virtual IActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDichVu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new DvDichVuSearchModel();
            var model = _itemModelFactory.PrepareDvDichVuSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(DvDichVuSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDichVu))
                return AccessDeniedDataTablesJson();
            //prepare model
            searchModel.IsCombo = false;
            var model = _itemModelFactory.PrepareDvDichVuListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult ListCombo()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDichVu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new DvDichVuSearchModel();
            var model = _itemModelFactory.PrepareDvDichVuSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult ListCombo(DvDichVuSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDichVu))
                return AccessDeniedDataTablesJson();
            //prepare model
            searchModel.IsCombo = true;
            var model = _itemModelFactory.PrepareDvDichVuListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDichVu))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareDvDichVuModel(new DvDichVuModel(), null);
            model.IS_COMBO = 0;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(DvDichVuModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDichVu))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<DvDichVu>();
                item.IS_COMBO = 0;
                item.TRANG_THAI = (int)EnumTrangThaiDichVu.HoatDong;
                item.DOANH_NGHIEP_ID = _storeContext.CurrentStore.Id;
                _itemService.InsertDvDichVu(item);
                var giaDichVu = new DvGiaDichVu
                {
                    DICH_VU_ID = item.Id,
                    GIA_BAN_LE = model.GiaBanLe,
                    GIA_BAN_BUON = model.GiaBanBuon
                };
                _dvGiaDichVuService.InsertDvGiaDichVu(giaDichVu);
                _customerActivityService.InsertActivity("AddNewDvDichVu", string.Format("Thêm mới: {0}", item.Id), item);
                _notificationService.SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.Id }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareDvDichVuModel(model, null);
            return View(model);
        }

        public virtual IActionResult CreateCombo()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDichVu))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareDvDichVuModel(new DvDichVuModel(), null, isComBo: false);
            model.IS_COMBO = 1;
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult CreateCombo(DvDichVuModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDichVu))
                return AccessDeniedView();
            if (ModelState.IsValid)
            {
                var item = model.ToEntity<DvDichVu>();
                item.TRANG_THAI = (int)EnumTrangThaiDichVu.HoatDong;
                item.IS_COMBO = 1;
                item.DOANH_NGHIEP_ID = _storeContext.CurrentStore.Id;
                _itemService.InsertDvDichVu(item);
                foreach (var dichVu in model.comBoDichVuModels)
                {
                    if (dichVu.SoLuong > 0)
                    {
                        var dichVuCombo = new DvDichVuCombo
                        {
                            DICH_VU_COMBO = item.Id,
                            DICH_VU_ID = dichVu.DichVuId,
                            SO_LUONG = dichVu.SoLuong
                        };
                        _dvDichVuComboService.InsertDvDichVuCombo(dichVuCombo);
                    }
                }
                var giaDichVu = new DvGiaDichVu
                {
                    DICH_VU_ID = item.Id,
                    GIA_BAN_LE = model.GiaBanLe
                };
                _dvGiaDichVuService.InsertDvGiaDichVu(giaDichVu);
                _customerActivityService.InsertActivity("AddNewDvComboDichVu", string.Format("Thêm mới: {0}", item.Id), item);
                return this.JsonSuccessMessage("Tạo mới dữ liệu thành công !", item);
            }

            return this.JsonErrorMessage("Có lỗi!", ModelState.SerializeErrors());
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDichVu))
                return AccessDeniedView();

            var item = _itemService.GetDvDichVuById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareDvDichVuModel(null, item);
            model.IS_COMBO = 0;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(DvDichVuModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDichVu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDvDichVuById(model.Id);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareDvDichVu(model, item);
                _itemService.UpdateDvDichVu(item);
                var giaBan = _dvGiaDichVuService.GetDvGiaDichVu(item.Id);
                if (giaBan.GIA_BAN_BUON != model.GiaBanBuon || giaBan.GIA_BAN_LE != model.GiaBanLe)
                {
                    // ghi log gia ban
                    var nhatKyGia = new DvNhatKyGia
                    {
                        DICH_VU_ID = item.Id,
                        NGAY_SUA = DateTime.Now,
                        GIA_BAN_BUON = giaBan.GIA_BAN_BUON,
                        GIA_BAN_LE = giaBan.GIA_BAN_LE,
                        GIA_1 = giaBan.GIA_1,
                        GIA_2 = giaBan.GIA_2,
                        GIA_3 = giaBan.GIA_3
                    };
                    _dvNhatKyGiaService.InsertDvNhatKyGia(nhatKyGia);
                    // update gia ban
                    giaBan.GIA_BAN_LE = model.GiaBanLe;
                    giaBan.GIA_BAN_BUON = model.GiaBanBuon;
                    _dvGiaDichVuService.UpdateDvGiaDichVu(giaBan);
                }
                _customerActivityService.InsertActivity("EditDvDichVu", string.Format("Cập nhật: {0}", item.Id), item);
                _notificationService.SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.Id }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareDvDichVuModel(model, item);
            return View(model);
        }

        public virtual IActionResult EditCombo(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDichVu))
                return AccessDeniedView();

            var item = _itemService.GetDvDichVuById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareDvDichVuModel(null, item, isComBo: true);
            model.IS_COMBO = 1;
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult EditCombo(DvDichVuModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDichVu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDvDichVuById(model.Id);
            if (item == null)
                return RedirectToAction("ListCombo");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareDvDichVu(model, item);
                _itemService.UpdateDvDichVu(item);
                var giaBan = _dvGiaDichVuService.GetDvGiaDichVu(item.Id);
                if (giaBan.GIA_BAN_BUON != model.GiaBanBuon || giaBan.GIA_BAN_LE != model.GiaBanLe)
                {
                    // ghi log gia ban
                    var nhatKyGia = new DvNhatKyGia
                    {
                        DICH_VU_ID = item.Id,
                        NGAY_SUA = DateTime.Now,
                        GIA_BAN_BUON = giaBan.GIA_BAN_BUON,
                        GIA_BAN_LE = giaBan.GIA_BAN_LE,
                        GIA_1 = giaBan.GIA_1,
                        GIA_2 = giaBan.GIA_2,
                        GIA_3 = giaBan.GIA_3
                    };
                    _dvNhatKyGiaService.InsertDvNhatKyGia(nhatKyGia);
                    // update gia ban
                    giaBan.GIA_BAN_LE = model.GiaBanLe;
                    giaBan.GIA_BAN_BUON = model.GiaBanBuon;
                    _dvGiaDichVuService.UpdateDvGiaDichVu(giaBan);
                }
                // combo map
                var listCombo = _dvDichVuComboService.GetDichVuCombos(comBoId: item.Id);
                _dvDichVuComboService.DeleteDvDichVuCombos(listCombo);
                foreach (var dichVu in model.comBoDichVuModels)
                {
                    var dichVuCombo = new DvDichVuCombo
                    {
                        DICH_VU_COMBO = item.Id,
                        DICH_VU_ID = dichVu.DichVuId,
                        SO_LUONG = dichVu.SoLuong
                    };
                    _dvDichVuComboService.InsertDvDichVuCombo(dichVuCombo);
                }

                _customerActivityService.InsertActivity("EditDvComboDichVu", string.Format("Cập nhật: {0}", item.Id), item);
                return this.JsonSuccessMessage("Cập nhật dữ liệu thành công !", item);
            }
            //prepare model
            return this.JsonErrorMessage("Có lỗi!", ModelState.SerializeErrors());
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDichVu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDvDichVuById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteDvDichVu(item);
                //activity log  
                _customerActivityService.InsertActivity("DeleteDvDichVu", string.Format("Xóa: {0}", item.Id), item);
                _notificationService.SuccessNotification("Xoá dữ liệu thành công");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc.Message);
                return RedirectToAction("Edit", new { id = item.Id });
            }
        }

        [HttpPost]
        public virtual IActionResult DeleteCombo(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDichVu))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDvDichVuById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                var listDichVuCombo = _dvDichVuComboService.GetDichVuCombos(comBoId: id);
                if (listDichVuCombo != null && listDichVuCombo.Count > 0)
                {
                    _dvDichVuComboService.DeleteDvDichVuCombos(listDichVuCombo);
                }
                _itemService.DeleteDvDichVu(item);
                //activity log  
                _customerActivityService.InsertActivity("DeleteDvDichVu", string.Format("Xóa: {0}", item.Id), item);
                return this.JsonSuccessMessage("Xoá dữ liệu thành công !");
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc.Message);
                return this.JsonErrorMessage("Có lỗi!", ModelState.SerializeErrors());
            }
        }

        public virtual IActionResult _ListDichVuCombo()
        {
            var model = new ComBoDichVuModel
            {
                DDLDichVuCombo = _itemModelFactory.PrepareDDLDichVuCombo(isAddFirst: true, isCombo: false)
            };
            return PartialView(model);
        }
        #endregion
    }
}

