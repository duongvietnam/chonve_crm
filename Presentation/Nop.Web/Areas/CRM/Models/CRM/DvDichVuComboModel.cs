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

    public class DvDichVuComboModel : BaseNopEntityModel
    {
        public DvDichVuComboModel()
        {

        }
        public Int32? DICH_VU_COMBO { get; set; }
        public Int32? DICH_VU_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Int32? NGUOI_TAO { get; set; }
        public Int32? SO_LUONG { get; set; }
    }
    public partial class DvDichVuComboSearchModel : BaseSearchModel
    {
        public DvDichVuComboSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class DvDichVuComboListModel : BasePagedListModel<DvDichVuComboModel>
    {

    }
}

