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
using System;

namespace Nop.Web.Areas.CRM.Controllers
{
    public partial class KhNhomKhachHangController : BaseCRMController
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
        private readonly IKhNhomKhachHangModelFactory _itemModelFactory;
        private readonly IKhNhomKhachHangService _itemService;
        private readonly IKhNhomKhachHangMapService _khNhomKhachHangMapService;
        private readonly IKhNhomHeThongMapService _khNhomHeThongMapService;
        #endregion

        #region Ctor
        public KhNhomKhachHangController(
            IStoreContext storeContext,
            INotificationService notificationService,
            ICustomerActivityService customerActivityService,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IWorkContext workContext,
            LocalizationSettings localizationSettings,
            IKhNhomKhachHangModelFactory itemModelFactory,
            IKhNhomKhachHangService itemService,
            IKhNhomKhachHangMapService khNhomKhachHangMapService,
            IKhNhomHeThongMapService khNhomHeThongMapService
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
            this._khNhomKhachHangMapService = khNhomKhachHangMapService;
            this._khNhomHeThongMapService = khNhomHeThongMapService;
        }
        #endregion

        #region Methods

        public virtual IActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLNhomKhachHang))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new KhNhomKhachHangSearchModel();
            var model = _itemModelFactory.PrepareKhNhomKhachHangSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(KhNhomKhachHangSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLNhomKhachHang))
                return AccessDeniedDataTablesJson();
            //prepare model
            var model = _itemModelFactory.PrepareKhNhomKhachHangListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLNhomKhachHang))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareKhNhomKhachHangModel(new KhNhomKhachHangModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(KhNhomKhachHangModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLNhomKhachHang))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<KhNhomKhachHang>();
                item.DOANH_NGHIEP_ID = _storeContext.CurrentStore.Id;
                _itemService.InsertKhNhomKhachHang(item);
                foreach (var nhomHeThong in model.NhomHeThongIds)
                {
                    var map = new KhNhomHeThongMap()
                    {
                        NHOM_KHACH_HANG_HE_THONG = nhomHeThong,
                        NHOM_KHACH_HANG_ID = item.Id
                    };
                    _khNhomHeThongMapService.InsertKhNhomHeThongMap(map);
                }
                _customerActivityService.InsertActivity("AddNewKhNhomKhachHang", string.Format("Thêm mới: {0}", item.Id), item);
                _notificationService.SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.Id }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareKhNhomKhachHangModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLNhomKhachHang))
                return AccessDeniedView();

            var item = _itemService.GetKhNhomKhachHangById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareKhNhomKhachHangModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(KhNhomKhachHangModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLNhomKhachHang))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKhNhomKhachHangById(model.Id);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareKhNhomKhachHang(model, item);
                _itemService.UpdateKhNhomKhachHang(item);
                // map
                var listMap = _khNhomHeThongMapService.GetAllKhNhomHeThongMaps(item.Id);
                _khNhomHeThongMapService.DeleteKhNhomHeThongMaps(listMap);
                foreach (var nhomHeThong in model.NhomHeThongIds)
                {
                    var map = new KhNhomHeThongMap()
                    {
                        NHOM_KHACH_HANG_HE_THONG = nhomHeThong,
                        NHOM_KHACH_HANG_ID = item.Id
                    };
                    _khNhomHeThongMapService.InsertKhNhomHeThongMap(map);
                }
                _customerActivityService.InsertActivity("EditKhNhomKhachHang", string.Format("Cập nhật: {0}", item.Id), item);
                _notificationService.SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.Id }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareKhNhomKhachHangModel(model, item);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLNhomKhachHang))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKhNhomKhachHangById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                // map
                var listMap = _khNhomHeThongMapService.GetAllKhNhomHeThongMaps(item.Id);
                _khNhomHeThongMapService.DeleteKhNhomHeThongMaps(listMap);
                // delete nhom
                _itemService.DeleteKhNhomKhachHang(item);
                //activity log  
                _customerActivityService.InsertActivity("DeleteKhNhomKhachHang", string.Format("Xóa: {0}", item.Id), item);
                _notificationService.SuccessNotification("Xoá dữ liệu thành công");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc.Message);
                return RedirectToAction("Edit", new { id = item.Id });
            }
        }
        #endregion
    }
}

