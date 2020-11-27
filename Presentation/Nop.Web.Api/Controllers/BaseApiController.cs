using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Common;
using Nop.Web.Api.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Api.Controllers
{
    [LoggingFilter]
    public class BaseApiController: ControllerBase
    {
        #region JsonMessageData return
        protected virtual IActionResult OkSuccessMessage(string msg = "Ok", object objdata = null)
        {
            return Ok(MessageReturn.CreateSuccessMessage(msg, objdata));
        }
        protected virtual IActionResult OkErrorMessage(string msg = "Error", object objdata = null)
        {
            return Ok(MessageReturn.CreateErrorMessage(msg, objdata));
        }
        protected virtual IActionResult OkNotFoundMessage(string msg = "Not Found", object objdata = null)
        {
            return Ok(MessageReturn.CreateErrorMessage(msg, objdata));
        }
        protected IActionResult NullModel()
        {
            return Ok(MessageReturn.CreateErrorMessage("Input is not correct"));
        }
        #endregion
    }
}
