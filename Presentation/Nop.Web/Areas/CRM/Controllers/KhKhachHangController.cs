//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Domain.CRM;
using Nop.Core.Domain.Localization;
using Nop.Services;
using Nop.Services.CRM;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.CRM.Factories.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using static Nop.Core.Domain.Common.CommonEnum;

namespace Nop.Web.Areas.CRM.Controllers
{
    public partial class KhKhachHangController : BaseCRMController
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
        private readonly IKhKhachHangModelFactory _itemModelFactory;
        private readonly IKhKhachHangService _itemService;
        private readonly IKhDanhBaDienThoaiService _khDanhBaDienThoaiService;
        private readonly IKhDanhBaDienThoaiModelFactory _khDanhBaDienThoaiModelFactory;
        private readonly IKhThongTinMoRongService _khThongTinMoRongService;
        private readonly IGdGiaoDichService _gdGiaoDichService;
        private readonly IGdGiaoDichKhachHangMapService _gdGiaoDichKhachHangMapService;
        private readonly IDvDichVuService _dvDichVuService;
        private readonly IGdGiaoDichSubService _gdGiaoDichSubService;
        private readonly ICustomerService _customerService;
        private readonly IKhNhomKhachHangMapService _nhomKhachHangMapService;
        private readonly IKhNhomKhachHangService _nhomKhachHangService;
        private readonly IChPhanHangKhachHangService _chPhanHangKhachHangService;
        private readonly IKhNhomKhachHangModelFactory _khNhomKhachHangModelFactory;
        private readonly IKhThongKeSuDungDichVuService _khThongKeSuDungDichVuService;
        private readonly IDvDichVuModelFactory _dvDichVuModelFactory;
        #endregion

        #region Ctor
        public KhKhachHangController(
            IStoreContext storeContext,
            INotificationService notificationService,
            ICustomerActivityService customerActivityService,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IWorkContext workContext,
            LocalizationSettings localizationSettings,
            IKhKhachHangModelFactory itemModelFactory,
            IKhKhachHangService itemService,
            IKhDanhBaDienThoaiService khDanhBaDienThoaiService,
            IKhDanhBaDienThoaiModelFactory khDanhBaDienThoaiModelFactory,
            IKhThongTinMoRongService khThongTinMoRongService,
            IGdGiaoDichService gdGiaoDichService,
            IGdGiaoDichKhachHangMapService gdGiaoDichKhachHangMapService,
            IDvDichVuService dvDichVuService,
            IGdGiaoDichSubService gdGiaoDichSubService,
            ICustomerService customerService,
            IKhNhomKhachHangMapService nhomKhachHangMapService,
            IKhNhomKhachHangService nhomKhachHangService,
            IChPhanHangKhachHangService chPhanHangKhachHangService,
            IKhNhomKhachHangModelFactory khNhomKhachHangModelFactory,
            IDvDichVuModelFactory dvDichVuModelFactory,
            IKhThongKeSuDungDichVuService khThongKeSuDungDichVuService
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
            this._khDanhBaDienThoaiService = khDanhBaDienThoaiService;
            this._khDanhBaDienThoaiModelFactory = khDanhBaDienThoaiModelFactory;
            this._khThongTinMoRongService = khThongTinMoRongService;
            this._gdGiaoDichService = gdGiaoDichService;
            this._gdGiaoDichKhachHangMapService = gdGiaoDichKhachHangMapService;
            this._dvDichVuService = dvDichVuService;
            this._gdGiaoDichSubService = gdGiaoDichSubService;
            this._customerService = customerService;
            this._nhomKhachHangMapService = nhomKhachHangMapService;
            this._nhomKhachHangService = nhomKhachHangService;
            this._chPhanHangKhachHangService = chPhanHangKhachHangService;
            this._khNhomKhachHangModelFactory = khNhomKhachHangModelFactory;
            this._dvDichVuModelFactory = dvDichVuModelFactory;
            this._khThongKeSuDungDichVuService = khThongKeSuDungDichVuService;
        }
        #endregion

        #region Methods

