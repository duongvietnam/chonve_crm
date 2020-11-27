using Nop.Core.Domain.Customers;
using Nop.Web.Areas.Admin.Models.Customers;
using Nop.Web.Areas.CRM.Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Nop.Web.Areas.CRM.Models.CRM.TaiKhoanModel;

namespace Nop.Web.Areas.CRM.Factory.CRM
{
    /// <summary>
    /// Represents the taikhoan model factory
    /// </summary>
    public interface ITaiKhoanModelFactory
    {
        /// <summary>
        /// Prepare customer search model
        /// </summary>
        /// <param name="searchModel">TaiKhoanModel search model</param>
        /// <returns>Customer search model</returns>
        TaiKhoanSearchModel PrepareCustomerSearchModel(TaiKhoanSearchModel searchModel);

        /// <summary>
        /// Prepare paged customer list model
        /// </summary>
        /// <param name="searchModel">TaiKhoanModel search model</param>
        /// <returns>Customer list model</returns>
        TaiKhoanListModel PrepareCustomerListModel(TaiKhoanSearchModel searchModel);

        /// <summary>
        /// Prepare customer model
        /// </summary>
        /// <param name="model">TaiKhoanModel model</param>
        /// <param name="customer">Customer</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>Customer model</returns>
        TaiKhoanModel PrepareCustomerModel(TaiKhoanModel model, Customer customer, bool excludeProperties = false);

        /// <summary>
        /// Prepare customer model
        /// </summary>
        /// <param name="model">TaiKhoanModel model</param>
        /// <returns>Customer model</returns>
        TaiKhoanModel PrepareAdminModel(TaiKhoanModel model);
        /// <summary>
        /// Prepare Store's customer model
        /// </summary>
        /// <param name="model">TaiKhoanModel model</param>
        /// <returns>Customer model</returns>
        TaiKhoanModel PrepareStoresCustomerModel(TaiKhoanModel model);

    }
}
