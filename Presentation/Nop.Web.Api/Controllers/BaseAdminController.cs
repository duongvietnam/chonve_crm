using Microsoft.AspNetCore.Authorization;
using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nop.Web.Api.Controllers
{
    [Authorize]
    public class BaseAdminController: BaseApiController
    {
        private CustomerApp _currentCustomer = null;

        protected CustomerApp currentCustomer
        {
            get
            {
                if (_currentCustomer != null)
                    return _currentCustomer;
                _currentCustomer = new CustomerApp();
                //Id
                var val = this.User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).First().Value;
                _currentCustomer.Id = !string.IsNullOrEmpty(val) ? Convert.ToInt32(val) : 0;
                //NhanVienId
                val = this.User.Claims.Where(c => c.Type == ClaimTypes.PrimaryGroupSid).First().Value;
                _currentCustomer.NhanVienId = !string.IsNullOrEmpty(val) ? Convert.ToInt32(val) : 0;
                //StoreId
                val = this.User.Claims.Where(c => c.Type == ClaimTypes.GroupSid).First().Value;
                _currentCustomer.StoreId = !string.IsNullOrEmpty(val) ? Convert.ToInt32(val) : 0;
                //CustomerGuid
                val = this.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;
                if (!string.IsNullOrEmpty(val))
                    _currentCustomer.CustomerGuid = new Guid(val);

                _currentCustomer.Email = this.User.Claims.Where(c => c.Type == ClaimTypes.Email).First().Value;
                _currentCustomer.Fullname = this.User.Claims.Where(c => c.Type == ClaimTypes.Name).First().Value;
                return _currentCustomer;
            }
        }
    }
}
