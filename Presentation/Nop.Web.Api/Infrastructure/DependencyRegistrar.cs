using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Services.DanhMuc;
using Nop.Services.HeThong;
using Nop.Web.Framework.Factories;

namespace Nop.Web.Api.Infrastructure
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
            builder.RegisterType<NLogger>().As<IApiLog>().InstancePerLifetimeScope();
            //common factories
            builder.RegisterType<AclSupportedModelFactory>().As<IAclSupportedModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DiscountSupportedModelFactory>().As<IDiscountSupportedModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<LocalizedModelFactory>().As<ILocalizedModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<StoreMappingSupportedModelFactory>().As<IStoreMappingSupportedModelFactory>().InstancePerLifetimeScope();

            #endregion

            #region Danh muc
            builder.RegisterType<BankService>().As<IBankService>().InstancePerLifetimeScope();

            #endregion
            #region He thong
            builder.RegisterType<WorkfilesService>().As<IWorkfilesService>().InstancePerLifetimeScope();

            #endregion
            #region CRM Register

            #endregion
        }

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order => 2;
    }
}
