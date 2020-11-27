using Microsoft.AspNetCore.Mvc.Filters;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Api.Infrastructure
{
    public class LoggingFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (EngineContext.Current.Resolve<NopConfig>().IsWriteLogFile)
            {
                var logger = EngineContext.Current.Resolve<IApiLog>();
                logger.Trace(context.HttpContext.Request, context.ActionDescriptor.DisplayName, context.ActionArguments);
            }
            base.OnActionExecuting(context);
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }
    }
}
