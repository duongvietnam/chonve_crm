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

    public class KhCauHinhMoRongModel : BaseNopEntityModel
    {
        public KhCauHinhMoRongModel()
        {
            DDLCauHinhCha = new List<SelectListItem>();
        }
        public Int32? PARENT_ID { get; set; }
        public String TEN { get; set; }
        public Int32? LOAI_DU_LIEU { get; set; }
        public Int32? DOANH_NGHIEP_ID { get; set; }
        // more
        public SelectList DLLLoaiDuLieu { get; set; }
        public IList<SelectListItem> DDLCauHinhCha { get; set; }
    }
    public partial class KhCauHinhMoRongSearchModel : BaseSearchModel
    {
        public KhCauHinhMoRongSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class KhCauHinhMoRongListModel : BasePagedListModel<KhCauHinhMoRongModel>
    {

    }
}

