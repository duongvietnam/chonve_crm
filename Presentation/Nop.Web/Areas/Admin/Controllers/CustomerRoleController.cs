using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Security;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Customers;
using Nop.Web.Areas.Admin.Models.Security;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Web.Areas.Admin.Controllers
{
    public partial class CustomerRoleController : BaseAdminController
    {
        #region Fields

        private readonly ICustomerActivityService _customerActivityService;
        private readonly ICustomerRoleModelFactory _customerRoleModelFactory;
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IProductService _productService;
        private readonly IWorkContext _workContext;
        private readonly ISecurityModelFactory _securityModelFactory;

        #endregion

        #region Ctor

        public CustomerRoleController(ISecurityModelFactory securityModelFactory,
            ICustomerActivityService customerActivityService,
            ICustomerRoleModelFactory customerRoleModelFactory,
            ICustomerService customerService,
            ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            IProductService productService,
            IWorkContext workContext)
        {
            _securityModelFactory = securityModelFactory;
            _customerActivityService = customerActivityService;
            _customerRoleModelFactory = customerRoleModelFactory;
            _customerService = customerService;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _productService = productService;
            _workContext = workContext;
        }

        #endregion

        #region Methods

        public virtual IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual IActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();

            //prepare model
            var model = _customerRoleModelFactory.PrepareCustomerRoleSearchModel(new CustomerRoleSearchModel());

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult List(CustomerRoleSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedDataTablesJson();

            //prepare model
            var model = _customerRoleModelFactory.PrepareCustomerRoleListModel(searchModel);

            return Json(model);
        }

        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers) || !_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                return AccessDeniedView();

            //prepare model
            var model = _customerRoleModelFactory.PrepareCustomerRoleModel(new CustomerRoleModel(), null);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(CustomerRoleModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers) || !_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var customerRole = model.ToEntity<CustomerRole>();
                _customerService.InsertCustomerRole(customerRole);

                //activity log
                _customerActivityService.InsertActivity("AddNewCustomerRole",
                    string.Format(_localizationService.GetResource("ActivityLog.AddNewCustomerRole"), customerRole.Name), customerRole);

                _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Customers.CustomerRoles.Added"));

                return continueEditing ? RedirectToAction("Edit", new { id = customerRole.Id }) : RedirectToAction("List");
            }

            //prepare model
            model = _customerRoleModelFactory.PrepareCustomerRoleModel(model, null, true);

            //if we got this far, something failed, redisplay form
            return View(model);
        }

        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers) || !_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                return AccessDeniedView();

            //try to get a customer role with the specified id
            var customerRole = _customerService.GetCustomerRoleById(id);
            if (customerRole == null)
                return RedirectToAction("List");

            //prepare model
            var model = _customerRoleModelFactory.PrepareCustomerRoleModel(null, customerRole);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Edit(CustomerRoleModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers) || !_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                return AccessDeniedView();

            //try to get a customer role with the specified id
            var customerRole = _customerService.GetCustomerRoleById(model.Id);
            if (customerRole == null)
                return RedirectToAction("List");

            try
            {
                if (ModelState.IsValid)
                {
                    if (customerRole.IsSystemRole && !model.Active)
                        throw new NopException(_localizationService.GetResource("Admin.Customers.CustomerRoles.Fields.Active.CantEditSystem"));

                    if (customerRole.IsSystemRole && !customerRole.SystemName.Equals(model.SystemName, StringComparison.InvariantCultureIgnoreCase))
                        throw new NopException(_localizationService.GetResource("Admin.Customers.CustomerRoles.Fields.SystemName.CantEditSystem"));

                    if (NopCustomerDefaults.RegisteredRoleName.Equals(customerRole.SystemName, StringComparison.InvariantCultureIgnoreCase) &&
                        model.PurchasedWithProductId > 0)
                        throw new NopException(_localizationService.GetResource("Admin.Customers.CustomerRoles.Fields.PurchasedWithProduct.Registered"));

                    customerRole = model.ToEntity(customerRole);
                    _customerService.UpdateCustomerRole(customerRole);

                    //activity log
                    _customerActivityService.InsertActivity("EditCustomerRole",
                        string.Format(_localizationService.GetResource("ActivityLog.EditCustomerRole"), customerRole.Name), customerRole);

                    _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Customers.CustomerRoles.Updated"));

                    return continueEditing ? RedirectToAction("Edit", new { id = customerRole.Id }) : RedirectToAction("List");
                }

                //prepare model
                model = _customerRoleModelFactory.PrepareCustomerRoleModel(model, customerRole, true);

                //if we got this far, something failed, redisplay form
                return View(model);
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = customerRole.Id });
            }
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers) || !_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                return AccessDeniedView();

            //try to get a customer role with the specified id
            var customerRole = _customerService.GetCustomerRoleById(id);
            if (customerRole == null)
                return RedirectToAction("List");

            try
            {
                _customerService.DeleteCustomerRole(customerRole);

                //activity log
                _customerActivityService.InsertActivity("DeleteCustomerRole",
                    string.Format(_localizationService.GetResource("ActivityLog.DeleteCustomerRole"), customerRole.Name), customerRole);

                _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Customers.CustomerRoles.Deleted"));

                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc.Message);
                return RedirectToAction("Edit", new { id = customerRole.Id });
            }
        }

        public virtual IActionResult AssociateProductToCustomerRolePopup()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers) || !_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                return AccessDeniedView();

            //prepare model
            var model = _customerRoleModelFactory.PrepareCustomerRoleProductSearchModel(new CustomerRoleProductSearchModel());

            return View(model);
        }

        [HttpPost]
        public virtual IActionResult AssociateProductToCustomerRolePopupList(CustomerRoleProductSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers) || !_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                return AccessDeniedDataTablesJson();

            //prepare model
            var model = _customerRoleModelFactory.PrepareCustomerRoleProductListModel(searchModel);

            return Json(model);
        }

        [HttpPost]
        [FormValueRequired("save")]
        public virtual IActionResult AssociateProductToCustomerRolePopup([Bind(Prefix = nameof(AddProductToCustomerRoleModel))] AddProductToCustomerRoleModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers) || !_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                return AccessDeniedView();

            //try to get a product with the specified id
            var associatedProduct = _productService.GetProductById(model.AssociatedToProductId);
            if (associatedProduct == null)
                return Content("Cannot load a product");

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null && associatedProduct.VendorId != _workContext.CurrentVendor.Id)
                return Content("This is not your product");

            ViewBag.RefreshPage = true;
            ViewBag.productId = associatedProduct.Id;
            ViewBag.productName = associatedProduct.Name;

            return View(new CustomerRoleProductSearchModel());
        }

        #endregion
        #region Permisions
        public virtual IActionResult PermissionsRole(int RoleId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                return AccessDeniedView();

            //prepare model
            var model = _securityModelFactory.PreparePermissionMappingModel(new PermissionMappingModel(), RoleId);

            return View(model);
        }
        [HttpPost, ActionName("PermissionsRole")]
        public virtual IActionResult PermissionsSave(PermissionMappingModel model, IFormCollection form)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                return AccessDeniedView();

            var permissionRecords = _permissionService.GetAllPermissionRecords();
            var customerRoles = _customerService.GetAllCustomerRoles(true).Where(c => c.Id == model.RoleId).ToList();

            foreach (var cr in customerRoles)
            {
                var formKey = "allow_" + cr.Id;
                var permissionRecordSystemNamesToRestrict = !StringValues.IsNullOrEmpty(form[formKey])
                    ? form[formKey].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                    : new List<string>();

                foreach (var pr in permissionRecords)
                {
                    var allow = permissionRecordSystemNamesToRestrict.Contains(pr.SystemName);

                    if (allow == _permissionService.Authorize(pr.SystemName, cr.Id))
                        continue;

                    if (allow)
                    {
                        _permissionService.InsertPermissionRecordCustomerRoleMapping(new PermissionRecordCustomerRoleMapping { PermissionRecordId = pr.Id, CustomerRoleId = cr.Id });
                    }
                    else
                    {
                        _permissionService.DeletePermissionRecordCustomerRoleMapping(pr.Id, cr.Id);
                    }

                    _permissionService.UpdatePermissionRecord(pr);
                }
            }

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Configuration.ACL.Updated"));

            return View(model);
        }

        

        #endregion
    }
}