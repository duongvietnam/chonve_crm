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
using System.Collections.Generic;
using System.Linq;

namespace Nop.Web.Areas.CRM.Controllers
{
    public partial class MarEventMarketingController : BaseCRMController
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
        private readonly IMarEventMarketingModelFactory _itemModelFactory;
        private readonly IMarEventMarketingService _itemService;
        private readonly IMarMarketingService _marMarketingService;
        private readonly IMarDoanhThuMarketingService _marDoanhThuMarketingService;
        #endregion

        #region Ctor
        public MarEventMarketingController(
            IStoreContext storeContext,
            INotificationService notificationService,
            ICustomerActivityService customerActivityService,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IWorkContext workContext,
            LocalizationSettings localizationSettings,
            IMarEventMarketingModelFactory itemModelFactory,
            IMarEventMarketingService itemService,
            IMarMarketingService marMarketingService,
            IMarDoanhThuMarketingService marDoanhThuMarketingService
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
            this._marMarketingService = marMarketingService;
            this._marDoanhThuMarketingService = marDoanhThuMarketingService;
        }
        #endregion

        #region Methods

        public virtual IActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLEventMarketing))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new MarEventMarketingSearchModel();
            var model = _itemModelFactory.PrepareMarEventMarketingSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(MarEventMarketingSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLEventMarketing))
                return AccessDeniedDataTablesJson();
            //prepare model
            var model = _itemModelFactory.PrepareMarEventMarketingListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLEventMarketing))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareMarEventMarketingModel(new MarEventMarketingModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(MarEventMarketingModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLEventMarketing))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<MarEventMarketing>();
                _itemService.InsertMarEventMarketing(item);
                _customerActivityService.InsertActivity("AddNewMarEventMarketing", string.Format("Thêm mới: {0}", item.Id), item);
                _notificationService.SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.Id }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareMarEventMarketingModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLEventMarketing))
                return AccessDeniedView();

            var item = _itemService.GetMarEventMarketingById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareMarEventMarketingModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(MarEventMarketingModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLEventMarketing))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetMarEventMarketingById(model.Id);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareMarEventMarketing(model, item);
                _itemService.UpdateMarEventMarketing(item);
                _customerActivityService.InsertActivity("EditMarEventMarketing", string.Format("Cập nhật: {0}", item.Id), item);
                _notificationService.SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.Id }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareMarEventMarketingModel(model, item);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLEventMarketing))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetMarEventMarketingById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteMarEventMarketing(item);
                //activity log  
                _customerActivityService.InsertActivity("DeleteMarEventMarketing", string.Format("Xóa: {0}", item.Id), item);
                _notificationService.SuccessNotification("Xoá dữ liệu thành công");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc.Message);
                return RedirectToAction("Edit", new { id = item.Id });
            }
        }

        public virtual IActionResult Detail(int id)
        {
            var item = _itemService.GetMarEventMarketingById(id);
            var mar = _marMarketingService.GetMarMarketingById((int)item.MARKETING_ID);
            if (mar.HINH_THUC == (int)EnumHinhThucMarketing.MaGiamGia)
            {
                return RedirectToAction("DetailMaGiamGia", "MarMarketing", new { marId = item.MARKETING_ID });
            }
            var model = item.ToModel<MarEventMarketingModel>();
            model.TenMarketing = _marMarketingService.GetMarMarketingById((int)item.MARKETING_ID).TEN;
            model.doanhThuSearchModel.MarEventId = id;

            return View(model);
        }

        public virtual IActionResult GetDoanhThuChart(int marEventId)
        {
            var listRawThongKe = _marDoanhThuMarketingService.GetMarDoanhThuMarketings(marEventId);
            var listData = listRawThongKe.Select(gl => new ChartGiaoDichModel
            {
                Time = gl.NGAY_EVENT.toDateVNString(),
                Amount = gl.DOANH_THU > 0 ? (decimal)gl.DOANH_THU : 0
            }).OrderBy(g => g.Time).ToList();

            return Json(listData);
        }
        #endregion
    }
}

