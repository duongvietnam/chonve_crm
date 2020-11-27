using Nop.Core.Domain.Stores;
using Nop.Services.Localization;
using Nop.Services.Stores;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
//using Nop.Web.Areas.CRM.Infrastructure.Mapper;
using Nop.Web.Areas.CRM.Models.CRM;
using Nop.Web.Framework.Factories;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.CRM.Factory.CRM
{
    public class DoanhNghiepModelFactory:IDoanhNghiepModelFactory
    {
        #region Fields

        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedModelFactory _localizedModelFactory;
        private readonly IStoreService _storeService;

        #endregion

        #region Ctor

        public DoanhNghiepModelFactory(IBaseAdminModelFactory baseAdminModelFactory,
            ILocalizationService localizationService,
            ILocalizedModelFactory localizedModelFactory,
            IStoreService storeService)
        {
            _baseAdminModelFactory = baseAdminModelFactory;
            _localizationService = localizationService;
            _localizedModelFactory = localizedModelFactory;
            _storeService = storeService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare store search model
        /// </summary>
        /// <param name="searchModel">Store search model</param>
        /// <returns>Store search model</returns>
        public virtual StoreSearchModel PrepareStoreSearchModel(StoreSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }

        /// <summary>
        /// Prepare paged store list model
        /// </summary>
        /// <param name="searchModel">Store search model</param>
        /// <returns>Store list model</returns>
        public virtual StoreListModel PrepareStoreListModel(StoreSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get stores
            var stores = _storeService.GetAllStores().ToPagedList(searchModel);

            //prepare list model
            var model = new StoreListModel().PrepareToGrid(searchModel, stores, () =>
            {
                //fill in model values from the entity
                return stores.Select(store => store.ToModel<StoreModel>());
            });

            return model;
        }

        /// <summary>
        /// Prepare store model
        /// </summary>
        /// <param name="model">Store model</param>
        /// <param name="store">Store</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>Store model</returns>
        public virtual StoreModel PrepareStoreModel(StoreModel model, Store store, bool excludeProperties = false)
        {
            Action<StoreLocalizedModel, int> localizedModelConfiguration = null;

            if (store != null)
            {
                //fill in model values from the entity
                model ??= store.ToModel<StoreModel>();

                //define localized model configuration action
                localizedModelConfiguration = (locale, languageId) =>
                {
                    locale.Name = _localizationService.GetLocalized(store, entity => entity.Name, languageId, false, false);
                };
            }

            //prepare available languages
            _baseAdminModelFactory.PrepareLanguages(model.AvailableLanguages,
                defaultItemText: _localizationService.GetResource("Admin.Common.EmptyItemText"));
            //prepare default language 
            model.DefaultLanguageId = 2;
            //prepare localized models
            if (!excludeProperties)
                model.Locales = _localizedModelFactory.PrepareLocalizedModels(localizedModelConfiguration);
            return model;
        }
        /// <summary>
        /// Chuyen doi store Model sang store Entity
        /// </summary>
        /// <param name="model"></param>
        /// <param name="store"></param>
        public virtual void PrepareStore(StoreModel model, Store entity)
        {
            entity.Name = model.Name;
            entity.CompanyFullName = model.CompanyFullName;
            entity.CompanyName = model.CompanyName;
            entity.CompanyAddress = model.CompanyAddress;
            entity.CompanyPhoneNumber = model.CompanyPhoneNumber;
            entity.Url = model.Url;
            entity.SslEnabled = model.SslEnabled;
            entity.CompanyVat = model.CompanyVat;
            entity.DefaultLanguageId = model.DefaultLanguageId;
            entity.DisplayOrder = model.DisplayOrder;
            entity.AccessKey = model.AccessKey;
            entity.Hosts = model.Hosts;
            entity.DomainLogin = model.DomainLogin;
        }
        #endregion
    }
}
