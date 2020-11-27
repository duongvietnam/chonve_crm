//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.CRM;
using Nop.Services;
using Nop.Services.CRM;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.CRM.Models.CRM;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using static Nop.Core.Domain.Common.CommonEnum;

namespace Nop.Web.Areas.CRM.Factories.CRM
{
    public class KhKhachHangModelFactory : IKhKhachHangModelFactory
    {
        #region Fields    		
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ILocalizationService _localizationService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IKhKhachHangService _itemService;
        private readonly IKhDanhBaDienThoaiService _khDanhBaDienThoaiService;
        private readonly IKhCauHinhMoRongService _khCauHinhMoRongService;
        private readonly IKhThongTinMoRongService _khThongTinMoRongService;
        private readonly IChPhanHangKhachHangService _chPhanHangKhachHangService;
        private readonly IGdGiaoDichService _gdGiaoDichService;
        private readonly IGdGiaoDichKhachHangMapService _gdGiaoDichKhachHangMapService;
        private readonly IKhNhomKhachHangMapService _khNhomKhachHangMapService;
        private readonly IKhNhomKhachHangService _khNhomKhachHangService;
        private readonly IKhThongKeSuDungDichVuService _khThongKeSuDungDichVuService;
        private readonly IDvDichVuService _dvDichVuService;
        private readonly ICustomerService _customerService;
        private readonly IGdGiaoDichSubService _gdGiaoDichSubService;
        private readonly IKhNhomKhachHangMapService _nhomKhachHangMapService;

        #endregion

        #region Ctor

        public KhKhachHangModelFactory(
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager,
            IKhKhachHangService itemService,
            IKhDanhBaDienThoaiService khDanhBaDienThoaiService,
            IKhCauHinhMoRongService khCauHinhMoRongService,
            IKhThongTinMoRongService khThongTinMoRongService,
            IChPhanHangKhachHangService chPhanHangKhachHangService,
            IGdGiaoDichService gdGiaoDichService,
            IGdGiaoDichKhachHangMapService gdGiaoDichKhachHangMapService,
            IKhNhomKhachHangMapService khNhomKhachHangMapService,
            IKhNhomKhachHangService khNhomKhachHangService,
            IKhThongKeSuDungDichVuService khThongKeSuDungDichVuService,
            IDvDichVuService dvDichVuService,
            ICustomerService customerService,
            IGdGiaoDichSubService gdGiaoDichSubService,
            IKhNhomKhachHangMapService nhomKhachHangMapService
            )
        {
            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._khDanhBaDienThoaiService = khDanhBaDienThoaiService;
            this._khCauHinhMoRongService = khCauHinhMoRongService;
            this._khThongTinMoRongService = khThongTinMoRongService;
            this._chPhanHangKhachHangService = chPhanHangKhachHangService;
            this._gdGiaoDichService = gdGiaoDichService;
            this._gdGiaoDichKhachHangMapService = gdGiaoDichKhachHangMapService;
            this._khNhomKhachHangMapService = khNhomKhachHangMapService;
            this._khNhomKhachHangService = khNhomKhachHangService;
            this._khThongKeSuDungDichVuService = khThongKeSuDungDichVuService;
            this._dvDichVuService = dvDichVuService;
            this._customerService = customerService;
            this._gdGiaoDichSubService = gdGiaoDichSubService;
            this._nhomKhachHangMapService = nhomKhachHangMapService;
        }
        #endregion

        #region KhKhachHang
        public KhKhachHangSearchModel PrepareKhKhachHangSearchModel(KhKhachHangSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.GenderLabels =  ((EnumGioiTinh)searchModel.GenderSearch.GetValueOrDefault(0)).ToSelectList();
            searchModel.TypeLabels = ((EnumLoaiKhachHang)searchModel.GenderSearch.GetValueOrDefault(0)).ToSelectList(); 
            searchModel.SetGridPageSize();
            return searchModel;
        }

