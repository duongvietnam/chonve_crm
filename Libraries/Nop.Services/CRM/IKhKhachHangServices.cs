//----------------------------------------------------------------------------------------------------------------------
// Create by       : TBOffice template 1.0 
// Template create : TBOffice
// Create date     : 9/9/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Domain.BaoCao;
using Nop.Core.Domain.CRM;


namespace Nop.Services.CRM
{
    public partial interface IKhKhachHangService 
    {    
    #region KhKhachHang
        IList<KhKhachHang> GetAllKhKhachHangs(int StoreId=0);
        IPagedList <KhKhachHang> SearchKhKhachHangs(int StoreId=0,string Keysearch = null,int pageIndex = 0, int pageSize = int.MaxValue, string email = null, Int32? gender = null, Int32? floor = null, Int32? top = null, Int32? type = null, Int32? daytime = null);
        KhKhachHang GetKhKhachHangById(int Id);
        IList<KhKhachHang> GetKhKhachHangByIds(int[] Ids);
        IQueryable<KhKhachHang> GetQueryableKhachHang(int doanhNgiepId);
        void InsertKhKhachHang(KhKhachHang entity);
        void UpdateKhKhachHang(KhKhachHang entity);
        void DeleteKhKhachHang(KhKhachHang entity);
        IPagedList<KhKhachHang> SearchPhanTichKhachHang(int StoreId = 0, string Keysearch = null, int pageIndex = 0, int pageSize = int.MaxValue, bool isHangKhachHang = false, bool isTangDan = true, int HangKhachHang = 0, bool isNhomKhachHang = false, int nhomKhachHang = 0, bool isThoiQuen = false, int TuDiem = 0, int DenDiem = 0, int dichVuId = 0);
        IList<BieuDo> GetThongKeKhachHangTheoPhanHang(int StoreId, int IdTieuChiPH);
        #endregion
    }
}
