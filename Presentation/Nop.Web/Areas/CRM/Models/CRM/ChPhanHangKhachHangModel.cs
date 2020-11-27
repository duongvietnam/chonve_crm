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
    
	public class ChPhanHangKhachHangModel :BaseNopEntityModel
	{
        public ChPhanHangKhachHangModel(){
        
        }
		public String TEN {get;set;}
		public String MA {get;set;}
		public Int32? GIA_TRI_CAP_1 {get;set;}
		public Int32? GIA_TRI_CAP_2 {get;set;}
		public Int32? GIA_TRI_CAP_3 {get;set;}
		public String TEN_CAP_1 {get;set;}
		public String TEN_CAP_2 {get;set;}
		public String TEN_CAP_3 {get;set;}
		public Int32? ACTIVE {get;set;}
		public String MO_TA {get;set;}
	}
    public partial class ChPhanHangKhachHangSearchModel :BaseSearchModel {
        public ChPhanHangKhachHangSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class ChPhanHangKhachHangListModel : BasePagedListModel<ChPhanHangKhachHangModel>
    {
       
    }
}

