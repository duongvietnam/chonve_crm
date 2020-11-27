//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 10/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;
using Nop.Core;
using Nop.Web.Extensions;

namespace Nop.Web.Models.Media
{
    
	public class WorkfilesModel :BaseNopEntityModel
	{
        public WorkfilesModel(){
        
        }
		public String Guid {get;set;}
		public String Ten {get;set;}
		public Int32 LoaiFileId {get;set;}
		public Int32 CustomerId {get;set;}
        public string TenNguoiTao { get; set; }
        public DateTime NgayTao {get;set;}
		public String DuoiFile {get;set;}
		public String DataJson {get;set;}
		public Boolean IsDeleted {get;set;}
		public Decimal KichThuoc {get;set;}
        
        public String LoaiNoiDung { get; set; }
        public MimeTypeGroup mimeTypeGroup
        {
            get { return LoaiNoiDung.getMimeTypeGroup(); }
        }
        public string ContentLengthText
        {
            get
            {
                if (KichThuoc > 1024m)
                    return Convert.ToDecimal(KichThuoc / 1024m).ToString("###.#0") + "M";
                return KichThuoc.ToString() + "KB";
            }
        }
        /// <summary>
        /// Lay thong tin ten file + duoi file
        /// </summary>
        public string TenDayDu
        {
            get
            {
                return string.Format("{0}{1}", Ten, DuoiFile);
            }
        }
        public string UrlDownload
        {
            get
            {
                return this.Guid.ToUrlDownload();
            }
        }
    }
    public partial class WorkfilesSearchModel :BaseSearchModel {
        public WorkfilesSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class WorkfilesListModel : BasePagedListModel<WorkfilesModel>
    {
       
    }
}

