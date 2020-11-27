//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.CRM.Models.CRM
{
    
	public class MarMarketingHeThongMapModel :BaseNopEntityModel
	{
        public MarMarketingHeThongMapModel(){
        
        }
		public Int32? MARKETING_ID {get;set;}
		public Int32? MAR_HE_THONG_ID {get;set;}
	}
    public partial class MarMarketingHeThongMapSearchModel :BaseSearchModel {
        public MarMarketingHeThongMapSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class MarMarketingHeThongMapListModel : BasePagedListModel<MarMarketingHeThongMapModel>
    {
       
    }
}

