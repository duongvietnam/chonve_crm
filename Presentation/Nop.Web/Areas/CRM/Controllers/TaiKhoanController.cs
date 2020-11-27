 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Tax;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Customers;
using Nop.Web.Areas.CRM.Factory.CRM;
using Nop.Web.Areas.CRM.Models.CRM;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework.Mvc.ModelBinding;
using static Nop.Web.Areas.CRM.Models.CRM.TaiKhoanModel;

namespace Nop.Web.Areas.CRM.Controllers
{
    public class TaiKhoanController : BaseCRMController
    {
        #region Fields
        private readonly IPermissionService _permissionService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ICustomerModelFactory _customerModelFactory;
        private readonly ICustomerService _customerService;
        private readonly CustomerSettings _customerSettings;
        private readonly DateTimeSettings _dateTimeSettings;
        private readonly ICustomerAttributeParser _customerAttributeParser;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly INotificationService _notificationService;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly ITaxService _taxService;
        private readonly IWorkContext _workContext;
        private readonly ITaiKhoanModelFactory _taikhoanModelFactory;
        private readonly TaxSettings _taxSettings;

        #endregion
        #region Ctor
        public TaiKhoanController(
            IPermissionService permissionService,
            ICustomerActivityService customerActivityService,
            ICustomerModelFactory customerModelFactory,
            ICustomerService customerService,
            CustomerSettings customerSettings,
            ICustomerAttributeService customerAttributeService,
            ILocalizationService localizationService,
            INotificationService notificationService,
            ICustomerAttributeParser customerAttributeParser,
            ICustomerRegistrationService customerRegistrationService,
            IStoreContext storeContext,
            IStoreService storeService,
            IWorkContext workContext,
            IGenericAttributeService genericAttributeService,
            INewsLetterSubscriptionService newsLetterSubscriptionService,
            DateTimeSettings dateTimeSettings,
            ITaiKhoanModelFactory taikhoanModelFactory,
            ITaxService taxService,
            TaxSettings taxSettings)
        {
            _customerActivityService = customerActivityService;
            _permissionService = permissionService;
            _customerModelFactory = customerModelFactory;
            _customerService = customerService;
            _customerSettings = customerSettings;
            _customerAttributeService = customerAttributeService;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _customerAttributeParser = customerAttributeParser;
            _customerRegistrationService = customerRegistrationService;
            _storeContext = storeContext;
            _storeService = storeService;
            _workContext = workContext;
            _genericAttributeService = genericAttributeService;
            _newsLetterSubscriptionService = newsLetterSubscriptionService;
            _dateTimeSettings = dateTimeSettings;
            _taikhoanModelFactory = taikhoanModelFactory;
            _taxService = taxService;
            _taxSettings = taxSettings;

        }
        #endregion
        #region Utilities

