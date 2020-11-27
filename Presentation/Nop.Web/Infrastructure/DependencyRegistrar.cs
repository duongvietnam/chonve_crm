using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Services.CRM;
using Nop.Services.DanhMuc;
using Nop.Services.HeThong;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.CRM.Factories.CRM;
using Nop.Web.Areas.CRM.Factory.CRM;
using Nop.Web.Framework.Factories;
using Nop.Web.Infrastructure.Installation;

namespace Nop.Web.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            #region Nop register
            //installation localization service
            builder.RegisterType<InstallationLocalizationService>().As<IInstallationLocalizationService>().InstancePerLifetimeScope();

            //common factories
            builder.RegisterType<AclSupportedModelFactory>().As<IAclSupportedModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DiscountSupportedModelFactory>().As<IDiscountSupportedModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<LocalizedModelFactory>().As<ILocalizedModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<StoreMappingSupportedModelFactory>().As<IStoreMappingSupportedModelFactory>().InstancePerLifetimeScope();

            //admin factories
            builder.RegisterType<BaseAdminModelFactory>().As<IBaseAdminModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ActivityLogModelFactory>().As<IActivityLogModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<AddressAttributeModelFactory>().As<IAddressAttributeModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<AffiliateModelFactory>().As<IAffiliateModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<BlogModelFactory>().As<IBlogModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<CampaignModelFactory>().As<ICampaignModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryModelFactory>().As<ICategoryModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<CheckoutAttributeModelFactory>().As<ICheckoutAttributeModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<CommonModelFactory>().As<ICommonModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<CountryModelFactory>().As<ICountryModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<CurrencyModelFactory>().As<ICurrencyModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerAttributeModelFactory>().As<ICustomerAttributeModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerModelFactory>().As<ICustomerModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerRoleModelFactory>().As<ICustomerRoleModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DiscountModelFactory>().As<IDiscountModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<EmailAccountModelFactory>().As<IEmailAccountModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ExternalAuthenticationMethodModelFactory>().As<IExternalAuthenticationMethodModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ForumModelFactory>().As<IForumModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<GiftCardModelFactory>().As<IGiftCardModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<HomeModelFactory>().As<IHomeModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<LanguageModelFactory>().As<ILanguageModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<LogModelFactory>().As<ILogModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ManufacturerModelFactory>().As<IManufacturerModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<MeasureModelFactory>().As<IMeasureModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<MessageTemplateModelFactory>().As<IMessageTemplateModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<NewsletterSubscriptionModelFactory>().As<INewsletterSubscriptionModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<NewsModelFactory>().As<INewsModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<OrderModelFactory>().As<IOrderModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentModelFactory>().As<IPaymentModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<PluginModelFactory>().As<IPluginModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<PollModelFactory>().As<IPollModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ProductModelFactory>().As<IProductModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ProductAttributeModelFactory>().As<IProductAttributeModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ProductReviewModelFactory>().As<IProductReviewModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ReportModelFactory>().As<IReportModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<QueuedEmailModelFactory>().As<IQueuedEmailModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<RecurringPaymentModelFactory>().As<IRecurringPaymentModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ReturnRequestModelFactory>().As<IReturnRequestModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ReviewTypeModelFactory>().As<IReviewTypeModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ScheduleTaskModelFactory>().As<IScheduleTaskModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<SecurityModelFactory>().As<ISecurityModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<SettingModelFactory>().As<ISettingModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ShippingModelFactory>().As<IShippingModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ShoppingCartModelFactory>().As<IShoppingCartModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<SpecificationAttributeModelFactory>().As<ISpecificationAttributeModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<StoreModelFactory>().As<IStoreModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaxModelFactory>().As<ITaxModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TemplateModelFactory>().As<ITemplateModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TopicModelFactory>().As<ITopicModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<VendorAttributeModelFactory>().As<IVendorAttributeModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<VendorModelFactory>().As<IVendorModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<WidgetModelFactory>().As<IWidgetModelFactory>().InstancePerLifetimeScope();

            //factories
            builder.RegisterType<Factories.AddressModelFactory>().As<Factories.IAddressModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.BlogModelFactory>().As<Factories.IBlogModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.CatalogModelFactory>().As<Factories.ICatalogModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.CheckoutModelFactory>().As<Factories.ICheckoutModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.CommonModelFactory>().As<Factories.ICommonModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.CountryModelFactory>().As<Factories.ICountryModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.CustomerModelFactory>().As<Factories.ICustomerModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.ForumModelFactory>().As<Factories.IForumModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.ExternalAuthenticationModelFactory>().As<Factories.IExternalAuthenticationModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.NewsModelFactory>().As<Factories.INewsModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.NewsletterModelFactory>().As<Factories.INewsletterModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.OrderModelFactory>().As<Factories.IOrderModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.PollModelFactory>().As<Factories.IPollModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.PrivateMessagesModelFactory>().As<Factories.IPrivateMessagesModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.ProductModelFactory>().As<Factories.IProductModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.ProfileModelFactory>().As<Factories.IProfileModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.ReturnRequestModelFactory>().As<Factories.IReturnRequestModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.ShoppingCartModelFactory>().As<Factories.IShoppingCartModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.TopicModelFactory>().As<Factories.ITopicModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.VendorModelFactory>().As<Factories.IVendorModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<Factories.WidgetModelFactory>().As<Factories.IWidgetModelFactory>().InstancePerLifetimeScope();
            #endregion
            #region Danh muc
            builder.RegisterType<BankService>().As<IBankService>().InstancePerLifetimeScope();
            #endregion
            #region Danh muc Factory

            #endregion
            #region He thong
            builder.RegisterType<WorkfilesService>().As<IWorkfilesService>().InstancePerLifetimeScope();
            #endregion
            #region CRM Service
            builder.RegisterType<CdChuyenDiService>().As<ICdChuyenDiService>().InstancePerLifetimeScope();
            builder.RegisterType<ChGiaTriThongTinMoRongService>().As<IChGiaTriThongTinMoRongService>().InstancePerLifetimeScope();
            builder.RegisterType<ChPhanHangKhachHangService>().As<IChPhanHangKhachHangService>().InstancePerLifetimeScope();
            builder.RegisterType<ChThongTinMoRongService>().As<IChThongTinMoRongService>().InstancePerLifetimeScope();
            builder.RegisterType<DmSuKienService>().As<IDmSuKienService>().InstancePerLifetimeScope();
            builder.RegisterType<DvDichVuService>().As<IDvDichVuService>().InstancePerLifetimeScope();
            builder.RegisterType<DvDichVuComboService>().As<IDvDichVuComboService>().InstancePerLifetimeScope();
            builder.RegisterType<DvDieuChinhGiaService>().As<IDvDieuChinhGiaService>().InstancePerLifetimeScope();
            builder.RegisterType<DvGiaDichVuService>().As<IDvGiaDichVuService>().InstancePerLifetimeScope();
            builder.RegisterType<DvLoaiDichVuService>().As<IDvLoaiDichVuService>().InstancePerLifetimeScope();
            builder.RegisterType<DvNhatKyGiaService>().As<IDvNhatKyGiaService>().InstancePerLifetimeScope();
            builder.RegisterType<DvNhomDichVuService>().As<IDvNhomDichVuService>().InstancePerLifetimeScope();
            builder.RegisterType<DvDonViTinhService>().As<IDvDonViTinhService>().InstancePerLifetimeScope();
            builder.RegisterType<GdGiaoDichService>().As<IGdGiaoDichService>().InstancePerLifetimeScope();
            builder.RegisterType<GdGiaoDichDanhGiaService>().As<IGdGiaoDichDanhGiaService>().InstancePerLifetimeScope();
            builder.RegisterType<GdGiaoDichDiemService>().As<IGdGiaoDichDiemService>().InstancePerLifetimeScope();
            builder.RegisterType<GdGiaoDichKhachHangMapService>().As<IGdGiaoDichKhachHangMapService>().InstancePerLifetimeScope();
            builder.RegisterType<GdGiaoDichSubService>().As<IGdGiaoDichSubService>().InstancePerLifetimeScope();
            builder.RegisterType<KhCauHinhMoRongService>().As<IKhCauHinhMoRongService>().InstancePerLifetimeScope();
            builder.RegisterType<KhDanhBaDienThoaiService>().As<IKhDanhBaDienThoaiService>().InstancePerLifetimeScope();
            builder.RegisterType<KhKhachHangService>().As<IKhKhachHangService>().InstancePerLifetimeScope();
            builder.RegisterType<KhNhomHeThongService>().As<IKhNhomHeThongService>().InstancePerLifetimeScope();
            builder.RegisterType<KhNhomHeThongMapService>().As<IKhNhomHeThongMapService>().InstancePerLifetimeScope();
            builder.RegisterType<KhNhomKhachHangService>().As<IKhNhomKhachHangService>().InstancePerLifetimeScope();
            builder.RegisterType<KhNhomKhachHangMapService>().As<IKhNhomKhachHangMapService>().InstancePerLifetimeScope();
            builder.RegisterType<KhThongKeSuDungDichVuService>().As<IKhThongKeSuDungDichVuService>().InstancePerLifetimeScope();
            builder.RegisterType<KhThongTinMoRongService>().As<IKhThongTinMoRongService>().InstancePerLifetimeScope();
            builder.RegisterType<MarDoanhThuMarketingService>().As<IMarDoanhThuMarketingService>().InstancePerLifetimeScope();
            builder.RegisterType<MarEventMarketingService>().As<IMarEventMarketingService>().InstancePerLifetimeScope();
            builder.RegisterType<MarMaGiamGiaService>().As<IMarMaGiamGiaService>().InstancePerLifetimeScope();
            builder.RegisterType<MarMarketingService>().As<IMarMarketingService>().InstancePerLifetimeScope();
            builder.RegisterType<MarMarketingDieuKienService>().As<IMarMarketingDieuKienService>().InstancePerLifetimeScope();
            builder.RegisterType<MarMarketingHeThongService>().As<IMarMarketingHeThongService>().InstancePerLifetimeScope();
            builder.RegisterType<MarMarketingHeThongMapService>().As<IMarMarketingHeThongMapService>().InstancePerLifetimeScope();
            #endregion
            #region CRM Factory
            builder.RegisterType<CdChuyenDiModelFactory>().As<ICdChuyenDiModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ChGiaTriThongTinMoRongModelFactory>().As<IChGiaTriThongTinMoRongModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ChPhanHangKhachHangModelFactory>().As<IChPhanHangKhachHangModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ChThongTinMoRongModelFactory>().As<IChThongTinMoRongModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DmSuKienModelFactory>().As<IDmSuKienModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DvDichVuModelFactory>().As<IDvDichVuModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DvDichVuComboModelFactory>().As<IDvDichVuComboModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DvDieuChinhGiaModelFactory>().As<IDvDieuChinhGiaModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DvGiaDichVuModelFactory>().As<IDvGiaDichVuModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DvLoaiDichVuModelFactory>().As<IDvLoaiDichVuModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DvNhatKyGiaModelFactory>().As<IDvNhatKyGiaModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DvNhomDichVuModelFactory>().As<IDvNhomDichVuModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DvDonViTinhModelFactory>().As<IDvDonViTinhModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<GdGiaoDichModelFactory>().As<IGdGiaoDichModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<GdGiaoDichDanhGiaModelFactory>().As<IGdGiaoDichDanhGiaModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<GdGiaoDichDiemModelFactory>().As<IGdGiaoDichDiemModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<GdGiaoDichKhachHangMapModelFactory>().As<IGdGiaoDichKhachHangMapModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<GdGiaoDichSubModelFactory>().As<IGdGiaoDichSubModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<KhCauHinhMoRongModelFactory>().As<IKhCauHinhMoRongModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<KhDanhBaDienThoaiModelFactory>().As<IKhDanhBaDienThoaiModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<KhKhachHangModelFactory>().As<IKhKhachHangModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<KhNhomHeThongModelFactory>().As<IKhNhomHeThongModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<KhNhomHeThongMapModelFactory>().As<IKhNhomHeThongMapModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<KhNhomKhachHangModelFactory>().As<IKhNhomKhachHangModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<KhNhomKhachHangMapModelFactory>().As<IKhNhomKhachHangMapModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<KhThongKeSuDungDichVuModelFactory>().As<IKhThongKeSuDungDichVuModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<KhThongTinMoRongModelFactory>().As<IKhThongTinMoRongModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<MarDoanhThuMarketingModelFactory>().As<IMarDoanhThuMarketingModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<MarEventMarketingModelFactory>().As<IMarEventMarketingModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<MarMaGiamGiaModelFactory>().As<IMarMaGiamGiaModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<MarMarketingModelFactory>().As<IMarMarketingModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<MarMarketingDieuKienModelFactory>().As<IMarMarketingDieuKienModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<MarMarketingHeThongModelFactory>().As<IMarMarketingHeThongModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<MarMarketingHeThongMapModelFactory>().As<IMarMarketingHeThongMapModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DoanhNghiepModelFactory>().As<IDoanhNghiepModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TaiKhoanModelFactory>().As<ITaiKhoanModelFactory>().InstancePerLifetimeScope();
            #endregion
        }

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order => 2;
    }
}
