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
    
	public class GdGiaoDichDiemModel :BaseNopEntityModel
	{
        public GdGiaoDichDiemModel(){
        
        }
		public Int32? GIAO_DICH_ID {get;set;}
		public Int32? DIEM_DICH_VU {get;set;}
		public Int32? DIEM_TICH_LUY {get;set;}
		public Int32? KHACH_HANG_ID {get;set;}
		public Int32? CONG_TRU {get;set;}
	}
    public partial class GdGiaoDichDiemSearchModel :BaseSearchModel {
        public GdGiaoDichDiemSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class GdGiaoDichDiemListModel : BasePagedListModel<GdGiaoDichDiemModel>
    {
       
    }
}

