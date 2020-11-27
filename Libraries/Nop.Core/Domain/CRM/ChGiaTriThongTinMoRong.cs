//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 4/11/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace Nop.Core.Domain.CRM
{
    public partial class ChGiaTriThongTinMoRong : BaseEntity
    {
        public Int32? THONG_TIN_MO_RONG_ID { get; set; }
        public Int32? DOI_TUONG_ID { get; set; }
        public Int32? LOAI_DOI_TUONG { get; set; }
        public String GIA_TRI { get; set; }

    }

    public enum EnumLoaiDoiTuong
    {
        DichVu = 1,
        GiaoDich = 2
    }
}



