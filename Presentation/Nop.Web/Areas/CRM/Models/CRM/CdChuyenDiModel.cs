//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 26/11/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.CRM.Models.CRM
{
    
	public class CdChuyenDiModel :BaseNopEntityModel
	{
        public CdChuyenDiModel(){
        
        }
		public String MA {get;set;}
		public String BIEN_SO_XE {get;set;}
		public Int32? DICH_VU_ID {get;set;}
		public String TEN_LOAI_XE {get;set;}
		public String TEN_LAI_XE {get;set;}
		public Int32? SO_KHACH {get;set;}
		public Int32? SO_GHE {get;set;}
	}
    public partial class CdChuyenDiSearchModel :BaseSearchModel {
        public CdChuyenDiSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class CdChuyenDiListModel : BasePagedListModel<CdChuyenDiModel>
    {
       
    }
}

