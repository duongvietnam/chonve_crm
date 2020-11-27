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
    
	public class KhNhomHeThongMapModel :BaseNopEntityModel
	{
        public KhNhomHeThongMapModel(){
        
        }
		public Int32? NHOM_KHACH_HANG_ID {get;set;}
		public Int32? NHOM_KHACH_HANG_HE_THONG {get;set;}
	}
    public partial class KhNhomHeThongMapSearchModel :BaseSearchModel {
        public KhNhomHeThongMapSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class KhNhomHeThongMapListModel : BasePagedListModel<KhNhomHeThongMapModel>
    {
       
    }
}

