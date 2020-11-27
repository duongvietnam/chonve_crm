//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 4/11/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Localization;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Security;
using Nop.Services.Messages;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework.Security;
using Nop.Web.Areas.CRM.Models.CRM;
using Nop.Web.Areas.CRM.Factories.CRM;
using Nop.Services.CRM;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Core.Domain.CRM;

namespace Nop.Web.Areas.CRM.Controllers
{
	public partial class ChGiaTriThongTinMoRongController : BaseCRMController
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
            private readonly IChGiaTriThongTinMoRongModelFactory _itemModelFactory;
            private readonly IChGiaTriThongTinMoRongService _itemService;
         #endregion
     
        #region Ctor
        public ChGiaTriThongTinMoRongController(
            IStoreContext storeContext,
            INotificationService notificationService,
            ICustomerActivityService customerActivityService,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService,            
            IPermissionService permissionService,            
            IWorkContext workContext,
            LocalizationSettings localizationSettings,
            IChGiaTriThongTinMoRongModelFactory itemModelFactory,
            IChGiaTriThongTinMoRongService itemService
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
            var searchmodel = new ChGiaTriThongTinMoRongSearchModel ();
            var model = _itemModelFactory.PrepareChGiaTriThongTinMoRongSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(ChGiaTriThongTinMoRongSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedDataTablesJson();
            //prepare model
            var model = _itemModelFactory.PrepareChGiaTriThongTinMoRongListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareChGiaTriThongTinMoRongModel(new ChGiaTriThongTinMoRongModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(ChGiaTriThongTinMoRongModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<ChGiaTriThongTinMoRong>();                
                _itemService.InsertChGiaTriThongTinMoRong(item);                
                _customerActivityService.InsertActivity("AddNewChGiaTriThongTinMoRong",string.Format("Thêm mới: {0}",item.Id),item);
                _notificationService.SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.Id }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareChGiaTriThongTinMoRongModel(model, null);            
            return View(model);
        } 
        
        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();
                
            var item = _itemService.GetChGiaTriThongTinMoRongById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareChGiaTriThongTinMoRongModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(ChGiaTriThongTinMoRongModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetChGiaTriThongTinMoRongById(model.Id);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareChGiaTriThongTinMoRong(model,item);
                _itemService.UpdateChGiaTriThongTinMoRong(item); 
                _customerActivityService.InsertActivity("EditChGiaTriThongTinMoRong",string.Format("Cập nhật: {0}",item.Id),item);
                _notificationService.SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.Id }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareChGiaTriThongTinMoRongModel(model, item);
            return View(model);
        }
        
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAccess))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetChGiaTriThongTinMoRongById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteChGiaTriThongTinMoRong(item);
                //activity log  
                _customerActivityService.InsertActivity("DeleteChGiaTriThongTinMoRong",string.Format("Xóa: {0}",item.Id),item);
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

