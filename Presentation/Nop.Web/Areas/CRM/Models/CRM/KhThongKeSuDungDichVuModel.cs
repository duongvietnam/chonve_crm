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
    
	public class KhThongKeSuDungDichVuModel :BaseNopEntityModel
	{
        public KhThongKeSuDungDichVuModel(){
        
        }
		public Int32? KHACH_HANG_ID {get;set;}
		public String TOP_DICH_VU {get;set;}
		public String TOP_DICH_VU_30DAYS {get;set;}
		public String TOP_DICH_VU_60DAYS {get;set;}
		public String TOP_DICH_VU_90DAYS {get;set;}
		public String TOP_DICH_VU_LE_TET {get;set;}
	}
    public partial class KhThongKeSuDungDichVuSearchModel :BaseSearchModel {
        public KhThongKeSuDungDichVuSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class KhThongKeSuDungDichVuListModel : BasePagedListModel<KhThongKeSuDungDichVuModel>
    {
       
    }
}

