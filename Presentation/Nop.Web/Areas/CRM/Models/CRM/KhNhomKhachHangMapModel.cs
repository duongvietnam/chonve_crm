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
    
	public class KhNhomKhachHangMapModel :BaseNopEntityModel
	{
        public KhNhomKhachHangMapModel(){
        
        }
		public Int32? KHACH_HANG_ID {get;set;}
		public Int32? NHOM_KHACH_HANG_ID {get;set;}
		public Int32? DIEM_DANH_GIA {get;set;}
	}
    public partial class KhNhomKhachHangMapSearchModel :BaseSearchModel {
        public KhNhomKhachHangMapSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class KhNhomKhachHangMapListModel : BasePagedListModel<KhNhomKhachHangMapModel>
    {
       
    }
}

