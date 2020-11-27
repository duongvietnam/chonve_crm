using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Stores;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Customers;
using Nop.Web.Areas.CRM.Models.CRM;
using Nop.Web.Framework.Events;
using Nop.Web.Framework.Factories;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Nop.Web.Areas.CRM.Models.CRM.TaiKhoanModel;

namespace Nop.Web.Areas.CRM.Factory.CRM
{
    public class TaiKhoanModelFactory : ITaiKhoanModelFactory
    {
        #region Fields
        private readonly ICustomerService _customerService;
        private readonly IAclSupportedModelFactory _aclSupportedModelFactory;
        private readonly IStoreService _storeService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _customerContext;

        #endregion
        #region Ctor
        public TaiKhoanModelFactory(ICustomerService customerService,
            IAclSupportedModelFactory aclSupportedModelFactory,
            IStoreService storeService,
            IBaseAdminModelFactory baseAdminModelFactory,
            ILocalizationService localizationService,
            IGenericAttributeService genericAttributeService,
            IDateTimeHelper dateTimeHelper,
            IStoreContext storeContext,
            IWorkContext customerContext)
        {
            _customerService = customerService;
            _aclSupportedModelFactory = aclSupportedModelFactory;
            _storeService = storeService;
            _baseAdminModelFactory = baseAdminModelFactory;
            _localizationService = localizationService;
            _genericAttributeService = genericAttributeService;
            _dateTimeHelper = dateTimeHelper;
            _storeContext = storeContext;
            _customerContext = customerContext;
        }
        #endregion
        #region Method
        /// <summary>
        /// Prepare customer search model
        /// </summary>
        /// <param name="searchModel">Customer search model</param>
        /// <returns>Customer search model</returns>
        public virtual TaiKhoanSearchModel PrepareCustomerSearchModel(TaiKhoanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            //search registered customers by default
            var registeredRole = _customerService.GetCustomerRoleBySystemName(NopCustomerDefaults.RegisteredRoleName);
            if (registeredRole != null)
                searchModel.SelectedCustomerRoleIds.Add(registeredRole.Id);

            //prepare available customer roles
            _aclSupportedModelFactory.PrepareModelCustomerRoles(searchModel);
            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }

        /// <summary>
        /// Prepare paged customer list model
        /// </summary>
        /// <param name="searchModel">Customer search model</param>
        /// <returns>Customer list model</returns>
        public virtual TaiKhoanListModel PrepareCustomerListModel(TaiKhoanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get customers
            var customers = _customerService.GetAllCustomers(customerRoleIds: searchModel.SelectedCustomerRoleIds.ToArray(),
                email: searchModel.SearchEmail,
                username: searchModel.SearchUsername,
                //fullName: searchModel.SearchFullName,
                phone: searchModel.SearchPhone,
                ipAddress: searchModel.SearchIpAddress,
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize,
                idStore: _storeContext.CurrentStore.Id);
            //prepare list model
            var model = new TaiKhoanListModel().PrepareToGrid(searchModel, customers, () =>
            {
                return customers.Select(customer =>
                {
                    //fill in model values from the entity
                    var customerModel = customer.ToModel<TaiKhoanModel>();

                    //convert dates to the user time
                    customerModel.Email = _customerService.IsRegistered(customer) ? customer.Email : _localizationService.GetResource("Admin.Customers.Guest");
                    customerModel.FullName = _customerService.GetCustomerFullName(customer);
                    customerModel.Phone = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.PhoneAttribute);
                    customerModel.CreatedOn = _dateTimeHelper.ConvertToUserTime(customer.CreatedOnUtc, DateTimeKind.Utc);
                    customerModel.LastActivityDate = _dateTimeHelper.ConvertToUserTime(customer.LastActivityDateUtc, DateTimeKind.Utc);

                    //fill in additional values (not existing in the entity)
                    customerModel.CustomerRoleNames = string.Join(", ", _customerService.GetCustomerRoles(customer).Select(role => role.Name));
                    return customerModel;
                });
            });

            return model;
        }

