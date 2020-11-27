using FluentValidation;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using Nop.Web.Areas.Admin.Models.Customers;
using Nop.Web.Framework.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.CRM.Validators.CRM
{
    public class TaiKhoanValidator:BaseNopValidator<CustomerModel>
    {
        public TaiKhoanValidator(ICustomerService customerService)
        {
            //Kiểm duyệt dữ liệu trống 
            RuleFor(c => c.Email).NotEmpty().WithMessage("Địa chỉ Email không được để trống!!");
            //RuleFor(c => c.Password).NotEmpty().WithMessage("Địa chỉ Email không được để trống!!");
            
            //ensure that valid email address is entered if Registered role is checked to avoid registered customers with empty email address
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                //.WithMessage("Valid Email is required for customer to be in 'Registered' role")
                .WithMessage("Địa chỉ Email không chính xác")
                //only for registered users
                .When(x => IsRegisteredCustomerRoleChecked(x, customerService));

        }
        private bool IsRegisteredCustomerRoleChecked(CustomerModel model, ICustomerService customerService)
        {
            var allCustomerRoles = customerService.GetAllCustomerRoles(true);
            var newCustomerRoles = new List<CustomerRole>();
            foreach (var customerRole in allCustomerRoles)
                if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                    newCustomerRoles.Add(customerRole);

            var isInRegisteredRole = newCustomerRoles.FirstOrDefault(cr => cr.SystemName == NopCustomerDefaults.RegisteredRoleName) != null;
            return isInRegisteredRole;
        }

    }
}
