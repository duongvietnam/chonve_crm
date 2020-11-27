using Nop.Web.Areas.Admin.Models.Customers;
using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.CRM.Models.Common
{
    public class ProfileModel
    {
        public ProfileModel()
        {
            customerModel = new CustomerModel();
        }
        public CustomerModel customerModel { get; set; }
        public string TabNameFocus { get; set; }
    }
    public class PasswordChangeModel : BaseNopModel
    {

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmNewPassword { get; set; }

        public bool IsFirstTime { get; set; }
    }
}
