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
    public partial class DvDichVuComboController : BaseCRMController
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
        private readonly IDvDichVuComboModelFactory _itemModelFactory;
        private readonly IDvDichVuComboService _itemService;
        #endregion

        #region Ctor
        public DvDichVuComboController(
            IStoreContext storeContext,
            INotificationService notificationService,
            ICustomerActivityService customerActivityService,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IWorkContext workContext,
            LocalizationSettings localizationSettings,
            IDvDichVuComboModelFactory itemModelFactory,
            IDvDichVuComboService itemService
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
            var searchmodel = new DvDichVuComboSearchModel();
            var model = _itemModelFactory.PrepareDvDichVuComboSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(DvDichVuComboSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedDataTablesJson();
            //prepare model
            var model = _itemModelFactory.PrepareDvDichVuComboListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareDvDichVuComboModel(new DvDichVuComboModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(DvDichVuComboModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<DvDichVuCombo>();
                _itemService.InsertDvDichVuCombo(item);
                _customerActivityService.InsertActivity("AddNewDvDichVuCombo", string.Format("Thêm mới: {0}", item.Id), item);
                _notificationService.SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.Id }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareDvDichVuComboModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();

            var item = _itemService.GetDvDichVuComboById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareDvDichVuComboModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(DvDichVuComboModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDvDichVuComboById(model.Id);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareDvDichVuCombo(model, item);
                _itemService.UpdateDvDichVuCombo(item);
                _customerActivityService.InsertActivity("EditDvDichVuCombo", string.Format("Cập nhật: {0}", item.Id), item);
                _notificationService.SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.Id }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareDvDichVuComboModel(model, item);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetDvDichVuComboById(id);
            if (item == null)
                return this.JsonErrorMessage("Có lỗi!", ModelState.SerializeErrors());
            try
            {
                _itemService.DeleteDvDichVuCombo(item);
                //activity log  
                _customerActivityService.InsertActivity("DeleteDvDichVuCombo", string.Format("Xóa: {0}", item.Id), item);
                return this.JsonSuccessMessage("Xoá dữ liệu thành công !");
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc.Message);
                return this.JsonErrorMessage("Có lỗi!", ModelState.SerializeErrors());
            }
        }
        #endregion
    }
}