        public KhKhachHangListModel PrepareKhKhachHangListModel(KhKhachHangSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchKhKhachHangs(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, email: searchModel.EmailSearch, gender: searchModel.GenderSearch, floor: searchModel.FloorPointSearch, top: searchModel.TopPointSearch, type: searchModel.TypeSearch,searchModel.DayTimeSearch);
            //prepare list model
            var model = new KhKhachHangListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<KhKhachHangModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<KhKhachHangModel>();
                    itemModel.NgaySinhString = itemModel.NGAY_SINH.toDateVNString();
                    itemModel.SoDienThoai = string.Join(Environment.NewLine, _khDanhBaDienThoaiService.GetKhDanhBaDienThoaiByKhachHang(item.Id).Select(c => c.SO_DIEN_THOAI));
                    ls.Add(itemModel);
                }
                return ls;
            });
            if (!String.IsNullOrEmpty(searchModel.PhoneSearch))
            {
                int? idKH = null;
                idKH = _khDanhBaDienThoaiService.GetKhDanhBaDienThoaiBySoDienThoai(searchModel.PhoneSearch, _storeContext.CurrentStore.Id)?.KHACH_HANG_ID;
                model.Data = model.Data.Where(d => d.Id.Equals(idKH));
            }
            return model;
        }

        public KhKhachHangListModel PreparePhanHangKhachHangListModel(KhPhanTichKhachHangSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var items = _itemService.SearchPhanTichKhachHang(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, isTangDan: Convert.ToBoolean(searchModel.OptionOrderBy), HangKhachHang: searchModel.HangKhachHang, isHangKhachHang: true);

            //prepare list model
            var model = new KhKhachHangListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<KhKhachHangModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<KhKhachHangModel>();
                    itemModel.SoDienThoai = string.Join(Environment.NewLine, _khDanhBaDienThoaiService.GetKhDanhBaDienThoaiByKhachHang(item.Id).Select(c => c.SO_DIEN_THOAI));
                    itemModel.TongSoTienDaChiTieu = _gdGiaoDichService.GetSoTienDaChiTieu(khachHangId: item.Id, TuNgay: new DateTime(day: 01, month: 01, year: DateTime.Now.Year), DenNgay: DateTime.Now).ToVNStringPrice() + " / " + _gdGiaoDichService.GetSoTienDaChiTieu(khachHangId: item.Id, TuNgay: null, DenNgay: null).ToVNStringPrice();
                    itemModel.SoLanSuDungDichVu = _gdGiaoDichKhachHangMapService.SoLanSuDungDichVuTrongNam(khachHangId: item.Id, TuNgay: new DateTime(day: 01, month: 01, year: DateTime.Now.Year), DenNgay: DateTime.Now);
                    var option = _chPhanHangKhachHangService.GetChPhanHangKhachHangActive(_storeContext.CurrentStore.Id);
                    if (item.DIEM_PHAN_HANG >= option.GIA_TRI_CAP_3)
                    {
                        itemModel.CapHangHienTai = option.TEN_CAP_3;
                    }
                    else if (item.DIEM_PHAN_HANG >= option.GIA_TRI_CAP_2)
                    {
                        itemModel.CapHangHienTai = option.TEN_CAP_2;
                    }
                    else if (item.DIEM_PHAN_HANG >= option.GIA_TRI_CAP_1)
                    {
                        itemModel.CapHangHienTai = option.TEN_CAP_1;
                    }
                    else
                    {
                        itemModel.CapHangHienTai = "Chưa phân hạng";
                    }

                    ls.Add(itemModel);
                }
                return ls;
            });

            return model;
        }

        public KhKhachHangListModel PrepareNhomKhachHangListModel(KhPhanTichKhachHangSearchModel searchModel)
        {
            var items = _itemService.SearchPhanTichKhachHang(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, isNhomKhachHang: true, nhomKhachHang: searchModel.NhomKhachHang, TuDiem: searchModel.TuDiem, DenDiem: searchModel.DenDiem, isTangDan: Convert.ToBoolean(searchModel.OptionOrderBy));

            //prepare list model
            var model = new KhKhachHangListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<KhKhachHangModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<KhKhachHangModel>();
                    itemModel.SoDienThoai = string.Join(Environment.NewLine, _khDanhBaDienThoaiService.GetKhDanhBaDienThoaiByKhachHang(item.Id).Select(c => c.SO_DIEN_THOAI));
                    itemModel.TongSoTienDaChiTieu = _gdGiaoDichService.GetSoTienDaChiTieu(khachHangId: item.Id, TuNgay: new DateTime(day: 01, month: 01, year: DateTime.Now.Year), DenNgay: DateTime.Now).ToVNStringPrice() + " / " + _gdGiaoDichService.GetSoTienDaChiTieu(khachHangId: item.Id, TuNgay: null, DenNgay: null).ToVNStringPrice();
                    itemModel.SoLanSuDungDichVu = _gdGiaoDichKhachHangMapService.SoLanSuDungDichVuTrongNam(khachHangId: item.Id, TuNgay: new DateTime(day: 01, month: 01, year: DateTime.Now.Year), DenNgay: DateTime.Now);
                    var listMap = _khNhomKhachHangMapService.GetKhNhomKhachHangMapByIdKhachHang(item.Id).Where(m => m.NHOM_KHACH_HANG_ID!=null);
                    var listNhomKH = _khNhomKhachHangService.GetAllKhNhomKhachHangs(StoreId:_storeContext.CurrentStore.Id);
                    foreach (var map in listMap)
                    {
                        if (map.NHOM_KHACH_HANG_ID > 0)
                        {
                            var nhom = new NhomKhachHangModel
                            {
                                Diem = map.DIEM_DANH_GIA > 0 ? (int)map.DIEM_DANH_GIA : 0,
                                TenNhom = listNhomKH.First(n => n.Id == (int)map.NHOM_KHACH_HANG_ID).TEN
                            };
                            itemModel.NhomKhachHang.Add(nhom);
                        }
                    }

                    ls.Add(itemModel);
                }
                return ls;
            });

            return model;
        }

        public KhKhachHangListModel PrepareThoiQuenKhachHangListModel(KhPhanTichKhachHangSearchModel searchModel)
        {
            var items = _itemService.SearchPhanTichKhachHang(StoreId: _storeContext.CurrentStore.Id, Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, isThoiQuen: true, dichVuId: searchModel.DichVu);

            //prepare list model
            var model = new KhKhachHangListModel().PrepareToGrid(searchModel, items, () =>
            {
                var ls = new List<KhKhachHangModel>();
                foreach (var item in items)
                {
                    var itemModel = item.ToModel<KhKhachHangModel>();
                    itemModel.SoDienThoai = string.Join(Environment.NewLine, _khDanhBaDienThoaiService.GetKhDanhBaDienThoaiByKhachHang(item.Id).Select(c => c.SO_DIEN_THOAI));
                    var thoiQuen = _khThongKeSuDungDichVuService.GetKhThongKeSuDungDichVuByIdKH(item.Id);
                    itemModel.TopGiaoDich = thoiQuen.TOP_DICH_VU != null ? thoiQuen.TOP_DICH_VU.toEntities<KhTopGiaoDichModel>() : null;
                    itemModel.TopGiaoDich30days = thoiQuen.TOP_DICH_VU_30DAYS != null ? thoiQuen.TOP_DICH_VU_30DAYS.toEntities<KhTopGiaoDichModel>() : null;
                    itemModel.TopGiaoDich60days = thoiQuen.TOP_DICH_VU_60DAYS != null ? thoiQuen.TOP_DICH_VU_60DAYS.toEntities<KhTopGiaoDichModel>() : null;
                    itemModel.TopGiaoDich90days = thoiQuen.TOP_DICH_VU_90DAYS != null ? thoiQuen.TOP_DICH_VU_90DAYS.toEntities<KhTopGiaoDichModel>() : null;

                    if (itemModel.TopGiaoDich != null && itemModel.TopGiaoDich.Count > 0)
                    {
                        foreach (var giaoDich in itemModel.TopGiaoDich)
                        {
                            giaoDich.TopGiaoDich.TenDichVu = _dvDichVuService.GetDvDichVuById(giaoDich.TopGiaoDich.DichVuId).TEN;
                        }
                    }

                    if (itemModel.TopGiaoDich30days != null && itemModel.TopGiaoDich30days.Count > 0)
                    {
                        foreach (var giaoDich in itemModel.TopGiaoDich30days)
                        {
                            giaoDich.TopGiaoDich.TenDichVu = _dvDichVuService.GetDvDichVuById(giaoDich.TopGiaoDich.DichVuId).TEN;
                        }
                    }

                    if (itemModel.TopGiaoDich60days != null && itemModel.TopGiaoDich60days.Count > 0)
                    {
                        foreach (var giaoDich in itemModel.TopGiaoDich60days)
                        {
                            giaoDich.TopGiaoDich.TenDichVu = _dvDichVuService.GetDvDichVuById(giaoDich.TopGiaoDich.DichVuId).TEN;
                        }
                    }

                    if (itemModel.TopGiaoDich90days != null && itemModel.TopGiaoDich90days.Count > 0)
                    {
                        foreach (var giaoDich in itemModel.TopGiaoDich90days)
                        {
                            giaoDich.TopGiaoDich.TenDichVu = _dvDichVuService.GetDvDichVuById(giaoDich.TopGiaoDich.DichVuId).TEN;
                        }
                    }

                    ls.Add(itemModel);
                }
                return ls;
            });

            return model;
        }

        public KhKhachHangModel PrepareKhKhachHangModel(KhKhachHangModel model, KhKhachHang item)
        {
            if (item != null)
            {
                //fill in model values from the entity 
                model = item.ToModel<KhKhachHangModel>();
                var listDanhBa = _khDanhBaDienThoaiService.GetKhDanhBaDienThoaiByKhachHang(khachHangId: item.Id);
                if (listDanhBa != null && listDanhBa.Count > 0)
                {
                    model.DanhBaDienThoai = listDanhBa.Select(c =>
                    {
                        var danhBa = new DanhBaDienThoaiModel();
                        danhBa.SoDienThoaiId = c.Id;
                        danhBa.SO_DIEN_THOAI = c.SO_DIEN_THOAI;
                        danhBa.NHOM_DANH_BA = (int)c.NHOM_DANH_BA;
                        danhBa.DDLNhomSoDienThoai = (from EnumNhomDanhBa d in Enum.GetValues(typeof(EnumNhomDanhBa))
                                                     select new SelectListItem
                                                     {
                                                         Text = CommonHelper.GetEnumDescription(d),
                                                         Value = Convert.ToInt32(d).ToString()
                                                     }).ToList();
                        return danhBa;
                    }).ToList();
                }
            }
            //more
            model.DDLGioiTinh = model.gioiTinh.ToSelectList();
            model.DDLLoaiKhachHang = model.loaiKhachHang.ToSelectList();
            model.ListCauHinhMoRong = _khCauHinhMoRongService.GetCauHinhMoRongs(DoanhNghiepId: _storeContext.CurrentStore.Id).Select(c =>
            {
                var cauHinh = new CauHinhMoRongModel();
                cauHinh.CauHinhId = c.Id;
                cauHinh.LoaiDuLieu = (int)c.LOAI_DU_LIEU;
                cauHinh.TenCauHinh = c.TEN;
                if (item != null)
                {
                    cauHinh.Value = _khThongTinMoRongService.GetKhThongTinMoRong(item.Id, c.Id) != null ? _khThongTinMoRongService.GetKhThongTinMoRong(item.Id, c.Id).VALUE : "";
                }
                return cauHinh;
            }).ToList();

            return model;
        }

        public KhKhachHangModel PrepareKhKhachHangDetailModel(KhKhachHangModel model, KhKhachHang item)
        {
            model.SoDienThoai = _khDanhBaDienThoaiService.GetKhDanhBaDienThoaiByKhachHang(item.Id).FirstOrDefault(c => c.NHOM_DANH_BA == (int)EnumNhomDanhBa.SoChinh).SO_DIEN_THOAI;
            model.TongSoTienDaChiTieu = _gdGiaoDichService.GetTongSoTienDaChiTieu(item.Id).ToVNStringPrice();
            model.TongSoHoaDon = _gdGiaoDichKhachHangMapService.GetGdGiaoDichKhachHangMaps(item.Id).Select(c => c.GIAO_DICH_ID).Distinct().Count();
            model.TongSoDichVu = _gdGiaoDichKhachHangMapService.GetGdGiaoDichKhachHangMaps(item.Id).Select(c => c.DICH_VU_ID).Distinct().Count();
            model.DDLGioiTinh = model.gioiTinh.ToSelectList();
            model.DDLLoaiKhachHang = model.loaiKhachHang.ToSelectList();
            model.TenNguoiTao = _customerService.GetCustomerById(model.NGUOI_TAO.Value).Username;
            var listGiaoDich = _gdGiaoDichService.GetGiaoDichs(item.Id);

            var listSubGD = _gdGiaoDichSubService.GetAllGdGiaoDichSubs(StoreId: _storeContext.CurrentStore.Id);
            var listDichVu = _dvDichVuService.GetAllDvDichVus(isCombo: false, StoreId: _storeContext.CurrentStore.Id);
            model.ListGiaoDichModels = listGiaoDich.Select(g => {
                var map = listSubGD.Where(s => s.GIAO_DICH_ID == g.Id && s.DICH_VU_ID != null).Select(s => (int)s.DICH_VU_ID).ToList();
                var dv = listDichVu.Where(d => map.Contains(d.Id));

                var giaodich = new GiaoDichModel
                {
                    NgayGiaoDich = g.NGAY_GIAO_DICH.toDateVNString(),
                    ThanhTien = g.THANH_TIEN.ToVNStringPrice(),
                    DichVu = string.Join(", ", dv.Select(c => c.TEN)),
                    MaGiaoDich = g.MA_GIAO_DICH,
                    EnumTrangThaiGiaoDichValue = CommonHelper.GetEnumDescription((EnumTrangThaiGiaoDich)g.TRANG_THAI),
                    EnumHinhThucThanhToanValue = CommonHelper.GetEnumDescription((EnumHinhThucThanhToan)g.HINH_THUC_THANH_TOAN)
                };
                return giaodich;
            }).ToList();
            var option = _chPhanHangKhachHangService.GetChPhanHangKhachHangActive(_storeContext.CurrentStore.Id);
            if(option != null)
            {

                if (item.DIEM_PHAN_HANG >= option.GIA_TRI_CAP_3)
                {
                    model.CapHangHienTai = option.TEN_CAP_3;
                }
                else if (item.DIEM_PHAN_HANG >= option.GIA_TRI_CAP_2)
                {
                    model.CapHangHienTai = option.TEN_CAP_2;
                }
                else if (item.DIEM_PHAN_HANG >= option.GIA_TRI_CAP_1)
                {
                    model.CapHangHienTai = option.TEN_CAP_1;
                }
                else
                {
                    model.CapHangHienTai = "Chưa phân hạng";
                }
            }
            else
            {
                model.CapHangHienTai = "";
            }    
            return model;
        }

        public KhKhachHangModel PrepareKhKhachHangTradeDetailModel(KhKhachHangModel model, KhKhachHang item)
        {

            model.SoDienThoai = _khDanhBaDienThoaiService.GetKhDanhBaDienThoaiByKhachHang(item.Id).FirstOrDefault(c => c.NHOM_DANH_BA == (int)EnumNhomDanhBa.SoChinh).SO_DIEN_THOAI;
            model.TongSoTienDaChiTieu = _gdGiaoDichService.GetTongSoTienDaChiTieu(item.Id).ToVNStringPrice();
            model.TongSoHoaDon = _gdGiaoDichKhachHangMapService.GetGdGiaoDichKhachHangMaps(item.Id).Select(c => c.GIAO_DICH_ID).Distinct().Count();
            model.TongSoDichVu = _gdGiaoDichKhachHangMapService.GetGdGiaoDichKhachHangMaps(item.Id).Select(c => c.DICH_VU_ID).Distinct().Count();
            model.DDLGioiTinh = model.gioiTinh.ToSelectList();
            model.DDLLoaiKhachHang = model.loaiKhachHang.ToSelectList();
            model.TenNguoiTao = _customerService.GetCustomerById(model.NGUOI_TAO.Value).Username;
            var listNhomKhachHangMap = _nhomKhachHangMapService.GetKhNhomKhachHangMapByIdKhachHang(item.Id);
            var listNhomKH = _khNhomKhachHangService.GetAllKhNhomKhachHangs(StoreId: _storeContext.CurrentStore.Id);
            foreach (var nhomMap in listNhomKhachHangMap)
            {
                var nhomKhachHang = new NhomKhachHangModel
                {
                    Diem = nhomMap.DIEM_DANH_GIA ?? 0,
                    TenNhom = listNhomKH.First(n => n.Id == (int)nhomMap.NHOM_KHACH_HANG_ID).TEN,
                    TenKhachHang = item.TEN,
                };
                model.NhomKhachHang.Add(nhomKhachHang);
            }
            var listGiaoDich = _gdGiaoDichService.GetGiaoDichs(item.Id);
            var listSubGD = _gdGiaoDichSubService.GetAllGdGiaoDichSubs(StoreId: _storeContext.CurrentStore.Id);
            var listDichVu = _dvDichVuService.GetAllDvDichVus(isCombo: false, StoreId: _storeContext.CurrentStore.Id);
            model.ListGiaoDichModels = listGiaoDich.Select(g => {
                var map = listSubGD.Where(s => s.GIAO_DICH_ID == g.Id && s.DICH_VU_ID != null).Select(s => (int)s.DICH_VU_ID).ToList();
                var dv = listDichVu.Where(d => map.Contains(d.Id));

                var giaodich = new GiaoDichModel
                {
                    NgayGiaoDich = g.NGAY_GIAO_DICH.toDateVNString(),
                    ThanhTien = g.THANH_TIEN.ToVNStringPrice(),
                    DichVu = string.Join(", ", dv.Select(c => c.TEN)),
                    MaGiaoDich = g.MA_GIAO_DICH,
                    EnumTrangThaiGiaoDichValue = CommonHelper.GetEnumDescription((EnumTrangThaiGiaoDich)g.TRANG_THAI),
                    EnumHinhThucThanhToanValue = CommonHelper.GetEnumDescription((EnumHinhThucThanhToan)g.HINH_THUC_THANH_TOAN)
                };
                return giaodich;
            }).ToList();
            var option = _chPhanHangKhachHangService.GetChPhanHangKhachHangActive(_storeContext.CurrentStore.Id);
            if (option != null)
            {

                if (item.DIEM_PHAN_HANG >= option.GIA_TRI_CAP_3)
                {
                    model.CapHangHienTai = option.TEN_CAP_3;
                }
                else if (item.DIEM_PHAN_HANG >= option.GIA_TRI_CAP_2)
                {
                    model.CapHangHienTai = option.TEN_CAP_2;
                }
                else if (item.DIEM_PHAN_HANG >= option.GIA_TRI_CAP_1)
                {
                    model.CapHangHienTai = option.TEN_CAP_1;
                }
                else
                {
                    model.CapHangHienTai = "Chưa phân hạng";
                }
            }
            else
            {
                model.CapHangHienTai = "";
            }
            return model;
        }

        public void PrepareKhKhachHang(KhKhachHangModel model, KhKhachHang item)
        {
            item.TEN = model.TEN;
            item.NGAY_SINH = model.NGAY_SINH;
            item.GIOI_TINH = model.GIOI_TINH;
            item.EMAIL = model.EMAIL;
            item.FACEBOOK = model.FACEBOOK;
            item.ZALO = model.ZALO;
            item.LOAI_KHACH_HANG = model.LOAI_KHACH_HANG;
        }

        public IList<SelectListItem> PrepareDDLHangKhachHang()
        {
            var selectList = new List<SelectListItem>();
            var cauHinh = _chPhanHangKhachHangService.GetChPhanHangKhachHangActive(_storeContext.CurrentStore.Id);
            selectList.Insert(0, new SelectListItem
            {
                Text = "--Chọn hạng khách hàng--",
                Value = "0",
                Selected = true
            });
            if (cauHinh != null)
            {
                selectList.Insert(1, new SelectListItem
                {
                    Text = cauHinh.TEN_CAP_1,
                    Value = "1"
                });
                selectList.Insert(2, new SelectListItem
                {
                    Text = cauHinh.TEN_CAP_2,
                    Value = "2"
                });
                selectList.Insert(3, new SelectListItem
                {
                    Text = cauHinh.TEN_CAP_3,
                    Value = "3"
                });
            }

            return selectList;
        }

        public IList<SelectListItem> PrepareMultiSelectHangKhachHang()
        {
            var selectList = new List<SelectListItem>();
            var cauHinh = _chPhanHangKhachHangService.GetChPhanHangKhachHangActive(_storeContext.CurrentStore.Id);
            if (cauHinh != null)
            {
                selectList.Insert(0, new SelectListItem
                {
                    Text = cauHinh.TEN_CAP_1,
                    Value = "1"
                });
                selectList.Insert(1, new SelectListItem
                {
                    Text = cauHinh.TEN_CAP_2,
                    Value = "2"
                });
                selectList.Insert(2, new SelectListItem
                {
                    Text = cauHinh.TEN_CAP_3,
                    Value = "3"
                });
            }

            return selectList;
        }
        #endregion
    }
}

