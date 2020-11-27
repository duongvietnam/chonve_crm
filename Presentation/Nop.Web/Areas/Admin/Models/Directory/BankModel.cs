//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 31/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Directory
{
	public class BankModel :BaseNopEntityModel
	{
        public BankModel(){
        }
		public String Code {get;set;}
		public String Name {get;set;}
        public String ShortName { get; set; }
        public Int32 OrderDisplay {get;set;}
		public String CodePlus {get;set;}
        
	}
    public partial class BankSearchModel :BaseSearchModel {
        public BankSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class BankListModel : BasePagedListModel<BankModel>
    {
       
    }
}

