using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 11/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Core.Domain.CRM;
using Nop.Core.Domain.Stores;
//using Nop.Web.Areas.CRM.Models;
using Nop.Web.Areas.CRM.Models.CRM;
namespace Nop.Web.Areas.CRM.Factory.CRM
{
    public interface IDoanhNghiepModelFactory
    {

        /// <summary>
        /// Prepare store search model
        /// </summary>
        /// <param name="searchModel">Store search model</param>
        /// <returns>Store search model</returns>
        StoreSearchModel PrepareStoreSearchModel(StoreSearchModel searchModel);

        /// <summary>
        /// Prepare paged store list model
        /// </summary>
        /// <param name="searchModel">Store search model</param>
        /// <returns>Store list model</returns>
        StoreListModel PrepareStoreListModel(StoreSearchModel searchModel);

        /// <summary>
        /// Prepare store model
        /// </summary>
        /// <param name="model">Store model</param>
        /// <param name="store">Store</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>Store model</returns>
        StoreModel PrepareStoreModel(StoreModel model, Store store, bool excludeProperties = false);
        void PrepareStore(StoreModel model, Store entity);
    }
}