        protected virtual string ValidateCustomerRoles(IList<CustomerRole> customerRoles, IList<CustomerRole> existingCustomerRoles)
        {
            if (customerRoles == null)
                throw new ArgumentNullException(nameof(customerRoles));

            if (existingCustomerRoles == null)
                throw new ArgumentNullException(nameof(existingCustomerRoles));

            //check ACL permission to manage customer roles
            var rolesToAdd = customerRoles.Except(existingCustomerRoles);
            var rolesToDelete = existingCustomerRoles.Except(customerRoles);
            if (rolesToAdd.Any(role => role.SystemName != NopCustomerDefaults.RegisteredRoleName) || rolesToDelete.Any())
            {
                if (!_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                    return _localizationService.GetResource("Admin.Customers.Customers.CustomerRolesManagingError");
            }

            //ensure a customer is not added to both 'Guests' and 'Registered' customer roles
            //ensure that a customer is in at least one required role ('Guests' and 'Registered')
            var isInGuestsRole = customerRoles.FirstOrDefault(cr => cr.SystemName == NopCustomerDefaults.GuestsRoleName) != null;
            var isInRegisteredRole = customerRoles.FirstOrDefault(cr => cr.SystemName == NopCustomerDefaults.RegisteredRoleName) != null;
            if (isInGuestsRole && isInRegisteredRole)
                return _localizationService.GetResource("Admin.Customers.Customers.GuestsAndRegisteredRolesError");
            if (!isInGuestsRole && !isInRegisteredRole)
                return _localizationService.GetResource("Admin.Customers.Customers.AddCustomerToGuestsOrRegisteredRoleError");

            //no errors
            return string.Empty;
        }

        protected virtual string ParseCustomCustomerAttributes(IFormCollection form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            var attributesXml = string.Empty;
            var customerAttributes = _customerAttributeService.GetAllCustomerAttributes();
            foreach (var attribute in customerAttributes)
            {
                var controlId = $"{NopCustomerServicesDefaults.CustomerAttributePrefix}{attribute.Id}";
                StringValues ctrlAttributes;

                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                        ctrlAttributes = form[controlId];
                        if (!StringValues.IsNullOrEmpty(ctrlAttributes))
                        {
                            var selectedAttributeId = int.Parse(ctrlAttributes);
                            if (selectedAttributeId > 0)
                                attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml,
                                    attribute, selectedAttributeId.ToString());
                        }

                        break;
                    case AttributeControlType.Checkboxes:
                        var cblAttributes = form[controlId];
                        if (!StringValues.IsNullOrEmpty(cblAttributes))
                        {
                            foreach (var item in cblAttributes.ToString()
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                var selectedAttributeId = int.Parse(item);
                                if (selectedAttributeId > 0)
                                    attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml,
                                        attribute, selectedAttributeId.ToString());
                            }
                        }

                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        //load read-only (already server-side selected) values
                        var attributeValues = _customerAttributeService.GetCustomerAttributeValues(attribute.Id);
                        foreach (var selectedAttributeId in attributeValues
                            .Where(v => v.IsPreSelected)
                            .Select(v => v.Id)
                            .ToList())
                        {
                            attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml,
                                attribute, selectedAttributeId.ToString());
                        }

                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextbox:
                        ctrlAttributes = form[controlId];
                        if (!StringValues.IsNullOrEmpty(ctrlAttributes))
                        {
                            var enteredText = ctrlAttributes.ToString().Trim();
                            attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml,
                                attribute, enteredText);
                        }

