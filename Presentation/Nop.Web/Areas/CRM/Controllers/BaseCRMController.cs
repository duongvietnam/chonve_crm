using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Nop.Core.Domain.Common;
using Nop.Core.Infrastructure;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Web.Areas.CRM.Controllers
{
    [Area(AreaNames.CRM)]
    [AutoValidateAntiforgeryToken]
    [ValidateIpAddress]
    [AuthorizeCRM]
    //[SaveSelectedTab]
    public abstract partial class BaseCRMController : BaseController
    {
        /// <summary>
        /// Creates an object that serializes the specified object to JSON.
        /// </summary>
        /// <param name="data">The object to serialize.</param>
        /// <returns>The created object that serializes the specified data to JSON format for the response.</returns>
        public override JsonResult Json(object data)
        {
            //use IsoDateFormat on writing JSON text to fix issue with dates in grid
            var useIsoDateFormat = EngineContext.Current.Resolve<AdminAreaSettings>()?.UseIsoDateFormatInJsonResult ?? false;
            var serializerSettings = EngineContext.Current.Resolve<IOptions<MvcNewtonsoftJsonOptions>>()?.Value?.SerializerSettings
                ?? new JsonSerializerSettings();

            if (!useIsoDateFormat)
                return base.Json(data, serializerSettings);

            serializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            serializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Unspecified;

            return base.Json(data, serializerSettings);
        }
        protected virtual IActionResult HomeView(string WarningMsg = null)
        {
            if (!string.IsNullOrEmpty(WarningMsg))
            {
                //var notificationService = EngineContext.Current.Resolve<INotificationService>();
                //notificationService.WarningNotification(WarningMsg);
            }
            return RedirectToAction("Index", "Home");
        }
        protected virtual IActionResult BackPageView(string WarningMsg = null, string pageName = "List")
        {
            if (!string.IsNullOrEmpty(WarningMsg))
            {
                //var notificationService = EngineContext.Current.Resolve<INotificationService>();
                //notificationService.WarningNotification(WarningMsg);
            }
            return RedirectToAction(pageName);
        }


    }
}