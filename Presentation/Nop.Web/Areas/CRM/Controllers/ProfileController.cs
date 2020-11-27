using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.Gdpr;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Tax;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.ExportImport;
using Nop.Services.Forums;
using Nop.Services.Gdpr;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Models.Customers;
using Nop.Web.Areas.CRM.Models.Common;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.CRM.Controllers
{
    public class ProfileController : BaseCRMController
    {
        #region Fields

        private readonly CustomerSettings _customerSettings;
        private readonly DateTimeSettings _dateTimeSettings;
        private readonly EmailAccountSettings _emailAccountSettings;
        private readonly ForumSettings _forumSettings;
        private readonly GdprSettings _gdprSettings;
        private readonly IAddressAttributeParser _addressAttributeParser;
        private readonly IAddressService _addressService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ICustomerAttributeParser _customerAttributeParser;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly ICustomerModelFactory _customerModelFactory;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ICustomerService _customerService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IEmailAccountService _emailAccountService;
        private readonly IExportManager _exportManager;
        private readonly IForumService _forumService;
        private readonly IGdprService _gdprService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IQueuedEmailService _queuedEmailService;
        private readonly IRewardPointService _rewardPointService;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly ITaxService _taxService;
        private readonly IWorkContext _workContext;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly TaxSettings _taxSettings;

        #endregion

        #region Ctor

        public ProfileController(CustomerSettings customerSettings,
            DateTimeSettings dateTimeSettings,
            EmailAccountSettings emailAccountSettings,
            ForumSettings forumSettings,
            GdprSettings gdprSettings,
            IAddressAttributeParser addressAttributeParser,
            IAddressService addressService,
            ICustomerActivityService customerActivityService,
            ICustomerAttributeParser customerAttributeParser,
            ICustomerAttributeService customerAttributeService,
            ICustomerModelFactory customerModelFactory,
            ICustomerRegistrationService customerRegistrationService,
            ICustomerService customerService,
            IDateTimeHelper dateTimeHelper,
            IEmailAccountService emailAccountService,
            IExportManager exportManager,
            IForumService forumService,
            IGdprService gdprService,
            IGenericAttributeService genericAttributeService,
            ILocalizationService localizationService,
            INewsLetterSubscriptionService newsLetterSubscriptionService,
            INotificationService notificationService,
            IPermissionService permissionService,
            IQueuedEmailService queuedEmailService,
            IRewardPointService rewardPointService,
            IStoreContext storeContext,
            IStoreService storeService,
            ITaxService taxService,
            IWorkContext workContext,
            IWorkflowMessageService workflowMessageService,
            TaxSettings taxSettings)
        {
            _customerSettings = customerSettings;
            _dateTimeSettings = dateTimeSettings;
            _emailAccountSettings = emailAccountSettings;
            _forumSettings = forumSettings;
            _gdprSettings = gdprSettings;
            _addressAttributeParser = addressAttributeParser;
            _addressService = addressService;
            _customerActivityService = customerActivityService;
            _customerAttributeParser = customerAttributeParser;
            _customerAttributeService = customerAttributeService;
            _customerModelFactory = customerModelFactory;
            _customerRegistrationService = customerRegistrationService;
            _customerService = customerService;
            _dateTimeHelper = dateTimeHelper;
            _emailAccountService = emailAccountService;
            _exportManager = exportManager;
            _forumService = forumService;
            _gdprService = gdprService;
            _genericAttributeService = genericAttributeService;
            _localizationService = localizationService;
            _newsLetterSubscriptionService = newsLetterSubscriptionService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _queuedEmailService = queuedEmailService;
            _rewardPointService = rewardPointService;
            _storeContext = storeContext;
            _storeService = storeService;
            _taxService = taxService;
            _workContext = workContext;
            _workflowMessageService = workflowMessageService;
            _taxSettings = taxSettings;
        }

        #endregion
        public virtual IActionResult Index()
        {

            return View();
        }
        public virtual IActionResult Info(string tab)
        {
            if (!_customerService.IsRegistered(_workContext.CurrentCustomer))
                return AccessDeniedView();
            var model = new ProfileModel();
            model.customerModel = _customerModelFactory.PrepareCustomerModel(null, _workContext.CurrentCustomer);
            model.TabNameFocus = tab;
            return View(model);

        }
        public virtual IActionResult _Info()
        {
           
            var model = _customerModelFactory.PrepareCustomerModel(null, _workContext.CurrentCustomer);

            return PartialView(model);
        }
        public virtual IActionResult _ChangePassword()
        {
            var model = new PasswordChangeModel();
            //lan dau tien dang nhap thi doi pass
            model.IsFirstTime = !_workContext.CurrentCustomer.LastLoginDateUtc.HasValue;
            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult _ChangePassword(PasswordChangeModel model)
        {
            var customer = _workContext.CurrentCustomer;

            if (ModelState.IsValid)
            {
                var changePasswordRequest = new ChangePasswordRequest(customer.Email,
                    true, _customerSettings.DefaultPasswordFormat, model.NewPassword, model.OldPassword);
                var changePasswordResult = _customerRegistrationService.ChangePassword(changePasswordRequest);
                if (changePasswordResult.Success)
                {
                    return this.JsonSuccessMessage("Thay đổi mật khẩu thành công!");
                }

                //errors
                foreach (var error in changePasswordResult.Errors)
                    ModelState.AddModelError("", error);
            }

            //If we got this far, something failed, redisplay form
            return this.JsonErrorMessage("Có lỗi!", ModelState.SerializeErrors());
        }
        public virtual IActionResult _NotificationInfor()
        {
            return PartialView();
        }
        public virtual IActionResult _LogInfor()
        {

            var model = _customerModelFactory.PrepareCustomerModel(null, _workContext.CurrentCustomer);

            return PartialView(model);
        }
        [HttpPost]
        public virtual IActionResult ListActivityLog(CustomerActivityLogSearchModel searchModel)
        {
            

            //prepare model
            var model = _customerModelFactory.PrepareCustomerActivityLogListModel(searchModel, _workContext.CurrentCustomer);

            return Json(model);
        }

    }
}
