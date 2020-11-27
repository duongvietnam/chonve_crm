using Microsoft.AspNetCore.Mvc;
using Nop.Core;

namespace Nop.Web.Controllers
{
    public partial class HomeController : BasePublicController
    {
        private readonly IWorkContext _workContext;
        public HomeController(
            IWorkContext workContext
            )
        {
            this._workContext = workContext;
        }
        public virtual IActionResult Index()
        {
            if (!string.IsNullOrEmpty(_workContext.CurrentCustomer.Email))
                return Redirect("/CRM/");
            return RedirectToRoute("Login");
        }
        [HttpPost]
        public virtual IActionResult Test()
        {
            return this.JsonSuccessMessage();
        }
    }
}