using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Security;
using Nop.Services.Messages;


namespace Nop.Web.Areas.CRM.Controllers
{
    public class SecurityController: BaseCRMController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public SecurityController(
            ILocalizationService localizationService,
            ILogger logger,
            INotificationService notificationService,
            IPermissionService permissionService,
            IWorkContext workContext)
        {
            _localizationService = localizationService;
            _logger = logger;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _workContext = workContext;
        }

        #endregion
        public virtual IActionResult AccessDenied(string pageUrl)
        {
            var currentCustomer = _workContext.CurrentCustomer;
            if (currentCustomer == null)
            {
                _logger.Information($"Access denied to anonymous request on {pageUrl}");
                return View();
            }

            _logger.Information($"Access denied to user #{currentCustomer.Email} '{currentCustomer.Email}' on {pageUrl}");

            return View();
        }
    }
}
