namespace Nop.Services.DanhMuc
{
    /// <summary>
    /// Service nay su dung de mapping lay thong tin tu cache, tang hieu nang 
    /// </summary>
    public interface IDanhMucCacheService
    {
        string GetUserName(int? Id);
        string GetTenNganHang(int? Id);
        string GetTenCRMLoaiDichVu(int? Id);
        string GetTenCRMNhomDichVu(int? Id);
    }
}
