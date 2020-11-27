//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 19/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.CRM.Models.CRM
{
    
	public class MarMarketingHeThongModel :BaseNopEntityModel
	{
        public MarMarketingHeThongModel(){
        
        }
		public String TEN {get;set;}
		public String MA {get;set;}
	}
    public partial class MarMarketingHeThongSearchModel :BaseSearchModel {
        public MarMarketingHeThongSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class MarMarketingHeThongListModel : BasePagedListModel<MarMarketingHeThongModel>
    {
       
    }
}

