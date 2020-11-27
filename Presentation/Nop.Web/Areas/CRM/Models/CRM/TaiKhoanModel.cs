using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Areas.Admin.Models.Customers;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.CRM.Models.CRM
{
    /// <summary>
    /// Represents a customer model CRM
    /// </summary>
    public class TaiKhoanModel: BaseNopEntityModel, IAclSupportedModel
    {
        #region Ctor

        public TaiKhoanModel()
        {
            SelectedCustomerRoleIds = new List<int>();
            AvailableCustomerRoles = new List<SelectListItem>();

        }

        #endregion
        #region Properties
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Username")]
        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Email")]
        public string Email { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Password")]
        [DataType(DataType.Password)]
        [NoTrim]
        public string Password { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Gender")]
        public string Gender { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.FullName")]
        public string FullName { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.FirstName")]
        public string FirstName { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.LastName")]
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Phone")]
        public string Phone { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Active")]
        public bool Active { get; set; }
        //customer roles
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.CustomerRoles")]
        public string CustomerRoleNames { get; set; }

        public IList<SelectListItem> AvailableCustomerRoles { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.CustomerRoles")]
        public IList<int> SelectedCustomerRoleIds { get; set; }
        //registration date
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.LastActivityDate")]
        public DateTime LastActivityDate { get; set; }

        //IP address
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.IPAddress")]
        public string LastIpAddress { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.LastVisitedPage")]
        public string LastVisitedPage { get; set; }

        #endregion
        
    }
    /// <summary>
    /// Represents a customer list model
    /// </summary>
    public partial class TaiKhoanListModel : BasePagedListModel<TaiKhoanModel>
    {
    }
    /// <summary>
    /// Represents a customer search model
    /// </summary>
    public partial class TaiKhoanSearchModel : BaseSearchModel, IAclSupportedModel
    {
        #region Ctor

        public TaiKhoanSearchModel()
        {
            SelectedCustomerRoleIds = new List<int>();
            AvailableCustomerRoles = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [NopResourceDisplayName("Admin.Customers.Customers.List.CustomerRoles")]
        public IList<int> SelectedCustomerRoleIds { get; set; }

        public IList<SelectListItem> AvailableCustomerRoles { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.List.SearchEmail")]
        public string SearchEmail { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.List.SearchUsername")]
        public string SearchUsername { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.List.SearchFullName")]
        public string SearchFullName { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.List.SearchPhone")]
        public string SearchPhone { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.List.SearchIpAddress")]
        public string SearchIpAddress { get; set; }

        public int SearchIdStore { get; set; }
        #endregion
    }
}