        /// <summary>
        /// Prepare customer model
        /// </summary>
        /// <param name="model">Customer model</param>
        /// <param name="customer">Customer</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>Customer model</returns>
        public virtual TaiKhoanModel PrepareCustomerModel(TaiKhoanModel model, Customer customer, bool excludeProperties = false)
        {
            if (customer != null)
            {
                //fill in model values from the entity
                model ??= new TaiKhoanModel();

                model.Id = customer.Id;
                //whether to fill in some of properties
                if (!excludeProperties)
                {
                    model.Email = customer.Email;
                    model.Username = customer.Username;
                    model.Active = customer.Active;
                    model.Gender = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.GenderAttribute);
                    model.Phone = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.PhoneAttribute);
                    model.CreatedOn = _dateTimeHelper.ConvertToUserTime(customer.CreatedOnUtc, DateTimeKind.Utc);
                    model.LastActivityDate = _dateTimeHelper.ConvertToUserTime(customer.LastActivityDateUtc, DateTimeKind.Utc);
                    model.LastIpAddress = customer.LastIpAddress;
                    model.LastVisitedPage = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.LastVisitedPageAttribute);
                    model.SelectedCustomerRoleIds = _customerService.GetCustomerRoleIds(customer).ToList();
                    model.FirstName = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.FirstNameAttribute);
                    model.LastName = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.LastNameAttribute);
                    model.FullName = _customerService.GetCustomerFullName(customer);
                    //model.RegisteredInStore = _storeService.GetAllStores()
                    //    .FirstOrDefault(store => store.Id == customer.RegisteredInStoreId)?.Name ?? string.Empty;
                    //model.DisplayRegisteredInStore = model.Id > 0 && !string.IsNullOrEmpty(model.RegisteredInStore) &&
                    //    _storeService.GetAllStores().Select(x => x.Id).Count() > 1;
                }
            }
            else
            {
                //whether to fill in some of properties
                if (!excludeProperties)
                {
                    //precheck Registered Role as a default role while creating a new customer through admin
                    var registeredRole = _customerService.GetCustomerRoleBySystemName(NopCustomerDefaults.RegisteredRoleName);
                    if (registeredRole != null)
                        model.SelectedCustomerRoleIds.Add(registeredRole.Id);
                }
            }
            //set default values for the new model
            if (customer == null)
            {
                model.Active = true;
                model.Gender = "M";
            }
            //prepare model customer roles
            _aclSupportedModelFactory.PrepareModelCustomerRoles(model);


            return model;
        }

        public virtual TaiKhoanModel PrepareAdminModel(TaiKhoanModel model)
        {
            //whether to fill in some of properties
            //precheck Registered Role as a default role while creating a new customer through admin
            var AdminRole = _customerService.GetCustomerRoleBySystemName("AdminStore");
            var CRMRoles = _customerService.GetAllCustomerRoles().Where(r =>
            {
                char[] chs = { r.SystemName[0], r.SystemName[1], r.SystemName[2] };
                string s = new string(chs);
                return s.Equals("CRM");
            }).ToList();
            if (AdminRole != null)
                model.SelectedCustomerRoleIds.Add(AdminRole.Id);
            foreach (var role in CRMRoles)
            {
                model.SelectedCustomerRoleIds.Add(role.Id);
            }
            //prepare model customer roles
            _aclSupportedModelFactory.PrepareModelCustomerRoles(model);
            return model;
        }
        public virtual TaiKhoanModel PrepareStoresCustomerModel(TaiKhoanModel model)
        {
            //whether to fill in some of properties
            //precheck Registered Role as a default role while creating a new customer through admin
            var CurrentUser = _customerContext.CurrentCustomer;
            var CCRMRoles = _customerService.GetCustomerRoles(CurrentUser).Where(r =>
            {
                char[] chs = { r.SystemName[0], r.SystemName[1], r.SystemName[2] };
                string s = new string(chs);
                return s.Equals("CRM");
            });
            //prepare model customer roles
            if (CCRMRoles != null && CCRMRoles.Any())
            {
                model.AvailableCustomerRoles = CCRMRoles.Select(role => new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Id.ToString(),
                    Selected = model.SelectedCustomerRoleIds.Contains(role.Id)
                }).ToList();
                //precheck Registered Role as a default role while creating a new customer through admin
                var registeredRole = _customerService.GetCustomerRoleBySystemName(NopCustomerDefaults.RegisteredRoleName);
                model.AvailableCustomerRoles.Add(new SelectListItem
                {
                    Text = registeredRole.Name,
                    Value = registeredRole.Id.ToString(),
                    Selected = model.SelectedCustomerRoleIds.Contains(registeredRole.Id)
                });
            }
            if (model.SelectedCustomerRoleIds != null && model.SelectedCustomerRoleIds.Any())
            {
                var Roles = _customerService.GetAllCustomerRoles().Where(r => model.SelectedCustomerRoleIds.Contains(r.Id));
                foreach (var role in Roles)
                {
                    var aRole = new SelectListItem
                    {
                        Text = role.Name,
                        Value = role.Id.ToString(),
                        Selected = model.SelectedCustomerRoleIds.Contains(role.Id)
                    };
                    if (model.AvailableCustomerRoles?.FirstOrDefault(r => r.Value.Equals(aRole.Value))==null) model.AvailableCustomerRoles.Add(aRole);
                }
            }
            return model;
        }
        #endregion
    }
}
