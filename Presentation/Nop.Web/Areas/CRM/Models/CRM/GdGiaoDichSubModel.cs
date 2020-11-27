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

    public class GdGiaoDichSubModel : BaseNopEntityModel
    {
        public GdGiaoDichSubModel()
        {

        }
        public Int32? GIAO_DICH_ID { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public Int32? DICH_VU_ID { get; set; }
        public Int32? DON_VI_TINH { get; set; }
        public Decimal? SO_TIEN { get; set; }
        public Int32? DIEM_DICH_VU { get; set; }
    }
    public partial class GdGiaoDichSubSearchModel : BaseSearchModel
    {
        public GdGiaoDichSubSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class GdGiaoDichSubListModel : BasePagedListModel<GdGiaoDichSubModel>
    {

    }
}

