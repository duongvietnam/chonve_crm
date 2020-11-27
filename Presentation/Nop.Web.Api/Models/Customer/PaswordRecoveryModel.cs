using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Web.Api.Models.Customer
{
    public partial class PasswordRecoveryModel : BaseNopModel
    {
        public string Email { get; set; }

    }
}