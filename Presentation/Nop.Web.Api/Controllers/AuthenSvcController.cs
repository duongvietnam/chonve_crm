using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Authentication;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Stores;
using Nop.Web.Api.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Api.Controllers
{
    public class AuthenSvcController : BaseApiController
    {
        #region Fields

        private readonly CustomerSettings _customerSettings;
        private readonly DateTimeSettings _dateTimeSettings;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ICustomerAttributeParser _customerAttributeParser;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ICustomerService _customerService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILogger _logger;
        private readonly IWebHelper _webHelper;
        private readonly ILocalizationService _localizationService;
        private readonly IStoreService _storeService;
        #endregion

        #region Ctor

        public AuthenSvcController(
            IStoreService storeService,
            ILocalizationService localizationService,
            CustomerSettings customerSettings,
            DateTimeSettings dateTimeSettings,
            IAddressAttributeParser addressAttributeParser,
            IAddressService addressService,
            IAuthenticationService authenticationService,
            ICustomerActivityService customerActivityService,
            ICustomerAttributeParser customerAttributeParser,
            ICustomerAttributeService customerAttributeService,
            ICustomerRegistrationService customerRegistrationService,
            ICustomerService customerService,
            IGenericAttributeService genericAttributeService,
            ILogger logger,
            IStoreContext storeContext,
            IWebHelper webHelper,
            IWorkContext workContext
)
        {
            _storeService = storeService;
            _customerSettings = customerSettings;
            _dateTimeSettings = dateTimeSettings;
            _authenticationService = authenticationService;
            _customerActivityService = customerActivityService;
            _customerAttributeParser = customerAttributeParser;
            _customerAttributeService = customerAttributeService;
            _customerRegistrationService = customerRegistrationService;
            _customerService = customerService;
            _genericAttributeService = genericAttributeService;
            _localizationService = localizationService;
            _logger = logger;
            _webHelper = webHelper;
        }

        #endregion
        [HttpGet]
        public IActionResult Ping(string s)
        {
            return Ok("Hello world: " + s);
        }
        #region Method
        [HttpPost]
        public IActionResult Login([FromBody]LoginModel model)
        {
            //kiem tra thong tin AppId

            string loginMsg = "";
            //lay thong tin store qua access key
            var _storeItem = _storeService.GetStoreByKey(model.StoreKey);
            if(_storeItem==null)
            {
                return OkNotFoundMessage("Bạn chưa đăng ký công ty", model);
            }
            string _email = string.Format("{0}@{1}", model.Username, _storeItem.DomainLogin);
            var loginResult = _customerRegistrationService.ValidateCustomer(_email, model.Password);
            switch (loginResult)
            {
                case CustomerLoginResults.Successful:
                    {
                        var customer = _customerService.GetCustomerByEmail(_email);
                        //activity log
                        _customerActivityService.InsertActivity(customer, "API.Login",
                            _localizationService.GetResource("ActivityLog.PublicStore.Login"), customer);
                        return OkSuccessMessage("Ok", _authenticationService.GenerateToken(customer));
                    }
                case CustomerLoginResults.CustomerNotExist:
                    loginMsg=_localizationService.GetResource("Account.Login.WrongCredentials.CustomerNotExist");
                    break;
                case CustomerLoginResults.Deleted:
                    loginMsg=_localizationService.GetResource("Account.Login.WrongCredentials.Deleted");
                    break;
                case CustomerLoginResults.NotActive:
                    loginMsg=_localizationService.GetResource("Account.Login.WrongCredentials.NotActive");
                    break;
                case CustomerLoginResults.NotRegistered:
                    loginMsg=_localizationService.GetResource("Account.Login.WrongCredentials.NotRegistered");
                    break;
                case CustomerLoginResults.LockedOut:
                    loginMsg=_localizationService.GetResource("Account.Login.WrongCredentials.LockedOut");
                    break;
                case CustomerLoginResults.WrongPassword:
                default:
                    loginMsg=_localizationService.GetResource("Account.Login.WrongCredentials");
                    break;
            }
            
            return OkNotFoundMessage(loginMsg,model);
        }
        #endregion
    }
}
