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
    
	public class DvDieuChinhGiaModel :BaseNopEntityModel
	{
        public DvDieuChinhGiaModel(){
        
        }
		public DateTime? NGAY_DIEU_CHINH {get;set;}
		public Int32? DICH_VU_ID {get;set;}
		public Decimal? GIA_BAN_BUON {get;set;}
		public Decimal? GIA_BAN_LE {get;set;}
		public Decimal? GIA_1 {get;set;}
		public Decimal? GIA_2 {get;set;}
		public Decimal? GIA_3 {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public Int32? NGUOI_TAO {get;set;}
	}
    public partial class DvDieuChinhGiaSearchModel :BaseSearchModel {
        public DvDieuChinhGiaSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class DvDieuChinhGiaListModel : BasePagedListModel<DvDieuChinhGiaModel>
    {
       
    }
}

