using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Web.Api.Models.Customer
{
    public partial class PasswordRecoveryConfirmModel : BaseNopModel
    {
        public string NewPassword { get; set; }
        
        public string ConfirmNewPassword { get; set; }
    }
}