        public virtual IActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLKhachHang))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new KhKhachHangSearchModel();
            var model = _itemModelFactory.PrepareKhKhachHangSearchModel(searchmodel);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(KhKhachHangSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLKhachHang))
                return AccessDeniedDataTablesJson();
            //prepare model
            var model = _itemModelFactory.PrepareKhKhachHangListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLKhachHang))
                return AccessDeniedView();
            //prepare model
            var model = _itemModelFactory.PrepareKhKhachHangModel(new KhKhachHangModel(), null);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Create(KhKhachHangModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLKhachHang))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var item = model.ToEntity<KhKhachHang>();
                item.DOANH_NGHIEP_ID = _storeContext.CurrentStore.Id;
                _itemService.InsertKhKhachHang(item);
                // danh ba dien thoai
                foreach (var sdt in model.DanhBaDienThoai)
                {
                    if (!string.IsNullOrEmpty(sdt.SO_DIEN_THOAI))
                    {
                        var danhBa = new KhDanhBaDienThoai
                        {
                            KHACH_HANG_ID = item.Id,
                            NHOM_DANH_BA = sdt.NHOM_DANH_BA,
                            SO_DIEN_THOAI = sdt.SO_DIEN_THOAI,
                            TRANG_THAI = (int)EnumTrangThaiDanhBa.HoatDong
                        };
                        _khDanhBaDienThoaiService.InsertKhDanhBaDienThoai(danhBa);
                    }
                }
                // cau hinh mo rong
                foreach (var cauHinh in model.ListCauHinhMoRong)
                {
                    if (!string.IsNullOrEmpty(cauHinh.Value))
                    {
                        var thongTinMoRong = new KhThongTinMoRong
                        {
                            KHACH_HANG_ID = item.Id,
                            CAU_HINH_ID = cauHinh.CauHinhId,
                            VALUE = cauHinh.Value
                        };
                        _khThongTinMoRongService.InsertKhThongTinMoRong(thongTinMoRong);
                    }
                }
                _customerActivityService.InsertActivity("AddNewKhKhachHang", string.Format("Thêm mới: {0}", item.Id), item);
                return this.JsonSuccessMessage("Thêm mới khách hàng thành công !", item);
            }

            return this.JsonErrorMessage("Có lỗi !", ModelState.SerializeErrors());
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLKhachHang))
                return AccessDeniedView();

            var item = _itemService.GetKhKhachHangById(id);
            if (item == null)
                return RedirectToAction("List");
            //prepare model
            var model = _itemModelFactory.PrepareKhKhachHangModel(null, item);
            return View(model);
        }

        [HttpPost]
        public virtual IActionResult Edit(KhKhachHangModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLKhachHang))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKhKhachHangById(model.Id);
            if (item == null)
                return RedirectToAction("List");
            if (ModelState.IsValid)
            {
                _itemModelFactory.PrepareKhKhachHang(model, item);
                _itemService.UpdateKhKhachHang(item);
                // danh ba dien thoai
                foreach (var sdt in model.DanhBaDienThoai)
                {
                    if (!string.IsNullOrEmpty(sdt.SO_DIEN_THOAI))
                    {
                        if (sdt.SoDienThoaiId > 0)
                        {
                            var danhBa = _khDanhBaDienThoaiService.GetKhDanhBaDienThoaiById(sdt.SoDienThoaiId);
                            var danhBaModel = new KhDanhBaDienThoaiModel
                            {
                                Id = sdt.SoDienThoaiId,
                                SO_DIEN_THOAI = sdt.SO_DIEN_THOAI,
                                NHOM_DANH_BA = sdt.NHOM_DANH_BA
                            };
                            _khDanhBaDienThoaiModelFactory.PrepareKhDanhBaDienThoai(danhBaModel, danhBa);
                            _khDanhBaDienThoaiService.UpdateKhDanhBaDienThoai(danhBa);
                        }
                        else
                        {
                            var danhBa = new KhDanhBaDienThoai
                            {
                                SO_DIEN_THOAI = sdt.SO_DIEN_THOAI,
                                NHOM_DANH_BA = sdt.NHOM_DANH_BA,
                                KHACH_HANG_ID = item.Id,
                                TRANG_THAI = (int)EnumTrangThaiDanhBa.HoatDong
                            };
                            _khDanhBaDienThoaiService.InsertKhDanhBaDienThoai(danhBa);
                        }
                    }
                }
                // thong tin mo rong
                foreach (var cauHinhMoRong in model.ListCauHinhMoRong)
                {
                    var thongTinMoRong = _khThongTinMoRongService.GetKhThongTinMoRong(khachHangId: item.Id, cauHinhId: cauHinhMoRong.CauHinhId);
                    if (thongTinMoRong != null)
                    {
                        if (!string.IsNullOrEmpty(cauHinhMoRong.Value))
                        {
                            thongTinMoRong.VALUE = cauHinhMoRong.Value;
                            _khThongTinMoRongService.UpdateKhThongTinMoRong(thongTinMoRong);
                        }
                        else
                        {
                            _khThongTinMoRongService.DeleteKhThongTinMoRong(thongTinMoRong);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(cauHinhMoRong.Value))
                        {
                            var newThongTinMoRong = new KhThongTinMoRong
                            {
                                CAU_HINH_ID = cauHinhMoRong.CauHinhId,
                                KHACH_HANG_ID = item.Id,
                                VALUE = cauHinhMoRong.Value
                            };
                            _khThongTinMoRongService.InsertKhThongTinMoRong(newThongTinMoRong);
                        }
                    }
                }
                _customerActivityService.InsertActivity("EditKhKhachHang", string.Format("Cập nhật: {0}", item.Id), item);
                return this.JsonSuccessMessage("Cập nhật thành công !", item);
            }
            return this.JsonErrorMessage("Có lỗi !", ModelState.SerializeErrors());
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLKhachHang))
                return AccessDeniedView();
            //try to get a store with the specified id
            var item = _itemService.GetKhKhachHangById(id);
            if (item == null)
                return RedirectToAction("List");
            try
            {
                item.TRANG_THAI = (int)EnumTrangThaiKhachHang.DaXoa;
                _itemService.UpdateKhKhachHang(item);
                //activity log  
                _customerActivityService.InsertActivity("DeleteKhKhachHang", string.Format("Xóa: {0}", item.Id), item);
                _notificationService.SuccessNotification("Xóa dữ liệu thành công");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc.Message);
                return RedirectToAction("Edit", new { id = item.Id });
            }
        }

        public virtual IActionResult _SoDienThoai()
        {
            var model = new DanhBaDienThoaiModel();
            model.DDLNhomSoDienThoai = (from EnumNhomDanhBa d in Enum.GetValues(typeof(EnumNhomDanhBa))
                                        select new SelectListItem
                                        {
                                            Text = CommonHelper.GetEnumDescription(d),
                                            Value = Convert.ToInt32(d).ToString()
                                        }).ToList();

            return PartialView(model);
        }

        public virtual IActionResult Details(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLKhachHang))
                return AccessDeniedView();
            var item = _itemService.GetKhKhachHangById(id);
            if (item == null||!item.DOANH_NGHIEP_ID.Equals(_storeContext.CurrentStore.Id))
            {
                return AccessDeniedView();
            }
            var model = item.ToModel<KhKhachHangModel>();
            model = _itemModelFactory.PrepareKhKhachHangDetailModel(model,item);
            return View(model);
        }

        public virtual IActionResult TradeDetail(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLKhachHang))
                return AccessDeniedView();
            var item = _itemService.GetKhKhachHangById(id);
            if (item == null || !item.DOANH_NGHIEP_ID.Equals(_storeContext.CurrentStore.Id))
            {
                return AccessDeniedView();
            }
            var model = item.ToModel<KhKhachHangModel>();
            model = _itemModelFactory.PrepareKhKhachHangTradeDetailModel(model,item);
            return View(model);
        }

        public virtual IActionResult GetGDChart(int idKH, int year = 0, int month = 0, int day = 0)
        {
            var listData = new List<ChartGiaoDichModel>();
            if (year > 0 && month == 0)
            {
                for (int i = 1; i <= 12; i++)
                {
                    listData.Add(new ChartGiaoDichModel
                    {
                        Amount = 0,
                        Time = i.ToString()
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
                        Time = i.ToString()
                    });
                }
            }
            if (year > 0 && month > 0 && day > 0)
            {
                for (int i = 0; i <= 23; i++)
                {
                    listData.Add(new ChartGiaoDichModel
                    {
                        Amount = 0,
                        Time = i.ToString()
                    });
                }
            }

            var listRawThongKe = _gdGiaoDichService.GetThongKeChiTieuKhachHang(_storeContext.CurrentStore.Id,idKH, year, month, day);
            if (listData.Any() && listData.Count > 0)
            {
                foreach (var item in listRawThongKe)
                {
                    listData.FirstOrDefault(l => l.Time == item.Nhan).Amount = item.GiaTri;
                }
            }
            else
            {
                listData = listRawThongKe.Select(gl => new ChartGiaoDichModel
                {
                    Time = gl.Nhan,
                    Amount = gl.GiaTri
                }).OrderBy(g => g.Time).ToList();
            }
            return Json(listData);
        }

        public virtual IActionResult GetTopDVKhachHang(int idKH)
        {
            var tkKhachHang = _khThongKeSuDungDichVuService.GetKhThongKeSuDungDichVuByIdKH(idKH);
            var model = new List<KhDVTopModel>();
            if (tkKhachHang != null)
            {
                var DSDichVu=_dvDichVuService.GetAllDvDichVus(false, _storeContext.CurrentStore.Id).ToList();
                var jsonStrTDV = tkKhachHang.TOP_DICH_VU;
                if (!String.IsNullOrEmpty(jsonStrTDV))
                {
                    model.Add(getDsTopGiaoDich(jsonStrTDV,DSDichVu, nameof(tkKhachHang.TOP_DICH_VU)));
                }
                var jsonStrTDV30 = tkKhachHang.TOP_DICH_VU_30DAYS;
                if (!String.IsNullOrEmpty(jsonStrTDV30)) model.Add(getDsTopGiaoDich(jsonStrTDV30, DSDichVu, nameof(tkKhachHang.TOP_DICH_VU_30DAYS)));
                var jsonStrTDV60 = tkKhachHang.TOP_DICH_VU_60DAYS;
                if (!String.IsNullOrEmpty(jsonStrTDV60)) model.Add(getDsTopGiaoDich(jsonStrTDV60, DSDichVu, nameof(tkKhachHang.TOP_DICH_VU_60DAYS)));
                var jsonStrTDV90 = tkKhachHang.TOP_DICH_VU_90DAYS;
                if (!String.IsNullOrEmpty(jsonStrTDV90)) model.Add(getDsTopGiaoDich(jsonStrTDV90, DSDichVu, nameof(tkKhachHang.TOP_DICH_VU_90DAYS)));

            }
            return Json(model);
        }

        private KhDVTopModel getDsTopGiaoDich(string jsonStr, List<DvDichVu> DSDichVu,string NameDVTop)
        {
            var topList = JsonConvert.DeserializeObject<List<KhTopGiaoDichModel>>(jsonStr);
            var listDVName = DSDichVu.Select(dv => new {
                idDV = dv.Id,
                nameDV = dv.TEN
            }).ToList();
            var topL = topList.Join(listDVName, c => c.TopGiaoDich.DichVuId, b => b.idDV, (c, b) => new KhTopGiaoDichModel
            {
                TopGiaoDich = new KhTopDichVuModel
                {
                    DichVuId = c.TopGiaoDich.DichVuId,
                    TenDichVu = b.nameDV,
                    SoLan = c.TopGiaoDich.SoLan
                }
            }).ToList();
            return new KhDVTopModel { DsTopGiaoDich = topL, NameDVTop = NameDVTop };
        } 

        public virtual IActionResult PhanTichKhachHang()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLPhanHangKhachHang))
                return AccessDeniedView();
            var model = new KhKhachHangModel();
            return View(model);
        }

        public virtual IActionResult _GetPhanHangKhachHang()
        {
            var searchModel = new KhPhanTichKhachHangSearchModel();
            searchModel.DDLOptionOrderBy = ((EnumSapXepHangKhachHang)searchModel.OptionOrderBy).ToSelectList();
            searchModel.DDLHangKhachHang = _itemModelFactory.PrepareDDLHangKhachHang();

            return PartialView(searchModel);
        }

        [HttpPost]
        public virtual IActionResult _GetPhanHangKhachHang(KhPhanTichKhachHangSearchModel searchModel)
        {
            var model = _itemModelFactory.PreparePhanHangKhachHangListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult _GetPhanNhomKhachHang()
        {
            var searchModel = new KhPhanTichKhachHangSearchModel();
            searchModel.DDLNhomKhachHang = _khNhomKhachHangModelFactory.PrepareSelectListNhomKhachHang(isAddFirst: true);
            searchModel.DDLOptionOrderBy = ((EnumSapXepHangKhachHang)searchModel.OptionOrderBy).ToSelectList();

            return PartialView(searchModel);
        }

        [HttpPost]
        public virtual IActionResult _GetPhanNhomKhachHang(KhPhanTichKhachHangSearchModel searchModel)
        {
            var model = _itemModelFactory.PrepareNhomKhachHangListModel(searchModel);
            return Json(model);
        }

        public virtual IActionResult _GetThoiQuenKhachHang()
        {
            var searchModel = new KhPhanTichKhachHangSearchModel();
            searchModel.DDLDichVu = _dvDichVuModelFactory.PrepareDDLDichVuCombo(isCombo: false, isAddFirst: true);

            return PartialView(searchModel);
        }

        [HttpPost]
        public virtual IActionResult _GetThoiQuenKhachHang(KhPhanTichKhachHangSearchModel searchModel)
        {
            var model = _itemModelFactory.PrepareThoiQuenKhachHangListModel(searchModel);
            return Json(model);
        }
        #endregion

        #region Thong Ke Khach Hang
        public virtual IActionResult GetKHPhanHangChart(int IdTieuChiPH = 1)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMQLKhachHang))
                return AccessDeniedView();
            var listRawThongKe = _itemService.GetThongKeKhachHangTheoPhanHang(_storeContext.CurrentStore.Id, IdTieuChiPH);
            var listData = listRawThongKe.Select(r => new ChartKhachHangPHModel()
            {
                IdPhanHang = r.Id,
                SoLuong = r.SoLuong,
                DoanhThu = r.GiaTri,
                TenPhanHang = r.Nhan??"?"

            }).ToList();
            return Json(listData);
        }
        #endregion
    }
}

