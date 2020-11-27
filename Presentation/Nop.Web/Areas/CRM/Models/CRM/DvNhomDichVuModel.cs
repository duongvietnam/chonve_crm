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
    
	public class DvNhomDichVuModel :BaseNopEntityModel
	{
        public DvNhomDichVuModel(){
        
        }
		public String TEN {get;set;}
	}
    public partial class DvNhomDichVuSearchModel :BaseSearchModel {
        public DvNhomDichVuSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class DvNhomDichVuListModel : BasePagedListModel<DvNhomDichVuModel>
    {
       
    }
}

