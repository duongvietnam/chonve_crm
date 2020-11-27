using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Domain.CRM;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Services.CRM;
using Nop.Services.Logging;
using Nop.Services.Stores;
using Nop.Web.Areas.CRM.Models.HeThongNguon;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Web.Areas.CRM.Tasks
{
    public class SchedulerApiGetData : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var _logger = EngineContext.Current.Resolve<ILogger>();
            _logger.Information("Bắt đầu thực hiện việc lấy dữ liệu");

            var _dvDichVuService = EngineContext.Current.Resolve<IDvDichVuService>();
            var _gdGiaoDichService = EngineContext.Current.Resolve<IGdGiaoDichService>();
            var _gdGiaoDichSubService = EngineContext.Current.Resolve<IGdGiaoDichSubService>();
            var _dvGiaDichVuService = EngineContext.Current.Resolve<IDvGiaDichVuService>();
            var _khDanhBaDienThoaiService = EngineContext.Current.Resolve<IKhDanhBaDienThoaiService>();
            var _khKhachHangService = EngineContext.Current.Resolve<IKhKhachHangService>();
            var _gdGiaoDichKhachHangMapService = EngineContext.Current.Resolve<IGdGiaoDichKhachHangMapService>();
            var _storeService = EngineContext.Current.Resolve<IStoreService>();
            var _dataProvider = EngineContext.Current.Resolve<INopDataProvider>();
            var _cdChuyenDiService = EngineContext.Current.Resolve<ICdChuyenDiService>();

            using (var httpClient = new HttpClient())
            {
                string token = await GetTokenThoiDai();
                List<int> ListDoanhNghiepId = _storeService.GetAllStores().Select(c => c.Id).ToList();
                foreach (int DoanhNghiepId in ListDoanhNghiepId)
                {
                    switch (DoanhNghiepId)
                    {
                        // Doanh nghiệp Thời đại
                        case 2:
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

                            // xử lý chuyến đi -> giao dịch
                            foreach (var dichvu in _dvDichVuService.GetAllDvDichVus(false, DoanhNghiepId))
                            {
                                if (dichvu.ID_NGUON > 0)
                                {
                                    var paramChuyenDi = new ParameterChuyenDiModel
                                    {
                                        HanhTrinhId = dichvu.ID_NGUON,
                                        TuNgay = DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:mm:ss"),
                                        DenNgay = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
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

                            break;
                    }

                    // exec procedure [CRM_PhanNhomKhachHang_30Phut]
                    var pStoreId = SqlParameterHelper.GetInt32Parameter("StoreId", DoanhNghiepId);
                    _dataProvider.ExecuteStoredProcedure("CRM_PhanNhomKhachHang_30Phut", pStoreId);
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
