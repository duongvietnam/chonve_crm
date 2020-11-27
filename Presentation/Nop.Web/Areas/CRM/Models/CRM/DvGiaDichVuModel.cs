//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using Nop.Web.Framework.Models;
using System;

namespace Nop.Web.Areas.CRM.Models.CRM
{

    public class DvGiaDichVuModel : BaseNopEntityModel
    {
        public DvGiaDichVuModel()
        {

        }
        public Int32? DICH_VU_ID { get; set; }
        public Decimal? GIA_BAN_BUON { get; set; }
        public Decimal? GIA_BAN_LE { get; set; }
        public Decimal? GIA_1 { get; set; }
        public Decimal? GIA_2 { get; set; }
        public Decimal? GIA_3 { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Int32? NGUOI_TAO { get; set; }
    }
    public partial class DvGiaDichVuSearchModel : BaseSearchModel
    {
        public DvGiaDichVuSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class DvGiaDichVuListModel : BasePagedListModel<DvGiaDichVuModel>
    {

    }
}

