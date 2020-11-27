//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 21/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.CRM.Models.CRM
{
    
	public class DvDonViTinhModel :BaseNopEntityModel
	{
        public DvDonViTinhModel(){
        
        }
		public String TEN {get;set;}
		public Int32? DOANH_NGHIEP_ID {get;set;}
	}
    public partial class DvDonViTinhSearchModel :BaseSearchModel {
        public DvDonViTinhSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class DvDonViTinhListModel : BasePagedListModel<DvDonViTinhModel>
    {
       
    }
}

