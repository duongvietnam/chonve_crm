using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.CRM.Controllers
{
    public class WorkController: BaseCRMController
    {
        #region Fields
        private readonly IEventPublisher _eventPublisher;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly INotificationService _notificationService;
        #endregion

        #region Ctor
        public WorkController(
            INotificationService notificationService,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService,
            IWorkContext workContext
            )
        {
            this._notificationService = notificationService;
            this._eventPublisher = eventPublisher;
            this._localizationService = localizationService;
            this._workContext = workContext;
        }
        #endregion
        public virtual IActionResult Index()
        {
            return View();
        }
    }
}
