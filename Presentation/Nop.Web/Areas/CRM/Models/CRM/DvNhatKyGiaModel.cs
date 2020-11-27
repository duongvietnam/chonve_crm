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
    
	public class DvNhatKyGiaModel :BaseNopEntityModel
	{
        public DvNhatKyGiaModel(){
        
        }
		public Int32? DICH_VU_ID {get;set;}
		public DateTime? NGAY_SUA {get;set;}
		public Decimal? GIA_BAN_BUON {get;set;}
		public Decimal? GIA_BAN_LE {get;set;}
		public Decimal? GIA_1 {get;set;}
		public Decimal? GIA_2 {get;set;}
		public Decimal? GIA_3 {get;set;}
	}
    public partial class DvNhatKyGiaSearchModel :BaseSearchModel {
        public DvNhatKyGiaSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class DvNhatKyGiaListModel : BasePagedListModel<DvNhatKyGiaModel>
    {
       
    }
}

