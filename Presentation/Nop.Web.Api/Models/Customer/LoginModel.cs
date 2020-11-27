using System.ComponentModel.DataAnnotations;
using Nop.Core.Domain.Customers;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Web.Api.Models.Customer
{
    public partial class LoginModel 
    {
        public string Username { get; set; }        
        public string Password { get; set; }
        public string StoreKey { get; set; }
        public string AppId { get; set; }

    }
}