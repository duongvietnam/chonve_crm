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
    
	public class GdGiaoDichKhachHangMapModel :BaseNopEntityModel
	{
        public GdGiaoDichKhachHangMapModel(){
        
        }
		public Int32? KHACH_HANG_ID {get;set;}
		public Int32? GIAO_DICH_ID {get;set;}
		public Int32? DICH_VU_ID {get;set;}
		public Int32? KHACH_HANG_CHINH {get;set;}
	}
    public partial class GdGiaoDichKhachHangMapSearchModel :BaseSearchModel {
        public GdGiaoDichKhachHangMapSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class GdGiaoDichKhachHangMapListModel : BasePagedListModel<GdGiaoDichKhachHangMapModel>
    {
       
    }
}

