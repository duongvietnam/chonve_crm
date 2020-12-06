using AutoMapper;
using Nop.Core.Domain.CRM;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Stores;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.Mapper;
using Nop.Services.DanhMuc;
using Nop.Services.Localization;
using Nop.Web.Areas.CRM.Models.CRM;

namespace Nop.Web.Areas.CRM.Infrastructure.Mapper
{
    public class CRMMapperConfiguration : Profile, IOrderedMapperProfile
    {
        public CRMMapperConfiguration()
        {
            CreateHeThongMaps();
            CreateCRMMaps();
        }
        #region HeThong
        protected virtual void CreateHeThongMaps()
        {
            
        }
        #endregion
        #region CRM
        protected virtual void CreateCRMMaps()
        {
            CreateMap<CdChuyenDi, CdChuyenDiModel>();
            CreateMap<CdChuyenDiModel, CdChuyenDi>();
            CreateMap<ChGiaTriThongTinMoRong, ChGiaTriThongTinMoRongModel>();
            CreateMap<ChGiaTriThongTinMoRongModel, ChGiaTriThongTinMoRong>();
            CreateMap<ChPhanHangKhachHang, ChPhanHangKhachHangModel>();
            CreateMap<ChPhanHangKhachHangModel, ChPhanHangKhachHang>();
            CreateMap<ChThongTinMoRong, ChThongTinMoRongModel>();
            CreateMap<ChThongTinMoRongModel, ChThongTinMoRong>();
            CreateMap<DmSuKien, DmSuKienModel>();
            CreateMap<DmSuKienModel, DmSuKien>();
            CreateMap<DvDichVu, DvDichVuModel>()
                .ForMember(model => model.GiaBanBuon, options => options.Ignore())
                .ForMember(model => model.GiaBanLe, options => options.Ignore())
                .ForMember(model => model.ListDichVuInCombo, options => options.Ignore())
                .ForMember(model => model.DDLLoaiDichVu, options => options.Ignore())
                .ForMember(model => model.DDLNhomDichVu, options => options.Ignore())
                .ForMember(model => model.DDLDonViTinh, options => options.Ignore())
                .ForMember(model => model.ListDichVuInCombo, options => options.Ignore())
                .ForMember(model => model.comBoDichVuModels, options => options.Ignore())
                .ForMember(model => model.LoaiDichVuString, o => o.MapFrom(e => EngineContext.Current.Resolve<IDanhMucCacheService>().GetTenCRMLoaiDichVu(e.LOAI_ID)))
                .ForMember(model => model.NhomDichVuString, o => o.MapFrom(e => EngineContext.Current.Resolve<IDanhMucCacheService>().GetTenCRMNhomDichVu(e.NHOM_ID)));
            CreateMap<DvDichVuModel, DvDichVu>();
            CreateMap<DvDichVuCombo, DvDichVuComboModel>();
            CreateMap<DvDichVuComboModel, DvDichVuCombo>();
            CreateMap<DvDieuChinhGia, DvDieuChinhGiaModel>();
            CreateMap<DvDieuChinhGiaModel, DvDieuChinhGia>();
            CreateMap<DvGiaDichVu, DvGiaDichVuModel>();
            CreateMap<DvGiaDichVuModel, DvGiaDichVu>();
            CreateMap<DvLoaiDichVu, DvLoaiDichVuModel>();
            CreateMap<DvLoaiDichVuModel, DvLoaiDichVu>();
            CreateMap<DvDonViTinh, DvDonViTinhModel>();
            CreateMap<DvDonViTinhModel, DvDonViTinh>();
            CreateMap<DvNhatKyGia, DvNhatKyGiaModel>();
            CreateMap<DvNhatKyGiaModel, DvNhatKyGia>();
            CreateMap<DvNhomDichVu, DvNhomDichVuModel>();
            CreateMap<DvNhomDichVuModel, DvNhomDichVu>();
            CreateMap<GdGiaoDich, GdGiaoDichModel>()
                .ForMember(model => model.ListGiaoDichSubModels, options => options.Ignore())
                .ForMember(model => model.KhachHangSrting, options => options.Ignore())
                .ForMember(model => model.ThanhTienString, options => options.Ignore())
                .ForMember(model => model.NgayGiaoDich, options => options.Ignore());
            CreateMap<GdGiaoDichModel, GdGiaoDich>();
            CreateMap<GdGiaoDichDanhGia, GdGiaoDichDanhGiaModel>();
            CreateMap<GdGiaoDichDanhGiaModel, GdGiaoDichDanhGia>();
            CreateMap<GdGiaoDichDiem, GdGiaoDichDiemModel>();
            CreateMap<GdGiaoDichDiemModel, GdGiaoDichDiem>();
            CreateMap<GdGiaoDichKhachHangMap, GdGiaoDichKhachHangMapModel>();
            CreateMap<GdGiaoDichKhachHangMapModel, GdGiaoDichKhachHangMap>();
            CreateMap<GdGiaoDichSub, GdGiaoDichSubModel>();
            CreateMap<GdGiaoDichSubModel, GdGiaoDichSub>();
            CreateMap<KhCauHinhMoRong, KhCauHinhMoRongModel>()
                .ForMember(model => model.DLLLoaiDuLieu, options => options.Ignore())
                .ForMember(model => model.DDLCauHinhCha, options => options.Ignore());
            CreateMap<KhCauHinhMoRongModel, KhCauHinhMoRong>();
            CreateMap<KhDanhBaDienThoai, KhDanhBaDienThoaiModel>()
                .ForMember(model => model.DDLNhomSoDienThoai, options => options.Ignore());
            CreateMap<KhDanhBaDienThoaiModel, KhDanhBaDienThoai>();
            CreateMap<KhKhachHang, KhKhachHangModel>()
                .ForMember(model => model.DDLLoaiKhachHang, options => options.Ignore())
                .ForMember(model => model.DDLGioiTinh, options => options.Ignore())
                .ForMember(model => model.DanhBaDienThoai, options => options.Ignore())
                .ForMember(model => model.NgaySinhString, options => options.Ignore())
                .ForMember(model => model.SoDienThoai, options => options.Ignore())
                .ForMember(model => model.ListCauHinhMoRong, options => options.Ignore())
                .ForMember(model => model.TongSoTienDaChiTieu, options => options.Ignore())
                .ForMember(model => model.TongSoHoaDon, options => options.Ignore())
                .ForMember(model => model.TongSoDichVu, options => options.Ignore())
                .ForMember(model => model.ListGiaoDichModels, options => options.Ignore())
                .ForMember(model => model.TenNguoiTao, options => options.Ignore())
                .ForMember(model => model.GIOI_TINH_TEXT, o => o.MapFrom(e => EngineContext.Current.Resolve<ILocalizationService>().GetLocalizedEnum(e.gioiTinh, null)))
                .ForMember(model => model.LOAI_KHACH_HANG_TEXT, o => o.MapFrom(e => EngineContext.Current.Resolve<ILocalizationService>().GetLocalizedEnum(e.loaiKhachHang,null)))
                .ForMember(model => model.TRANG_THAI_TEXT, o => o.MapFrom(e => EngineContext.Current.Resolve<ILocalizationService>().GetLocalizedEnum(e.trangThaiKhachHang, null)))
                .ForMember(model => model.NhomKhachHang, options => options.Ignore())
                .ForMember(model => model.CapHangHienTai, options => options.Ignore())
                .ForMember(model => model.SoLanSuDungDichVu, options => options.Ignore())
                .ForMember(model => model.TopGiaoDich, options => options.Ignore())
                .ForMember(model => model.TopGiaoDich30days, options => options.Ignore())
                .ForMember(model => model.TopGiaoDich60days, options => options.Ignore())
                .ForMember(model => model.TopGiaoDich90days, options => options.Ignore());
            CreateMap<KhKhachHangModel, KhKhachHang>();
            CreateMap<KhNhomHeThong, KhNhomHeThongModel>();
            CreateMap<KhNhomHeThongModel, KhNhomHeThong>();
            CreateMap<KhNhomHeThongMap, KhNhomHeThongMapModel>();
            CreateMap<KhNhomHeThongMapModel, KhNhomHeThongMap>();
            CreateMap<KhNhomKhachHang, KhNhomKhachHangModel>()
                .ForMember(model => model.NhomHeThongIds, options => options.Ignore())
                .ForMember(model => model.DDLNhomHeThong, options => options.Ignore());
            CreateMap<KhNhomKhachHangModel, KhNhomKhachHang>();
            CreateMap<KhNhomKhachHangMap, KhNhomKhachHangMapModel>();
            CreateMap<KhNhomKhachHangMapModel, KhNhomKhachHangMap>();
            CreateMap<KhThongKeSuDungDichVu, KhThongKeSuDungDichVuModel>();
            CreateMap<KhThongKeSuDungDichVuModel, KhThongKeSuDungDichVu>();
            CreateMap<KhThongTinMoRong, KhThongTinMoRongModel>();
            CreateMap<KhThongTinMoRongModel, KhThongTinMoRong>();
            CreateMap<MarDoanhThuMarketing, MarDoanhThuMarketingModel>();
            CreateMap<MarDoanhThuMarketingModel, MarDoanhThuMarketing>();
            CreateMap<MarEventMarketing, MarEventMarketingModel>();
            CreateMap<MarEventMarketingModel, MarEventMarketing>();
            CreateMap<MarMaGiamGia, MarMaGiamGiaModel>();
            CreateMap<MarMaGiamGiaModel, MarMaGiamGia>();
            CreateMap<MarMarketing, MarMarketingModel>()
                .ForMember(model => model.HinhThucMarketingString, o => o.MapFrom(e => EngineContext.Current.Resolve<ILocalizationService>().GetLocalizedEnum(e.HinhThucMarketing, null)));
            CreateMap<MarMarketingModel, MarMarketing>();
            CreateMap<MarMarketingDieuKien, MarMarketingDieuKienModel>();
            CreateMap<MarMarketingDieuKienModel, MarMarketingDieuKien>();
            CreateMap<MarMarketingHeThong, MarMarketingHeThongModel>();
            CreateMap<MarMarketingHeThongModel, MarMarketingHeThong>();
            CreateMap<MarMarketingHeThongMap, MarMarketingHeThongMapModel>();
            CreateMap<MarMarketingHeThongMapModel, MarMarketingHeThongMap>();
            CreateMap<StoreModel, Store>();
            CreateMap<Store, StoreModel>();
            CreateMap<TaiKhoanModel, Customer>();
            CreateMap<Customer, TaiKhoanModel>();
        }
        #endregion
        #region Properties

        /// <summary>
        /// Order of this mapper implementation
        /// </summary>
        public int Order => 1;

        #endregion
    }
}
