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
    
	public class KhThongTinMoRongModel :BaseNopEntityModel
	{
        public KhThongTinMoRongModel(){
        
        }
		public Int32? KHACH_HANG_ID {get;set;}
		public Int32? CAU_HINH_ID {get;set;}
		public String VALUE {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public Int32? NGUOI_TAO {get;set;}
	}
    public partial class KhThongTinMoRongSearchModel :BaseSearchModel {
        public KhThongTinMoRongSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class KhThongTinMoRongListModel : BasePagedListModel<KhThongTinMoRongModel>
    {
       
    }
}

