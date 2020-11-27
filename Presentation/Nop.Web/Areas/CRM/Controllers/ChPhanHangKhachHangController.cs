//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 2/10/2020
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
    public partial class ChPhanHangKhachHangController : BaseCRMController
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
        private readonly IChPhanHangKhachHangModelFactory _itemModelFactory;
        private readonly IChPhanHangKhachHangService _itemService;
        #endregion

        #region Ctor
        public ChPhanHangKhachHangController(
            IStoreContext storeContext,
            INotificationService notificationService,
            ICustomerActivityService customerActivityService,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IWorkContext workContext,
            LocalizationSettings localizationSettings,
            IChPhanHangKhachHangModelFactory itemModelFactory,
            IChPhanHangKhachHangService itemService
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
        }
        #endregion

        #region Methods

        public virtual IActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new ChPhanHangKhachHangSearchModel();
            var model = _itemModelFactory.PrepareChPhanHangKhachHangSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(ChPhanHangKhachHangSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedDataTablesJson();
            //prepare model
            var model = _itemModelFactory.PrepareChPhanHangKhachHangListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareChPhanHangKhachHangModel(new ChPhanHangKhachHangModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(ChPhanHangKhachHangModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<ChPhanHangKhachHang>();
                _itemService.InsertChPhanHangKhachHang(item);
                _customerActivityService.InsertActivity("AddNewChPhanHangKhachHang", string.Format("Thêm mới: {0}", item.Id), item);
                _notificationService.SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.Id }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareChPhanHangKhachHangModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();

            var item = _itemService.GetChPhanHangKhachHangById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareChPhanHangKhachHangModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(ChPhanHangKhachHangModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetChPhanHangKhachHangById(model.Id);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareChPhanHangKhachHang(model, item);
                _itemService.UpdateChPhanHangKhachHang(item);
                _customerActivityService.InsertActivity("EditChPhanHangKhachHang", string.Format("Cập nhật: {0}", item.Id), item);
                _notificationService.SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.Id }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareChPhanHangKhachHangModel(model, item);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetChPhanHangKhachHangById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteChPhanHangKhachHang(item);
                //activity log  
                _customerActivityService.InsertActivity("DeleteChPhanHangKhachHang", string.Format("Xóa: {0}", item.Id), item);
                _notificationService.SuccessNotification("Xoá dữ liệu thành công");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc.Message);
                return RedirectToAction("Edit", new { id = item.Id });
            }
        }

        public virtual IActionResult Active(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();

            var itemActive = _itemService.GetChPhanHangKhachHangActive(_storeContext.CurrentStore.Id);
            itemActive.ACTIVE = 0;
            _itemService.UpdateChPhanHangKhachHang(itemActive);

            var item = _itemService.GetChPhanHangKhachHangById(id);
            item.ACTIVE = 1;
            _itemService.UpdateChPhanHangKhachHang(item);

            return this.JsonSuccessMessage("Thành công !");
        }
        #endregion
    }
}

