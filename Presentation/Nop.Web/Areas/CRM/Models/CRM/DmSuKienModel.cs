//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 2/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.CRM.Models.CRM
{
    
	public class DmSuKienModel :BaseNopEntityModel
	{
        public DmSuKienModel(){
        
        }
		public String TEN {get;set;}
		public String MA {get;set;}
		public DateTime? NGAY_BAT_DAU {get;set;}
		public DateTime? NGAY_KET_THUC {get;set;}
		public Int32? DOANH_NGHIEP_ID {get;set;}
	}
    public partial class DmSuKienSearchModel :BaseSearchModel {
        public DmSuKienSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class DmSuKienListModel : BasePagedListModel<DmSuKienModel>
    {
       
    }
}

