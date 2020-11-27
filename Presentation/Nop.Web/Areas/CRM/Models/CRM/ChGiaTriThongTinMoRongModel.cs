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
    
	public class ChGiaTriThongTinMoRongModel :BaseNopEntityModel
	{
        public ChGiaTriThongTinMoRongModel(){
        
        }
		public Int32? THONG_TIN_MO_RONG_ID {get;set;}
		public Int32? DOI_TUONG_ID {get;set;}
		public Int32? LOAI_DOI_TUONG {get;set;}
		public String GIA_TRI {get;set;}
	}
    public partial class ChGiaTriThongTinMoRongSearchModel :BaseSearchModel {
        public ChGiaTriThongTinMoRongSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class ChGiaTriThongTinMoRongListModel : BasePagedListModel<ChGiaTriThongTinMoRongModel>
    {
       
    }
}

