//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 4/11/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.CRM.Models.CRM
{
    
	public class ChThongTinMoRongModel :BaseNopEntityModel
	{
        public ChThongTinMoRongModel(){
        
        }
		public String TEN {get;set;}
		public String MA {get;set;}
		public Int32 DOANH_NGHIEP_ID {get;set;}
		public String MO_TA {get;set; }
        public Int32? KIEU_DU_LIEU { get; set; }
    }
    public partial class ChThongTinMoRongSearchModel :BaseSearchModel {
        public ChThongTinMoRongSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class ChThongTinMoRongListModel : BasePagedListModel<ChThongTinMoRongModel>
    {
       
    }
}

