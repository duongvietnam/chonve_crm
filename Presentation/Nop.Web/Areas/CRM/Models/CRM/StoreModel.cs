using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.CRM.Models.CRM
{
    
        /// <summary>
        /// Represents a store model
        /// </summary>
        public class StoreModel : BaseNopEntityModel, ILocalizedModel<StoreLocalizedModel>
        {
            #region Ctor

            public StoreModel()
            {
                Locales = new List<StoreLocalizedModel>();
                AvailableLanguages = new List<SelectListItem>();
            }

            #endregion

            #region Properties

            [NopResourceDisplayName("Admin.Configuration.Stores.Fields.Name")]
            public string Name { get; set; }

            [NopResourceDisplayName("Admin.Configuration.Stores.Fields.Url")]
            public string Url { get; set; }

            [NopResourceDisplayName("Admin.Configuration.Stores.Fields.SslEnabled")]
            public virtual bool SslEnabled { get; set; }

            [NopResourceDisplayName("Admin.Configuration.Stores.Fields.Hosts")]
            public string Hosts { get; set; }

            //default language
            [NopResourceDisplayName("Admin.Configuration.Stores.Fields.DefaultLanguage")]
            public int DefaultLanguageId { get; set; }

            public IList<SelectListItem> AvailableLanguages { get; set; }

            [NopResourceDisplayName("Admin.Configuration.Stores.Fields.DisplayOrder")]
            public int DisplayOrder { get; set; }

            [NopResourceDisplayName("Admin.Configuration.Stores.Fields.CompanyName")]
            public string CompanyName { get; set; }
            public string CompanyFullName { get; set; }

            [NopResourceDisplayName("Admin.Configuration.Stores.Fields.CompanyAddress")]
            public string CompanyAddress { get; set; }

            [NopResourceDisplayName("Admin.Configuration.Stores.Fields.CompanyPhoneNumber")]
            public string CompanyPhoneNumber { get; set; }

            [NopResourceDisplayName("Admin.Configuration.Stores.Fields.CompanyVat")]
            public string CompanyVat { get; set; }

            public IList<StoreLocalizedModel> Locales { get; set; }
            public string DomainLogin { get; set; }
            public string AccessKey { get; set; }

            #endregion
        }

        public class StoreLocalizedModel : ILocalizedLocaleModel
        {
            public int LanguageId { get; set; }

            [NopResourceDisplayName("Admin.Configuration.Stores.Fields.Name")]
            public string Name { get; set; }
        }
    /// <summary>
    /// Represents a store search model
    /// </summary>
    public class StoreSearchModel : BaseSearchModel
    {
        public string KeySearch { get; set; }
    }

    /// <summary>
    /// Represents a store list model
    /// </summary>
    public class StoreListModel : BasePagedListModel<StoreModel>
    {
    }
}
