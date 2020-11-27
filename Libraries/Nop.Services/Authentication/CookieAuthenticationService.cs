using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Nop.Core.Configuration;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;

namespace Nop.Services.Authentication
{
    /// <summary>
    /// Represents service using cookie middleware for the authentication
    /// </summary>
    public partial class CookieAuthenticationService : IAuthenticationService
    {
        #region Fields

        private readonly CustomerSettings _customerSettings;
        private readonly ICustomerService _customerService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NopConfig _nopConfig;
        private Customer _cachedCustomer;

        #endregion

        #region Ctor

        public CookieAuthenticationService(
            NopConfig nopConfig,
            CustomerSettings customerSettings,
            ICustomerService customerService,
            IHttpContextAccessor httpContextAccessor)
        {
            _nopConfig = nopConfig;
            _customerSettings = customerSettings;
            _customerService = customerService;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="isPersistent">Whether the authentication session is persisted across multiple requests</param>
        public virtual async void SignIn(Customer customer, bool isPersistent)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            //create claims for customer's username and email
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(customer.Username))
                claims.Add(new Claim(ClaimTypes.Name, customer.Username, ClaimValueTypes.String, NopAuthenticationDefaults.ClaimsIssuer));

            if (!string.IsNullOrEmpty(customer.Email))
                claims.Add(new Claim(ClaimTypes.Email, customer.Email, ClaimValueTypes.Email, NopAuthenticationDefaults.ClaimsIssuer));

            //create principal for the current authentication scheme
            var userIdentity = new ClaimsIdentity(claims, NopAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            //set value indicating whether session is persisted and the time at which the authentication was issued
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.UtcNow
            };

            //sign in
            await _httpContextAccessor.HttpContext.SignInAsync(NopAuthenticationDefaults.AuthenticationScheme, userPrincipal, authenticationProperties);

            //cache authenticated customer
            _cachedCustomer = customer;
        }

        /// <summary>
        /// Sign out
        /// </summary>
        public virtual async void SignOut()
        {
            //reset cached customer
            _cachedCustomer = null;

            //and sign out from the current authentication scheme
            await _httpContextAccessor.HttpContext.SignOutAsync(NopAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Get authenticated customer
        /// </summary>
        /// <returns>Customer</returns>
        public virtual Customer GetAuthenticatedCustomer()
        {
            if (_nopConfig.IsApiApplication)
                return null;
            //whether there is a cached customer
            if (_cachedCustomer != null)
                return _cachedCustomer;

            //try to get authenticated user identity
            var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(NopAuthenticationDefaults.AuthenticationScheme).Result;
            if (!authenticateResult.Succeeded)
                return null;

            Customer customer = null;
            if (_customerSettings.UsernamesEnabled)
            {
                //try to get customer by username
                var usernameClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Name
                    && claim.Issuer.Equals(NopAuthenticationDefaults.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
                if (usernameClaim != null)
                    customer = _customerService.GetCustomerByUsername(usernameClaim.Value);
            }
            else
            {
                //try to get customer by email
                var emailClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Email
                    && claim.Issuer.Equals(NopAuthenticationDefaults.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
                if (emailClaim != null)
                    customer = _customerService.GetCustomerByEmail(emailClaim.Value);
            }

            //whether the found customer is available
            if (customer == null || !customer.Active || customer.RequireReLogin || customer.Deleted || !_customerService.IsRegistered(customer))
                return null;

            //cache authenticated customer
            _cachedCustomer = customer;

            return _cachedCustomer;
        }

        #endregion
        #region Token
        private List<Claim> GetUserClaims(CustomerApp item)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.PrimarySid, item.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.PrimaryGroupSid, item.NhanVienId.GetValueOrDefault(0).ToString()));
            claims.Add(new Claim(ClaimTypes.GroupSid, item.StoreId.ToString()));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, item.CustomerGuid.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, item.Email));            
            claims.Add(new Claim(ClaimTypes.Name, item.Fullname));
            
            return claims;
        }

        public CustomerApp GenerateToken(Customer item)
        {
            var _customerApp = new CustomerApp();
            _customerApp.Id = item.Id;
            _customerApp.CustomerGuid = item.CustomerGuid;
            _customerApp.Username = item.Username;
            _customerApp.Email = item.Email;
            _customerApp.NhanVienId = item.NhanVienId;
            _customerApp.Fullname = _customerService.GetCustomerFullName(item);
            _customerApp.StoreId = item.RegisteredInStoreId;
            var claims = GetUserClaims(_customerApp);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _nopConfig.ApiValidIssuer,
                Audience = _nopConfig.ApiValidIssuer,
                Expires=DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_nopConfig.ApiKeyEncrypt)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            _customerApp.Token= tokenHandler.WriteToken(token);
            return _customerApp;


        }
        private IEnumerable<Claim> GetClaims(string tokenString)
        {
            try
            {
                SecurityToken securityToken = new JwtSecurityToken(tokenString);
                JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();


                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_nopConfig.ApiKeyEncrypt)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = _nopConfig.ApiValidIssuer,
                    ValidIssuer = _nopConfig.ApiValidIssuer,
                    ValidateLifetime = true
                };

                ClaimsPrincipal claimsPrincipal = securityTokenHandler.ValidateToken(tokenString, validationParameters, out securityToken);

                return claimsPrincipal.Claims;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public CustomerApp ValidateToken(string tokenString)
        {
            var claims = GetClaims(tokenString);
            if (claims != null)
            {
                var item = new CustomerApp();
                item.Id = Convert.ToInt32(claims.Where(c => c.Type == ClaimTypes.PrimarySid).First().Value);
                item.CustomerGuid = new Guid(claims.Where(c => c.Type == ClaimTypes.PrimaryGroupSid).First().Value);
                item.Email = claims.Where(c => c.Type == ClaimTypes.Email).First().Value;
                item.Fullname = claims.Where(c => c.Type == ClaimTypes.Name).First().Value;
                item.Token = tokenString;
                return item;
            }
            return null;
        }
        #endregion
    }
}