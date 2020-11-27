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
    public partial class GdGiaoDichController : BaseCRMController
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
        private readonly IGdGiaoDichModelFactory _itemModelFactory;
        private readonly IGdGiaoDichService _itemService;
        private readonly IDvDichVuModelFactory _dvDichVuModelFactory;
        private readonly IDvDonViTinhService _dvDonViTinhService;
        private readonly IDvDichVuService _dvDichVuService;
        private readonly IDvGiaDichVuService _dvGiaDichVuService;
        private readonly IGdGiaoDichSubService _gdGiaoDichSubService;
        private readonly IGdGiaoDichKhachHangMapService _gdGiaoDichKhachHangMapService;
        private readonly IKhKhachHangService _khKhachHangService;
        #endregion

        #region Ctor
        public GdGiaoDichController(
            IStoreContext storeContext,
            INotificationService notificationService,
            ICustomerActivityService customerActivityService,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IWorkContext workContext,
            LocalizationSettings localizationSettings,
            IGdGiaoDichModelFactory itemModelFactory,
            IGdGiaoDichService itemService,
            IDvDichVuModelFactory dvDichVuModelFactory,
            IGdGiaoDichService gdGiaoDichService,
            IDvDonViTinhService dvDonViTinhService,
            IDvDichVuService dvDichVuService,
            IDvGiaDichVuService dvGiaDichVuService,
            IGdGiaoDichSubService gdGiaoDichSubService,
            IGdGiaoDichKhachHangMapService gdGiaoDichKhachHangMapService,
            IKhKhachHangService khKhachHangService
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
            this._dvDichVuModelFactory = dvDichVuModelFactory;
            this._dvDonViTinhService = dvDonViTinhService;
            this._dvDichVuService = dvDichVuService;
            this._dvGiaDichVuService = dvGiaDichVuService;
            this._gdGiaoDichSubService = gdGiaoDichSubService;
            this._gdGiaoDichKhachHangMapService = gdGiaoDichKhachHangMapService;
            this._khKhachHangService = khKhachHangService;
        }
        #endregion

        #region Methods

        public virtual IActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLGiaoDich))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new GdGiaoDichSearchModel();
            var model = _itemModelFactory.PrepareGdGiaoDichSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(GdGiaoDichSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLGiaoDich))
                return AccessDeniedDataTablesJson();
            //prepare model
            var model = _itemModelFactory.PrepareGdGiaoDichListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLGiaoDich))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareGdGiaoDichModel(new GdGiaoDichModel(), null);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(GdGiaoDichModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLGiaoDich))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<GdGiaoDich>();
                _itemService.InsertGdGiaoDich(item);
                _customerActivityService.InsertActivity("AddNewGdGiaoDich", string.Format("Thêm mới: {0}", item.Id), item);
                _notificationService.SuccessNotification("Tạo mới dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.Id }) : RedirectToAction("List");
            }

            //prepare model
            model = _itemModelFactory.PrepareGdGiaoDichModel(model, null);
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLGiaoDich))
                return AccessDeniedView();

            var item = _itemService.GetGdGiaoDichById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareGdGiaoDichModel(null, item);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public virtual IActionResult Edit(GdGiaoDichModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLGiaoDich))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetGdGiaoDichById(model.Id);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareGdGiaoDich(model, item);
                _itemService.UpdateGdGiaoDich(item);
                _customerActivityService.InsertActivity("EditGdGiaoDich", string.Format("Cập nhật: {0}", item.Id), item);
                _notificationService.SuccessNotification("Cập nhật dữ liệu thành công !");
                return continueEditing ? RedirectToAction("Edit", new { id = item.Id }) : RedirectToAction("List");
            }
            //prepare model
            model = _itemModelFactory.PrepareGdGiaoDichModel(model, item);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLGiaoDich))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetGdGiaoDichById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                _itemService.DeleteGdGiaoDich(item);
                //activity log  
                _customerActivityService.InsertActivity("DeleteGdGiaoDich", string.Format("Xóa: {0}", item.Id), item);
                _notificationService.SuccessNotification("Xoá dữ liệu thành công");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc.Message);
                return RedirectToAction("Edit", new { id = item.Id });
            }
        }

        public virtual IActionResult _CreateOrUpdateSub()
        {
            var model = new GiaoDichSubModel();
            model.DDLDichVu = _dvDichVuModelFactory.PrepareDDLDichVuCombo(isAddFirst: true);

            return PartialView(model);
        }

        public virtual JsonResult _LoadFormData(int dichVuId, decimal soLuong)
        {
            var dichVu = _dvDichVuService.GetDvDichVuById(dichVuId);
            var giaDichVu = _dvGiaDichVuService.GetDvGiaDichVu(dichVuId);
            var model = new GiaoDichSubModel
            {
                DonViTinh = _dvDonViTinhService.GetDvDonViTinhById(dichVu.DON_VI_TINH).TEN,
                ThanhTienSub = (giaDichVu.GIA_BAN_LE * soLuong).ToVNStringPrice()
            };
            return Json(model);
        }
        #region Thong Ke
        public virtual IActionResult GetGDChart(int year = 0, int month = 0, int day = 0)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLGiaoDich))
                return AccessDeniedView();
            var listData = new List<ChartGiaoDichModel>();
            if (year > 0 && month == 0)
            {
                for (int i = 1; i <= 12; i++)
                {
                    listData.Add(new ChartGiaoDichModel
                    {
                        Amount = 0,
                        Time = i.ToString(),
                        Quantity = 0
                    });
                }
            }
            if (year > 0 && month > 0 && day == 0)
            {
                for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                {
                    listData.Add(new ChartGiaoDichModel
                    {
                        Amount = 0,
                        Time = i.ToString(),
                        Quantity = 0
                    });
                }
            }
            if (year > 0 && month > 0 && day != 0)
            {
                for (int i = 0; i <= 23; i++)
                {
                    listData.Add(new ChartGiaoDichModel
                    {
                        Amount = 0,
                        Time = i.ToString(),
                        Quantity=0
                    });
                }
            }

            var listRawThongKe = _itemService.GetThongKeDoanhThu(_storeContext.CurrentStore.Id, year, month, day);
            if (listData.Any() && listData.Count > 0)
            {
                foreach (var item in listRawThongKe)
                {
                    listData.FirstOrDefault(l => l.Time == item.Nhan).Amount = item.GiaTri;
                    listData.FirstOrDefault(l => l.Time == item.Nhan).Quantity = item.GiaTri1;
                }
            }
            else
            {
                listData = listRawThongKe.Select(gl => new ChartGiaoDichModel
                {
                    Time = gl.Nhan,
                    Amount = gl.GiaTri,
                    Quantity = gl.GiaTri1
                }).OrderBy(g => g.Time).ToList();
            }
            return Json(listData);
        }
        public virtual IActionResult GetDVChart(int year = 0, int month = 0, int day = 0)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLGiaoDich))
                return AccessDeniedView();
            var listRawThongKe = _itemService.GetThongKeDoanhThuDichVu(_storeContext.CurrentStore.Id, year, month, day);
            var listData = listRawThongKe.Select(r => new ChartDichVuModel()
            {
                IdDichVu = r.Id,
                SoLuong = r.SoLuong,
                DoanhThu = r.GiaTri,
                TenDichVu = r.Nhan

            }).ToList();
            return Json(listData);
        }

        public virtual IActionResult GetTopNewGiaoDichs()
        {
            var topGD = _itemService.GetTopGiaoDichByTime(_storeContext.CurrentStore.Id);
            var topKHGDMap = _gdGiaoDichKhachHangMapService.GetKHsByGDIds(topGD.Select(t => t.Id).ToArray());
            var topKH = _khKhachHangService.GetKhKhachHangByIds(topKHGDMap.Select(t => t.KHACH_HANG_ID.Value).ToArray());
            var topGDKHId = topGD.Join(topKHGDMap, a => a.Id, b => b.GIAO_DICH_ID, (a, b) => new
            {
                KHid = b.KHACH_HANG_ID,
                Time = a.NGAY_GIAO_DICH?.ToString("HH:mm"),
                Amount = a.THANH_TIEN
            });
            var topGDKH = topGDKHId.Join(topKH, a => a.KHid, b => b.Id, (a, b) => new
            {
                NameKH = b.TEN,
                DateTime = a.Time,
                TotalAmount = a.Amount,
                IdKH = b.Id
            }).ToList();
            return Json(topGDKH);
        }

        public virtual IActionResult GetGDNTChart(int stepYear = 10, bool month = true, bool day = false, bool hour = false)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLGiaoDich))
                return AccessDeniedView();
            var listRawThongKe = _itemService.GetThongKeGiaoDich(_storeContext.CurrentStore.Id, stepYear, month, day,hour);
            //var listLabel = listRawThongKe.GroupBy(lr => lr.SoLuong1).ToList();
            var listData = listRawThongKe.GroupBy(lr => lr.SoLuong1).Select(l => new ChartGiaoDichNTModel
            {
                Label = l.Key.ToString(),
                GiaoDichChildren = listRawThongKe.Where(gl => gl.SoLuong1.Equals(l.Key)).Select(gl => new ChartGiaoDichModel
                {
                    Time = gl.Nhan,
                    Quantity = gl.SoLuong
                }).ToList()
            }).ToList();
            return Json(listData);
        }
        public virtual IActionResult GetSSGDNTChart(int SoMoc = 2, int year = 0, int month = 0,bool weekday = false)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLGiaoDich))
                return AccessDeniedView();
            var listRawThongKe = _itemService.GetThongKeSoSanhGiaoDichGioNgayThang(_storeContext.CurrentStore.Id, SoMoc,year ,month, weekday);
            //var listLabel = listRawThongKe.GroupBy(lr => lr.SoLuong1).ToList();
            var listData = listRawThongKe.GroupBy(lr => lr.Nhan).Select(l => new ChartGiaoDichNTModel
            {
                Label = l.Key.ToString(),
                GiaoDichChildren = listRawThongKe.Where(gl => gl.Nhan.Equals(l.Key)).Select(gl => new ChartGiaoDichModel
                {
                    Time = gl.Nhan1,
                    Amount = gl.GiaTri,
                    Quantity = gl.GiaTri1
                }).ToList()
            }).ToList();
            return Json(listData);
        }
        public virtual IActionResult GetBaoCaoDoanhThuCungKyChart( int year = 0, int month = 0, int day = 0,int week = -1)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLGiaoDich))
                return AccessDeniedView();
            var date = DateTime.Now;
            var listRawThongKe = _itemService.GetThongKeBaoCaoGiaoDichCungKy(_storeContext.CurrentStore.Id, date.Year, date.Month, date.Day, week);
            return Json(listRawThongKe);
        }
        #endregion
        #endregion
    }
}

