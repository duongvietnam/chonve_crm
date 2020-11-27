//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;

namespace Nop.Web.Areas.CRM.Models.CRM
{
    
	public class KhNhomKhachHangModel :BaseNopEntityModel
	{
        public KhNhomKhachHangModel()
        {
            DDLNhomHeThong = new List<SelectListItem>();
            NhomHeThongIds = new List<int>();
        }
		public String TEN {get;set;}
		public Int32? DOANH_NGHIEP_ID {get;set;}
        // more
        public IList<int> NhomHeThongIds { get; set; }
        public IList<SelectListItem> DDLNhomHeThong { get; set; }
    }

    public partial class KhNhomKhachHangSearchModel :BaseSearchModel {
        public KhNhomKhachHangSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }

    public partial class KhNhomKhachHangListModel : BasePagedListModel<KhNhomKhachHangModel>
    {

    }
}

