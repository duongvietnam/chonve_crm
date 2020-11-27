using Nop.Services.CRM;
using Nop.Services.Customers;

namespace Nop.Services.DanhMuc
{
    public class DanhMucCacheService : IDanhMucCacheService
    {
        #region Fields
        private readonly IBankService _bankService;
        private readonly ICustomerService _customerService;
        private readonly IDvLoaiDichVuService _dvLoaiDichVuService;
        private readonly IDvNhomDichVuService _dvNhomDichVuService;
        #endregion

        #region Ctor
        public DanhMucCacheService(IBankService bankService,
            ICustomerService customerService,
            IDvLoaiDichVuService dvLoaiDichVuService,
            IDvNhomDichVuService dvNhomDichVuService
            )
        {
            this._customerService = customerService;
            this._bankService = bankService;
            this._dvLoaiDichVuService = dvLoaiDichVuService;
            this._dvNhomDichVuService = dvNhomDichVuService;
        }
        #endregion

        public string GetUserName(int? Id)
        {
            var item = _customerService.GetCustomerById(Id.GetValueOrDefault(0));
            if (item == null)
                return "";
            return item.Username;
        }
        public string GetTenNganHang(int? Id)
        {
            var item = _bankService.GetBankById(Id.GetValueOrDefault(0));
            if (item == null)
                return "";
            return string.Format("{0} - {1}",item.Code, item.Name);
        }
        public string GetTenCRMLoaiDichVu(int? Id)
        {
            var item = _dvLoaiDichVuService.GetDvLoaiDichVuById(Id.GetValueOrDefault(0));
            if (item == null)
                return "";
            return item.TEN;
        }
        public string GetTenCRMNhomDichVu(int? Id)
        {
            var item = _dvNhomDichVuService.GetDvNhomDichVuById(Id.GetValueOrDefault(0));
            if (item == null)
                return "";
            return item.TEN;
        }
    }
}
