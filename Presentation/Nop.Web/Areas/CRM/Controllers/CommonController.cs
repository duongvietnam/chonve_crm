using System;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Security;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Models.Common;

namespace Nop.Web.Areas.CRM.Controllers
{
    [AutoValidateAntiforgeryToken]
    public partial class CommonController : BaseCRMController
    {
        #region Fields

        private readonly CaptchaSettings _captchaSettings;
        private readonly CommonSettings _commonSettings;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly LocalizationSettings _localizationSettings;
        private readonly StoreInformationSettings _storeInformationSettings;

        #endregion

        #region Ctor

        public CommonController(CaptchaSettings captchaSettings,
            CommonSettings commonSettings,
            ILocalizationService localizationService,
            IWorkContext workContext,
            LocalizationSettings localizationSettings,
            StoreInformationSettings storeInformationSettings
            )
        {
            _captchaSettings = captchaSettings;
            _commonSettings = commonSettings;
            _localizationService = localizationService;
            _workContext = workContext;
            _localizationSettings = localizationSettings;
            _storeInformationSettings = storeInformationSettings;
        }

        #endregion

        #region Methods

        //page not found
        public virtual IActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            Response.ContentType = "text/html";

            return View();
        }

        #endregion
    }
}