                        break;
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.ImageSquares:
                    case AttributeControlType.FileUpload:
                    //not supported customer attributes
                    default:
                        break;
                }
            }

            return attributesXml;
        }

        private bool SecondAdminAccountExists(Customer customer)
        {
            var customers = _customerService.GetAllCustomers(customerRoleIds: new[] { _customerService.GetCustomerRoleBySystemName(NopCustomerDefaults.AdministratorsRoleName).Id });

            return customers.Any(c => c.Active && c.Id != customer.Id);
        }

        #endregion
        public IActionResult Index()
        {
            return View();
        }
        public virtual IActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAdminStore))
                return AccessDeniedView();

            //prepare model
            var model = _taikhoanModelFactory.PrepareCustomerSearchModel(new TaiKhoanSearchModel());

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(TaiKhoanSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAdminStore))
                return AccessDeniedDataTablesJson();
            //prepare model
            var model = _taikhoanModelFactory.PrepareCustomerListModel(searchModel);
            return Json(model);
        }

        public IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAdminStore))
                return AccessDeniedView();

            //prepare model
            var model = _taikhoanModelFactory.PrepareCustomerModel(new TaiKhoanModel(), null);
            model = _taikhoanModelFactory.PrepareStoresCustomerModel(model);
            //pass domain of store's customer to view
            ViewBag.domainStore = _storeContext.CurrentStore.DomainLogin;
            ViewBag.idStore = _storeContext.CurrentStore.Id;
            return View(model);
        }
        public virtual IActionResult GetStoreAdminInfoView(int idStore)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAdminStore))
            {
                return AccessDeniedView();
            }
            var customer = _customerService.GetAllCustomers().Where(c => c.RegisteredInStoreId == idStore && _permissionService.Authorize(StandardPermissionProvider.CRMAdminStore, c))?.FirstOrDefault();
            //prepare model
            var customerModel = new TaiKhoanModel();
            var model = _taikhoanModelFactory.PrepareCustomerModel(customerModel, customer);
            var adminModel = new TaiKhoanModel();
            adminModel = customer == null ? _taikhoanModelFactory.PrepareAdminModel(model) : model;
            ViewBag.idStore = idStore;
            ViewBag.domainStore = _storeService.GetStoreById(idStore).DomainLogin;
            return PartialView("_DoanhNghiepAdminForm", adminModel);
        }

        [HttpPost/*, ParameterBasedOnFormName("save-continue", "continueEditing")*/]

        public virtual IActionResult Create(TaiKhoanModel model, bool continueEditing, IFormCollection form, bool fromAjax, int idStore)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAdminStore))
                if (fromAjax) return this.JsonDeniedMessage();
                else
                    return AccessDeniedView();

            if (!string.IsNullOrWhiteSpace(model.Email) && _customerService.GetCustomerByEmail(model.Email) != null)
                ModelState.AddModelError("Email", "Địa chỉ Email đã được đăng kí!!");

            if (!string.IsNullOrWhiteSpace(model.Username) &&
                _customerService.GetCustomerByUsername(model.Username) != null)
            {
                ModelState.AddModelError("Username", "Username đã được đăng kí!!");
            }

            //validate customer roles
            var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
            var newCustomerRoles = new List<CustomerRole>();
            foreach (var customerRole in allCustomerRoles)
                if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                    newCustomerRoles.Add(customerRole);
            var customerRolesError = ValidateCustomerRoles(newCustomerRoles, new List<CustomerRole>());
            if (!string.IsNullOrEmpty(customerRolesError))
            {
                ModelState.AddModelError(string.Empty, customerRolesError);
                _notificationService.ErrorNotification(customerRolesError);
            }

            // Ensure that valid email address is entered if Registered role is checked to avoid registered customers with empty email address
            if (newCustomerRoles.Any() && newCustomerRoles.FirstOrDefault(c => c.SystemName == NopCustomerDefaults.RegisteredRoleName) != null &&
                !CommonHelper.IsValidEmail(model.Email))
            {
                ModelState.AddModelError("Email", _localizationService.GetResource("Admin.Customers.Customers.ValidEmailRequiredRegisteredRole"));

                _notificationService.ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.ValidEmailRequiredRegisteredRole"));
            }

            //custom customer attributes
            var customerAttributesXml = ParseCustomCustomerAttributes(form);
            if (newCustomerRoles.Any() && newCustomerRoles.FirstOrDefault(c => c.SystemName == NopCustomerDefaults.RegisteredRoleName) != null)
            {
                var customerAttributeWarnings = _customerAttributeParser.GetAttributeWarnings(customerAttributesXml);
                foreach (var error in customerAttributeWarnings)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            if (ModelState.IsValid)
            {
                //fill entity from model
                var customer = model.ToEntity<Customer>();

                customer.CustomerGuid = Guid.NewGuid();
                customer.CreatedOnUtc = DateTime.UtcNow;
                customer.LastActivityDateUtc = DateTime.UtcNow;
                //customer.RegisteredInStoreId = _storeContext.CurrentStore.Id;
                customer.RegisteredInStoreId = idStore;

                _customerService.InsertCustomer(customer);

                //form fields
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.GenderAttribute, model.Gender);
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.PhoneAttribute, model.Phone);
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.FirstNameAttribute, model.FirstName);
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.LastNameAttribute, model.LastName);

                //custom customer attributes
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.CustomCustomerAttributes, customerAttributesXml);

                //password
                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    var changePassRequest = new ChangePasswordRequest(model.Email, false, _customerSettings.DefaultPasswordFormat, model.Password);
                    var changePassResult = _customerRegistrationService.ChangePassword(changePassRequest);
                    if (!changePassResult.Success)
                    {
                        foreach (var changePassError in changePassResult.Errors)
                            _notificationService.ErrorNotification(changePassError);
                    }
                }

                //customer roles
                foreach (var customerRole in newCustomerRoles)
                {
                    //ensure that the current customer cannot add to "Administrators" system role if he's not an admin himself
                    if (customerRole.SystemName == NopCustomerDefaults.AdministratorsRoleName && !_customerService.IsAdmin(_workContext.CurrentCustomer))
                        continue;

                    _customerService.AddCustomerRoleMapping(new CustomerCustomerRoleMapping { CustomerId = customer.Id, CustomerRoleId = customerRole.Id });
                }

                _customerService.UpdateCustomer(customer);

                //activity log
                _customerActivityService.InsertActivity("AddNewCustomer",
                    string.Format(_localizationService.GetResource("ActivityLog.AddNewCustomer"), customer.Id), customer);
                _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.Added"));

                if (fromAjax)
                    return this.JsonSuccessMessage("Đã tạo mới thành công", model);
                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = customer.Id });
            }

            //prepare model
            model = _taikhoanModelFactory.PrepareCustomerModel(model, null, true);
            model = _taikhoanModelFactory.PrepareStoresCustomerModel(model);
            //pass domain of store's customer to view
            ViewBag.domainStore = _storeContext.CurrentStore.DomainLogin;
            ViewBag.idStore = _storeContext.CurrentStore.Id;
            //if we got this far, something failed, redisplay form
            if (fromAjax)
                return this.JsonErrorMessage("Có lỗi!", ModelState.SerializeErrors());
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAdminStore))
                return AccessDeniedView();

            //try to get a customer with the specified id
            var customer = _customerService.GetCustomerById(id);
            if (customer == null || customer.Deleted)
                return RedirectToAction("List");

            //prepare model
            var model = _taikhoanModelFactory.PrepareCustomerModel(null, customer);
            model = _taikhoanModelFactory.PrepareStoresCustomerModel(model);
            ViewBag.isAdminStore = _customerService.GetCustomerRoles(customer)?.FirstOrDefault(r => r.SystemName.Equals("AdminStore"))!=null;
            ViewBag.domainStore = _storeContext.CurrentStore.DomainLogin;
            return View(model);
        }

        [HttpPost/*, ParameterBasedOnFormName("save-continue", "continueEditing")*/]
        //[FormValueRequired("save", "save-continue")]

        public virtual IActionResult Update(TaiKhoanModel model, bool continueEditing, IFormCollection form, bool fromAjax)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAdminStore))
                if (fromAjax) return this.JsonDeniedMessage();
                else
                    return AccessDeniedView();

            //try to get a customer with the specified id
            var customer = _customerService.GetCustomerById(model.Id);
            if (customer == null || customer.Deleted)
                if (fromAjax) return this.JsonErrorMessage("Không tìm thấy tài khoản!!");
                else
                    return RedirectToAction("List");

            //validate customer roles
            var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
            var newCustomerRoles = new List<CustomerRole>();
            foreach (var customerRole in allCustomerRoles)
                if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                    newCustomerRoles.Add(customerRole);

            var customerRolesError = ValidateCustomerRoles(newCustomerRoles, _customerService.GetCustomerRoles(customer));

            if (!string.IsNullOrEmpty(customerRolesError))
            {
                ModelState.AddModelError(string.Empty, customerRolesError);
                _notificationService.ErrorNotification(customerRolesError);
            }

            // Ensure that valid email address is entered if Registered role is checked to avoid registered customers with empty email address
            if (newCustomerRoles.Any() && newCustomerRoles.FirstOrDefault(c => c.SystemName == NopCustomerDefaults.RegisteredRoleName) != null &&
                !CommonHelper.IsValidEmail(model.Email))
            {
                ModelState.AddModelError("Email", _localizationService.GetResource("Admin.Customers.Customers.ValidEmailRequiredRegisteredRole"));
                _notificationService.ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.ValidEmailRequiredRegisteredRole"));
            }

            //custom customer attributes
            var customerAttributesXml = ParseCustomCustomerAttributes(form);
            if (newCustomerRoles.Any() && newCustomerRoles.FirstOrDefault(c => c.SystemName == NopCustomerDefaults.RegisteredRoleName) != null)
            {
                var customerAttributeWarnings = _customerAttributeParser.GetAttributeWarnings(customerAttributesXml);
                foreach (var error in customerAttributeWarnings)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //prevent deactivation of the last active administrator
                    if (!_customerService.IsAdmin(customer) || model.Active || SecondAdminAccountExists(customer))
                        customer.Active = model.Active;
                    else
                        _notificationService.ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.AdminAccountShouldExists.Deactivate"));

                    //email
                    if (!string.IsNullOrWhiteSpace(model.Email))
                        _customerRegistrationService.SetEmail(customer, model.Email, false);
                    else
                        customer.Email = model.Email;

                    //username
                    customer.Username = model.Username;

                    //form fields
                    _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.GenderAttribute, model.Gender);
                    _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.PhoneAttribute, model.Phone);
                    _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.FirstNameAttribute, model.FirstName);
                    _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.LastNameAttribute, model.LastName);

                    //custom customer attributes
                    _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.CustomCustomerAttributes, customerAttributesXml);

                    var currentCustomerRoleIds = _customerService.GetCustomerRoleIds(customer, true);

                    //customer roles
                    foreach (var customerRole in allCustomerRoles)
                    {
                        //ensure that the current customer cannot add/remove to/from "Administrators" system role
                        //if he's not an admin himself
                        if (customerRole.SystemName == NopCustomerDefaults.AdministratorsRoleName &&
                            !_customerService.IsAdmin(_workContext.CurrentCustomer))
                            continue;

                        if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                        {
                            //new role
                            if (currentCustomerRoleIds.All(roleId => roleId != customerRole.Id))
                                _customerService.AddCustomerRoleMapping(new CustomerCustomerRoleMapping { CustomerId = customer.Id, CustomerRoleId = customerRole.Id });
                        }
                        else
                        {
                            //prevent attempts to delete the administrator role from the user, if the user is the last active administrator
                            if (customerRole.SystemName == NopCustomerDefaults.AdministratorsRoleName && !SecondAdminAccountExists(customer))
                            {
                                _notificationService.ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.AdminAccountShouldExists.DeleteRole"));
                                continue;
                            }

                            //remove role
                            if (currentCustomerRoleIds.Any(roleId => roleId == customerRole.Id))
                                _customerService.RemoveCustomerRoleMapping(customer, customerRole);
                        }
                    }

                    _customerService.UpdateCustomer(customer);
                    //activity log
                    _customerActivityService.InsertActivity("EditCustomer",
                        string.Format(_localizationService.GetResource("ActivityLog.EditCustomer"), customer.Id), customer);

                    _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.Updated"));
                    if (fromAjax)
                        return this.JsonSuccessMessage("Đã chỉnh sửa thành công", model);
                    if (!continueEditing)
                        return RedirectToAction("List");

                    return RedirectToAction("Edit", new { id = customer.Id });
                }
                catch (Exception exc)
                {
                    _notificationService.ErrorNotification(exc.Message);
                }
            }

            //prepare model
            model = _taikhoanModelFactory.PrepareCustomerModel(model, customer, true);
            model = _taikhoanModelFactory.PrepareStoresCustomerModel(model);
            if (fromAjax)
                return this.JsonErrorMessage("Có lỗi!", ModelState.SerializeErrors());
            //if we got this far, something failed, redisplay form
            _notificationService.ErrorNotification("Có lỗi!");
            ViewBag.isAdminStore = _customerService.GetCustomerRoles(customer)?.FirstOrDefault(r => r.SystemName.Equals("AdminStore"))!=null;
            ViewBag.domainStore = _storeContext.CurrentStore.DomainLogin;
            //return View("Edit",model);
            return RedirectToAction("Edit",new { id= model.Id});
        }
        [HttpPost]
        public virtual IActionResult ChangePassword(TaiKhoanModel model, bool fromAjax)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAdminStore))
                if (fromAjax) return this.JsonDeniedMessage();
                else
                    return AccessDeniedView();

            //try to get a customer with the specified id
            var customer = _customerService.GetCustomerById(model.Id);
            if (customer == null)
                if (fromAjax) return this.JsonErrorMessage("Không tìm thấy tài khoản!!");
                else
                    return RedirectToAction("List");

            //ensure that the current customer cannot change passwords of "Administrators" if he's not an admin himself
            if (_customerService.IsAdmin(customer) && !_customerService.IsAdmin(_workContext.CurrentCustomer))
            {
                if (fromAjax)
                {
                    return JsonErrorMessage(_localizationService.GetResource("Admin.Customers.Customers.OnlyAdminCanChangePassword"));
                }
                else
                {
                    _notificationService.ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.OnlyAdminCanChangePassword"));
                    return RedirectToAction("Edit", new { id = customer.Id });
                }
            }

            if (!ModelState.IsValid)
                if (fromAjax) return this.JsonErrorMessage("Có Lỗi!!", ModelState.SerializeErrors());
                else
                    return RedirectToAction("Edit", new { id = customer.Id });

            var changePassRequest = new ChangePasswordRequest(model.Email,
                false, _customerSettings.DefaultPasswordFormat, model.Password);
            var changePassResult = _customerRegistrationService.ChangePassword(changePassRequest);
            if (fromAjax)
            {
                if (changePassResult.Success) return JsonSuccessMessage("Đã thay đổi mật khẩu thành công", model);
                else return JsonErrorMessage("Chưa thay đổi được mật khẩu", changePassResult.Errors);
            }
            if (changePassResult.Success)
                _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.PasswordChanged"));
            else
                foreach (var error in changePassResult.Errors)
                    _notificationService.ErrorNotification(error);

            return RedirectToAction("Edit", new { id = customer.Id });
         }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CRMAdminStore))
                return AccessDeniedView();

            //try to get a customer with the specified id
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
                return RedirectToAction("List");

            try
            {
                //prevent attempts to delete the user, if it is the last active administrator
                if (_customerService.IsAdmin(customer) && !SecondAdminAccountExists(customer))
                {
                    _notificationService.ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.AdminAccountShouldExists.DeleteAdministrator"));
                    return RedirectToAction("Edit", new { id = customer.Id });
                }

                //ensure that the current customer cannot delete "Administrators" if he's not an admin himself
                if (_customerService.IsAdmin(customer) && !_customerService.IsAdmin(_workContext.CurrentCustomer))
                {
                    _notificationService.ErrorNotification(_localizationService.GetResource("Admin.Customers.Customers.OnlyAdminCanDeleteAdmin"));
                    return RedirectToAction("Edit", new { id = customer.Id });
                }

                //delete
                _customerService.DeleteCustomer(customer);

                //remove newsletter subscription (if exists)
                foreach (var store in _storeService.GetAllStores())
                {
                    var subscription = _newsLetterSubscriptionService.GetNewsLetterSubscriptionByEmailAndStoreId(customer.Email, store.Id);
                    if (subscription != null)
                        _newsLetterSubscriptionService.DeleteNewsLetterSubscription(subscription);
                }

                //activity log
                _customerActivityService.InsertActivity("DeleteCustomer",
                    string.Format(_localizationService.GetResource("ActivityLog.DeleteCustomer"), customer.Id), customer);

                _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.Deleted"));

                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc.Message);
                return RedirectToAction("Edit", new { id = customer.Id });
            }
        }

    }
}
