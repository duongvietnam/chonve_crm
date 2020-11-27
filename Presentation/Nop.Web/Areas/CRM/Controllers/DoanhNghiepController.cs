using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Stores;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.CRM.Factory.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Linq;

namespace Nop.Web.Areas.CRM.Controllers
{
    public class DoanhNghiepController : BaseCRMController
    {
        #region Fields
        private readonly IPermissionService _permissionService;
        private readonly IDoanhNghiepModelFactory _storeModelFactory;
        private readonly IStoreService _storeService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly INotificationService _notificationService;
        private readonly ISettingService _settingService;
        private readonly ICustomerService _customerService;
        #endregion
        #region Ctor
        public DoanhNghiepController(IPermissionService permissionService,
            IDoanhNghiepModelFactory storeModelFactory, 
            IStoreService storeService, 
            ICustomerActivityService customerActivityService, 
            INotificationService notificationService, 
            ISettingService settingService,
            ICustomerService customerService)
        {
            _permissionService = permissionService;
            _storeModelFactory = storeModelFactory;
            _storeService = storeService;
            _customerActivityService = customerActivityService;
            _notificationService = notificationService;
            _settingService = settingService;
            _customerService = customerService;
        }
        #endregion
        public virtual IActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDoanhNghiep))
                return AccessDeniedView();

            //prepare model
            var model = _storeModelFactory.PrepareStoreSearchModel(new StoreSearchModel());

            return View(model);
        }
        [HttpPost]
        public virtual IActionResult List(StoreSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDoanhNghiep))
                return AccessDeniedDataTablesJson();

            //prepare model
            var model = _storeModelFactory.PrepareStoreListModel(searchModel);

            return Json(model);
        }
        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();
            //prepare model
            var model = _storeModelFactory.PrepareStoreModel(new StoreModel(), null);
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(StoreModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDoanhNghiep))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var store = model.ToEntity<Store>();

                //ensure we have "/" at the end
                if (!String.IsNullOrEmpty(store.Url)&&!store.Url.EndsWith("/"))
                    store.Url += "/";

                _storeService.InsertStore(store);

                //activity log
                _customerActivityService.InsertActivity("AddNewStore",
                    string.Format("Thêm mới doanh nghiệp : {0}", store.Id), store);

                //locales
                //UpdateAttributeLocales(store, model);

                _notificationService.SuccessNotification("Tạo mới dữ liệu thành công");
                return RedirectToAction("Edit", new { id = store.Id , fromCreateView =true});
            }

            //prepare model
            model = _storeModelFactory.PrepareStoreModel(model, null, true);

            //if we got this far, something failed, redisplay form
            return View(model);
        }
        public virtual IActionResult Edit(int id,bool fromCreateView = false)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDoanhNghiep))
            {
                return AccessDeniedView();
            }
            var store = _storeService.GetStoreById(id);
            if (store == null)
                return RedirectToAction("List");
            //prepare model
            var model = _storeModelFactory.PrepareStoreModel(null,store);
            ViewBag.FromCreate = fromCreateView;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(StoreModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDoanhNghiep))
                return AccessDeniedView();

            //try to get a store with the specified id
            var store = _storeService.GetStoreById(model.Id);
            if (store == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                store = model.ToEntity(store);

                //ensure we have "/" at the end
                if (!store.Url.EndsWith("/"))
                    store.Url += "/";

                _storeService.UpdateStore(store);

                //activity log
                _customerActivityService.InsertActivity("EditStore",
                    string.Format("Chỉnh sửa doanh nghiệp: {0}", store.Id), store);

                //locales
                //UpdateAttributeLocales(store, model);

                _notificationService.SuccessNotification("Đã chỉnh sửa thành công!!");

                return continueEditing ? RedirectToAction("Edit", new { id = store.Id }) : RedirectToAction("List");
            }

            //prepare model
            model = _storeModelFactory.PrepareStoreModel(model, store, true);

            //if we got this far, something failed, redisplay form
            return View(model);
        }
        public virtual IActionResult GetBasicInfoView(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDoanhNghiep))
            {
                return AccessDeniedView();
            }
            var store = _storeService.GetStoreById(id);
            if (store == null)
                return null;
            //prepare model
            var model = _storeModelFactory.PrepareStoreModel(null, store);
            return PartialView("_DoanhNghiepBasicForm", model);
        }
        public virtual IActionResult GetConfigInfoView(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDoanhNghiep))
            {
                return AccessDeniedView();
            }
            var store = _storeService.GetStoreById(id);
            if (store == null)
                return null;
            //prepare model
            var model = _storeModelFactory.PrepareStoreModel(null, store);
            return PartialView("_DoanhNghiepConfig", model);
        }
        
        [HttpPost]
        public virtual IActionResult Update(StoreModel model)
        {

            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDoanhNghiep))
                return this.JsonDeniedMessage();
            //try to get a store with the specified id
            var store = _storeService.GetStoreById(model.Id);
            if (store == null)
                return this.JsonNotFoundMessage();

            if (ModelState.IsValid)
            {
                _storeModelFactory.PrepareStore(model,store);

                //ensure we have "/" at the end
                if (!String.IsNullOrEmpty(store.Url)&&!store.Url.EndsWith("/"))
                    store.Url += "/";

                _storeService.UpdateStore(store);

                //activity log
                _customerActivityService.InsertActivity("EditStore",
                    string.Format("Chỉnh sửa doanh nghiệp: {0}", store.Name), store);
              
                return this.JsonSuccessMessage("Đã chỉnh sửa thành công", model);
            }

            return this.JsonErrorMessage("Có lỗi!", ModelState.SerializeErrors());
        }
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLDoanhNghiep))
                return AccessDeniedView();

            //try to get a store with the specified id
            var store = _storeService.GetStoreById(id);
            if (store == null)
                return RedirectToAction("List");

            try
            {
                _storeService.DeleteStore(store);

                //activity log
                _customerActivityService.InsertActivity("DeleteStore",
                    string.Format("Xóa doanh nghiệp: {0}", store.Name), store);

                //when we delete a store we should also ensure that all "per store" settings will also be deleted
                var settingsToDelete = _settingService
                    .GetAllSettings()
                    .Where(s => s.StoreId == id)
                    .ToList();
                _settingService.DeleteSettings(settingsToDelete);

                //when we had two stores and now have only one store, we also should delete all "per store" settings
                var allStores = _storeService.GetAllStores();
                if (allStores.Count == 1)
                {
                    settingsToDelete = _settingService
                        .GetAllSettings()
                        .Where(s => s.StoreId == allStores[0].Id)
                        .ToList();
                    _settingService.DeleteSettings(settingsToDelete);
                }

                _notificationService.SuccessNotification("Đã xóa doanh nghiệp thành công!!");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = store.Id });
            }
        }
    }
}
