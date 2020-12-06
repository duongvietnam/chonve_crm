using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Domain.CRM;
using Nop.Core.Domain.Localization;
using Nop.Services.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
using Nop.Web.Areas.CRM.Models.HeThongNguon;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Web.Areas.CRM.Controllers
{
    public class HomeController : BaseCRMController
    {

        private readonly IWorkContext _workContext;
        private readonly LocalizationSettings _localizationSettings;
        private readonly IDvDichVuService _dvDichVuService;
        private readonly IGdGiaoDichService _gdGiaoDichService;
        private readonly IGdGiaoDichSubService _gdGiaoDichSubService;
        private readonly IDvGiaDichVuService _dvGiaDichVuService;
        private readonly IKhDanhBaDienThoaiService _khDanhBaDienThoaiService;
        private readonly IKhKhachHangService _khKhachHangService;
        private readonly IGdGiaoDichKhachHangMapService _gdGiaoDichKhachHangMapService;
        private readonly ICdChuyenDiService _cdChuyenDiService;

        public HomeController(
            LocalizationSettings localizationSettings,
            IWorkContext workContext,
            IDvDichVuService dvDichVuService,
            IGdGiaoDichService gdGiaoDichService,
            IGdGiaoDichSubService gdGiaoDichSubService,
            IDvGiaDichVuService dvGiaDichVuService,
            IKhDanhBaDienThoaiService khDanhBaDienThoaiService,
            IKhKhachHangService khKhachHangService,
            IGdGiaoDichKhachHangMapService gdGiaoDichKhachHangMapService,
            ICdChuyenDiService cdChuyenDiService
            )
        {
            this._localizationSettings = localizationSettings;
            this._workContext = workContext;
            this._dvDichVuService = dvDichVuService;
            this._gdGiaoDichService = gdGiaoDichService;
            this._gdGiaoDichSubService = gdGiaoDichSubService;
            this._dvGiaDichVuService = dvGiaDichVuService;
            this._khDanhBaDienThoaiService = khDanhBaDienThoaiService;
            this._khKhachHangService = khKhachHangService;
            this._gdGiaoDichKhachHangMapService = gdGiaoDichKhachHangMapService;
            this._cdChuyenDiService = cdChuyenDiService;
        }
        public virtual IActionResult Index()
        {
            return View();
        }
        public virtual IActionResult Test()
        {
            return View(new KhKhachHangModel());
        }

        public async Task ExportDataLinq()
        {
            using (var httpClient = new HttpClient())
            {
                string token = await GetTokenThoiDai();
                int DoanhNghiepId = 2;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var ListHanhTrinhs = await httpClient.GetAsync("http://thoidai.chonve.vn:8080/api/xeSvc/GetAllHanhTrinh"))
                {
                    if (ListHanhTrinhs.IsSuccessStatusCode)
                    {
                        string apiResponse = await ListHanhTrinhs.Content.ReadAsStringAsync();
                        var listHanhTrinh = JsonConvert.DeserializeObject<ResponseApi<List<HanhTrinhModel>>>(apiResponse).objectInfo;
                        // xử lý hành trình -> dịch vụ
                        foreach (var hanhTrinh in listHanhTrinh)
                        {
                            var dichVu = _dvDichVuService.GetDvDichVuByIdNguon(hanhTrinh.Id, DoanhNghiepId);
                            if (dichVu != null)
                            {
                                dichVu.TEN = hanhTrinh.MoTa;
                                _dvDichVuService.UpdateDvDichVu(dichVu);
                            }
                            else
                            {
                                dichVu = new DvDichVu
                                {
                                    TEN = hanhTrinh.MoTa,
                                    TRANG_THAI = (int)EnumTrangThaiDichVu.HoatDong,
                                    ID_NGUON = hanhTrinh.Id,
                                    IS_COMBO = 0,
                                    DOANH_NGHIEP_ID = DoanhNghiepId
                                };
                                _dvDichVuService.InsertDvDichVu(dichVu);
                            }
                            // gia dich vu
                            if (_dvGiaDichVuService.GetDvGiaDichVu(dichVu.Id) == null)
                            {
                                var giaDichVu = new DvGiaDichVu
                                {
                                    NGAY_TAO = DateTime.Now,
                                    DICH_VU_ID = dichVu.Id
                                };
                                _dvGiaDichVuService.InsertDvGiaDichVu(giaDichVu);
                            }
                        }
                    }
                }
                var DateFrom = new DateTime(year: 2020, month: 11, day: 26, hour: 0, minute: 0, second: 0);
                for (var day = DateFrom.Date; day.Date <= DateTime.Now.Date; day = day.AddDays(1))
                {
                    // xử lý chuyến đi -> giao dịch
                    foreach (var dichvu in _dvDichVuService.GetAllDvDichVus(false, DoanhNghiepId))
                    {
                        if (dichvu.ID_NGUON > 0)
                        {
                            var paramChuyenDi = new ParameterChuyenDiModel
                            {
                                HanhTrinhId = dichvu.ID_NGUON,
                                TuNgay = day.ToString("yyyy-MM-dd HH:mm:ss"),
                                DenNgay = day.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")
                            };
                            StringContent content = new StringContent(JsonConvert.SerializeObject(paramChuyenDi), Encoding.UTF8, "application/json");
                            using (var ListChuyenDi = await httpClient.PostAsync(new Uri("http://thoidai.chonve.vn:8080/api/CRMSvc/GetChuyenDi"), content))
                            {
                                if (ListChuyenDi.IsSuccessStatusCode)
                                {
                                    string apiResponse = await ListChuyenDi.Content.ReadAsStringAsync();
                                    var listChuyenDi = JsonConvert.DeserializeObject<ResponseApi<List<ChuyenDiModel>>>(apiResponse).objectInfo;
                                    foreach (var chuyendi in listChuyenDi)
                                    {
                                        if (!string.IsNullOrEmpty(chuyendi.Ma))
                                        {
                                            // Chuyen Di
                                            var itemChuyenDi = _cdChuyenDiService.GetChuyenDi(ma: chuyendi.Ma, doanhNghiepId: DoanhNghiepId);
                                            if (itemChuyenDi != null)
                                            {
                                                itemChuyenDi.BIEN_SO_XE = chuyendi.BienSoXe;
                                                itemChuyenDi.DICH_VU_ID = dichvu.Id;
                                                itemChuyenDi.MA = chuyendi.Ma;
                                                itemChuyenDi.SO_GHE = chuyendi.SoGhe;
                                                itemChuyenDi.SO_KHACH = chuyendi.SoKhach;
                                                itemChuyenDi.TEN_LAI_XE = chuyendi.TenLaiXe;
                                                itemChuyenDi.TEN_LOAI_XE = chuyendi.TenLoaiXe;
                                                itemChuyenDi.NGAY_DI = chuyendi.NgayDiThuc;

                                                _cdChuyenDiService.UpdateCdChuyenDi(itemChuyenDi);
                                            }
                                            else
                                            {
                                                itemChuyenDi = new CdChuyenDi();
                                                itemChuyenDi.BIEN_SO_XE = chuyendi.BienSoXe;
                                                itemChuyenDi.DICH_VU_ID = dichvu.Id;
                                                itemChuyenDi.MA = chuyendi.Ma;
                                                itemChuyenDi.SO_GHE = chuyendi.SoGhe;
                                                itemChuyenDi.SO_KHACH = chuyendi.SoKhach;
                                                itemChuyenDi.TEN_LAI_XE = chuyendi.TenLaiXe;
                                                itemChuyenDi.TEN_LOAI_XE = chuyendi.TenLoaiXe;
                                                itemChuyenDi.DOANH_NGHIEP_ID = DoanhNghiepId;
                                                itemChuyenDi.NGAY_DI = chuyendi.NgayDiThuc;

                                                _cdChuyenDiService.InsertCdChuyenDi(itemChuyenDi);
                                            }

                                            // Dat Ve
                                            using (var ListDatVe = await httpClient.GetAsync(new Uri("http://thoidai.chonve.vn:8080/api/CRMSvc/GetDatVe?ChuyenDiId=" + chuyendi.Ma)))
                                            {
                                                string apiResponseDatVe = await ListDatVe.Content.ReadAsStringAsync();
                                                var listDatVe = JsonConvert.DeserializeObject<ResponseApi<List<DatVeModel>>>(apiResponseDatVe).objectInfo;
                                                foreach (var datVe in listDatVe)
                                                {
                                                    // khach hang
                                                    if (!string.IsNullOrEmpty(datVe.DienThoai) && !string.IsNullOrEmpty(datVe.Ma))
                                                    {
                                                        var giaoDich = _gdGiaoDichService.GetGiaoDichByMaNguon(MaNguon: datVe.Ma, DoanhNghiepId: DoanhNghiepId);
                                                        if (giaoDich != null)
                                                        {
                                                            // giao dich
                                                            giaoDich.TRANG_THAI = chuyendi.TrangThaiId == 4 ? (int)EnumTrangThaiGiaoDich.DaHuy : (int)EnumTrangThaiGiaoDich.DaThanhToan;
                                                            giaoDich.NGAY_TAO = Convert.ToDateTime(chuyendi.NgayTao);
                                                            giaoDich.NGAY_GIAO_DICH = Convert.ToDateTime(chuyendi.NgayTao);
                                                            giaoDich.NGAY_BAT_DAU = Convert.ToDateTime(chuyendi.NgayDi);
                                                            giaoDich.NGAY_KET_THUC = Convert.ToDateTime(chuyendi.NgayDi);
                                                            giaoDich.NGUOI_TAO = chuyendi.NguoiTaoId;
                                                            giaoDich.THANH_TIEN = chuyendi.GiaVe;
                                                            giaoDich.GHI_CHU = chuyendi.GhiChu;
                                                            _gdGiaoDichService.UpdateGdGiaoDich(giaoDich);

                                                            // giao dich sub
                                                            var giaoDichSubs = _gdGiaoDichSubService.GetGdGiaoDichSubs(giaoDich.Id);
                                                            foreach (var giaoDichSub in giaoDichSubs)
                                                            {
                                                                giaoDichSub.SO_TIEN = chuyendi.GiaVe;
                                                                _gdGiaoDichSubService.UpdateGdGiaoDichSub(giaoDichSub);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // giao dich
                                                            giaoDich = new GdGiaoDich
                                                            {
                                                                DOANH_NGHIEP_ID = DoanhNghiepId,
                                                                THANH_TIEN = chuyendi.GiaVe,
                                                                HINH_THUC_THANH_TOAN = (int)EnumHinhThucThanhToan.TienMat,
                                                                LOAI_ID = (int)EnumLoaiGiaoDich.GiaoDichThanhToan,
                                                                MA_GIAO_DICH = datVe.Ma,
                                                                NGAY_BAT_DAU = Convert.ToDateTime(chuyendi.NgayDi),
                                                                NGAY_KET_THUC = Convert.ToDateTime(chuyendi.NgayDi),
                                                                NGAY_GIAO_DICH = Convert.ToDateTime(chuyendi.NgayTao),
                                                                NGAY_TAO = Convert.ToDateTime(chuyendi.NgayTao),
                                                                NGUOI_TAO = chuyendi.NguoiTaoId,
                                                                TRANG_THAI = chuyendi.TrangThaiId == 4 ? (int)EnumTrangThaiGiaoDich.DaHuy : (int)EnumTrangThaiGiaoDich.DaThanhToan,
                                                                GHI_CHU = chuyendi.GhiChu
                                                            };
                                                            _gdGiaoDichService.InsertGdGiaoDich(giaoDich);

                                                            // giao dich sub
                                                            var giaoDichSubs = new GdGiaoDichSub
                                                            {
                                                                GIAO_DICH_ID = giaoDich.Id,
                                                                SO_TIEN = chuyendi.GiaVe,
                                                                DICH_VU_ID = dichvu.Id,
                                                                SO_LUONG = 1,
                                                                DIEM_DICH_VU = dichvu.DIEM_DICH_VU,
                                                                DOANH_NGHIEP_ID = DoanhNghiepId
                                                            };
                                                            _gdGiaoDichSubService.InsertGdGiaoDichSub(giaoDichSubs);
                                                        }

                                                        // gia dich vu
                                                        var giaDichVu = _dvGiaDichVuService.GetDvGiaDichVu(dichvu.Id);
                                                        giaDichVu.GIA_BAN_LE = chuyendi.GiaVe;
                                                        _dvGiaDichVuService.UpdateDvGiaDichVu(giaDichVu);

                                                        var danhBa = _khDanhBaDienThoaiService.GetKhDanhBaDienThoaiBySoDienThoai(datVe.DienThoai, DoanhNghiepId);
                                                        if (danhBa == null)
                                                        {
                                                            var khachHang = new KhKhachHang
                                                            {
                                                                TEN = datVe.TenKhachHang,
                                                                DOANH_NGHIEP_ID = DoanhNghiepId,
                                                                DA_QUET = 0
                                                            };
                                                            _khKhachHangService.InsertKhKhachHang(khachHang);
                                                            danhBa = new KhDanhBaDienThoai
                                                            {
                                                                KHACH_HANG_ID = khachHang.Id,
                                                                NGAY_TAO = DateTime.Now,
                                                                NGUOI_TAO = datVe.NguoiTaoId,
                                                                SO_DIEN_THOAI = datVe.DienThoai,
                                                                TRANG_THAI = (int)EnumTrangThaiDanhBa.HoatDong,
                                                                NHOM_DANH_BA = (int)EnumNhomDanhBa.SoChinh
                                                            };
                                                            _khDanhBaDienThoaiService.InsertKhDanhBaDienThoai(danhBa);
                                                        }
                                                        else
                                                        {
                                                            var khachHang = _khKhachHangService.GetKhKhachHangById((int)danhBa.KHACH_HANG_ID);
                                                            khachHang.DA_QUET = 0;
                                                            _khKhachHangService.UpdateKhKhachHang(khachHang);
                                                        }
                                                        var map = _gdGiaoDichKhachHangMapService.GetGdGiaoDichKhachHangMapByGiaoDichId(giaoDich.Id, DoanhNghiepId);
                                                        if (map == null || map.Count == 0)
                                                        {
                                                            // giao dich khach hang map
                                                            var giaoDichKhachHangMap = new GdGiaoDichKhachHangMap
                                                            {
                                                                GIAO_DICH_ID = giaoDich.Id,
                                                                KHACH_HANG_CHINH = danhBa.KHACH_HANG_ID,
                                                                KHACH_HANG_ID = danhBa.KHACH_HANG_ID,
                                                                DICH_VU_ID = dichvu.Id,
                                                                DOANH_NGHIEP_ID = DoanhNghiepId,
                                                                CHUYEN_DI_ID = itemChuyenDi.Id,
                                                                DIEM_DON = datVe.TenDiemDon,
                                                                DIEM_TRA = datVe.TenDiemTra
                                                            };
                                                            _gdGiaoDichKhachHangMapService.InsertGdGiaoDichKhachHangMap(giaoDichKhachHangMap);
                                                        }

                                                        // trang thai giao dich
                                                        if (datVe.isKhachHuy || datVe.TrangThaiId == 4)
                                                        {
                                                            giaoDich.TRANG_THAI = (int)EnumTrangThaiGiaoDich.DaHuy;
                                                        }
                                                        else
                                                        {
                                                            giaoDich.TRANG_THAI = (int)EnumTrangThaiGiaoDich.DaThanhToan;
                                                        }
                                                        _gdGiaoDichService.UpdateGdGiaoDich(giaoDich);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }

        public async Task ExportDataChuyenDi()
        {
            using (var httpClient = new HttpClient())
            {
                string token = await GetTokenThoiDai();
                int DoanhNghiepId = 2;
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var DateFrom = new DateTime(year: 2018, month: 12, day: 01, hour: 0, minute: 0, second: 0);
                for (var day = DateFrom.Date; day.Date <= DateTime.Now.Date; day = day.AddDays(1))
                {
                    // xử lý chuyến đi -> giao dịch
                    foreach (var dichvu in _dvDichVuService.GetAllDvDichVus(false, DoanhNghiepId))
                    {
                        if (dichvu.ID_NGUON > 0)
                        {
                                var paramChuyenDi = new ParameterChuyenDiModel
                                {
                                    HanhTrinhId = dichvu.ID_NGUON,
                                    TuNgay = day.ToString("yyyy-MM-dd HH:mm:ss"),
                                    DenNgay = day.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss")
                                };
                                StringContent content = new StringContent(JsonConvert.SerializeObject(paramChuyenDi), Encoding.UTF8, "application/json");
                                using (var ListChuyenDi = await httpClient.PostAsync(new Uri("http://thoidai.chonve.vn:8080/api/CRMSvc/GetChuyenDi"), content))
                                {
                                    if (ListChuyenDi.IsSuccessStatusCode)
                                    {
                                        string apiResponse = await ListChuyenDi.Content.ReadAsStringAsync();
                                        var listChuyenDi = JsonConvert.DeserializeObject<ResponseApi<List<ChuyenDiModel>>>(apiResponse).objectInfo;
                                        foreach (var chuyendi in listChuyenDi)
                                        {
                                            if (!string.IsNullOrEmpty(chuyendi.Ma))
                                            {
                                                // Chuyen Di
                                                var itemChuyenDi = _cdChuyenDiService.GetChuyenDi(ma: chuyendi.Ma, doanhNghiepId: DoanhNghiepId);
                                                if (itemChuyenDi != null)
                                                {
                                                    itemChuyenDi.BIEN_SO_XE = chuyendi.BienSoXe;
                                                    itemChuyenDi.DICH_VU_ID = dichvu.Id;
                                                    itemChuyenDi.MA = chuyendi.Ma;
                                                    itemChuyenDi.SO_GHE = chuyendi.SoGhe;
                                                    itemChuyenDi.SO_KHACH = chuyendi.SoKhach;
                                                    itemChuyenDi.TEN_LAI_XE = chuyendi.TenLaiXe;
                                                    itemChuyenDi.TEN_LOAI_XE = chuyendi.TenLoaiXe;

                                                    _cdChuyenDiService.UpdateCdChuyenDi(itemChuyenDi);
                                                }
                                                else
                                                {
                                                    itemChuyenDi = new CdChuyenDi();
                                                    itemChuyenDi.BIEN_SO_XE = chuyendi.BienSoXe;
                                                    itemChuyenDi.DICH_VU_ID = dichvu.Id;
                                                    itemChuyenDi.MA = chuyendi.Ma;
                                                    itemChuyenDi.SO_GHE = chuyendi.SoGhe;
                                                    itemChuyenDi.SO_KHACH = chuyendi.SoKhach;
                                                    itemChuyenDi.TEN_LAI_XE = chuyendi.TenLaiXe;
                                                    itemChuyenDi.TEN_LOAI_XE = chuyendi.TenLoaiXe;
                                                    itemChuyenDi.DOANH_NGHIEP_ID = DoanhNghiepId;

                                                    _cdChuyenDiService.InsertCdChuyenDi(itemChuyenDi);
                                                }

                                                // Dat Ve
                                                using (var ListDatVe = await httpClient.GetAsync(new Uri("http://thoidai.chonve.vn:8080/api/CRMSvc/GetDatVe?ChuyenDiId=" + chuyendi.Ma)))
                                                {
                                                    string apiResponseDatVe = await ListDatVe.Content.ReadAsStringAsync();
                                                    var listDatVe = JsonConvert.DeserializeObject<ResponseApi<List<DatVeModel>>>(apiResponseDatVe).objectInfo;
                                                    foreach (var datVe in listDatVe)
                                                    {
                                                        // khach hang
                                                        if (!string.IsNullOrEmpty(datVe.DienThoai) && !string.IsNullOrEmpty(datVe.Ma))
                                                        {
                                                            var giaoDich = _gdGiaoDichService.GetGiaoDichByMaNguon(MaNguon: datVe.Ma, DoanhNghiepId: DoanhNghiepId);
                                                            var danhBa = _khDanhBaDienThoaiService.GetKhDanhBaDienThoaiBySoDienThoai(datVe.DienThoai, DoanhNghiepId);
                                                            if (giaoDich != null && danhBa != null)
                                                            {
                                                                var map = _gdGiaoDichKhachHangMapService.GetGdGiaoDichKhachHangMapByGiaoDichId(giaoDich.Id, DoanhNghiepId);
                                                                if (map == null || map.Count == 0)
                                                                {
                                                                    // giao dich khach hang map
                                                                    var giaoDichKhachHangMap = new GdGiaoDichKhachHangMap
                                                                    {
                                                                        GIAO_DICH_ID = giaoDich.Id,
                                                                        KHACH_HANG_CHINH = danhBa.KHACH_HANG_ID,
                                                                        KHACH_HANG_ID = danhBa.KHACH_HANG_ID,
                                                                        DICH_VU_ID = dichvu.Id,
                                                                        DOANH_NGHIEP_ID = DoanhNghiepId,
                                                                        CHUYEN_DI_ID = itemChuyenDi.Id,
                                                                        DIEM_DON = datVe.TenDiemDon,
                                                                        DIEM_TRA = datVe.TenDiemTra
                                                                    };
                                                                    _gdGiaoDichKhachHangMapService.InsertGdGiaoDichKhachHangMap(giaoDichKhachHangMap);
                                                                }
                                                                else
                                                                {
                                                                    foreach (var m in map)
                                                                    {
                                                                        m.CHUYEN_DI_ID = itemChuyenDi.Id;
                                                                        m.DIEM_DON = datVe.TenDiemDon;
                                                                        m.DIEM_TRA = datVe.TenDiemTra;

                                                                        _gdGiaoDichKhachHangMapService.UpdateGdGiaoDichKhachHangMap(m);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                        }
                    }
                }
            }
        }

        public async Task<string> GetTokenThoiDai()
        {
            var login = new LoginModel
            {
                ClientId = "CVCRM",
                CodeName = "CVCRM",
                KeyPass = "Xe@2017_01_01#XE"
            };

            Uri authorizationServerTokenIssuerUri = new Uri("http://thoidai.chonve.vn:8080/api/AuthenSvc/Login");

            string rawJwtToken = "";
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(authorizationServerTokenIssuerUri, content))
                {
                    rawJwtToken = await response.Content.ReadAsStringAsync();
                }
            }

            var objAuthen = JsonConvert.DeserializeObject<ResponseApi<ObjectInfoModel>>(rawJwtToken);
            return objAuthen.objectInfo.ApiToken;
        }
    }
}
