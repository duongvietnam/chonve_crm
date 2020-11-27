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
    
	public class DvLoaiDichVuModel :BaseNopEntityModel
	{
        public DvLoaiDichVuModel(){
        
        }
		public String TEN {get;set;}
	}
    public partial class DvLoaiDichVuSearchModel :BaseSearchModel {
        public DvLoaiDichVuSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class DvLoaiDichVuListModel : BasePagedListModel<DvLoaiDichVuModel>
    {
       
    }
}
