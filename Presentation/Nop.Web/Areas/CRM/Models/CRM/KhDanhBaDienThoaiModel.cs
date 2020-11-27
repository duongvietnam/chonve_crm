//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using System;

namespace Nop.Web.Areas.CRM.Models.CRM
{

    public class KhDanhBaDienThoaiModel :BaseNopEntityModel
	{
        public KhDanhBaDienThoaiModel(){
        
        }
		public Int32? KHACH_HANG_ID {get;set;}
        //[UIHint("Mobile")]
        public String SO_DIEN_THOAI {get;set;}
		public Int32? NHOM_DANH_BA {get;set;}
		public Int32? TRANG_THAI {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public Int32? NGUOI_TAO {get;set;}
        // more
        public SelectList DDLNhomSoDienThoai { get; set; }
    }
    public partial class KhDanhBaDienThoaiSearchModel :BaseSearchModel {
        public KhDanhBaDienThoaiSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class KhDanhBaDienThoaiListModel : BasePagedListModel<KhDanhBaDienThoaiModel>
    {
       
    }
}

