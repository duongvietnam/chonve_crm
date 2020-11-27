using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Authentication;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Web.Api.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Api.Controllers
{
    public class CommonSvcController: BaseAdminController
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
        #endregion

        #region Ctor

        public CommonSvcController(
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
        #region Danh muc
        [HttpGet]
        public IActionResult Ping()
        {
            return this.OkSuccessMessage("Hello world",this.currentCustomer);
        }
        #endregion
    }
}
