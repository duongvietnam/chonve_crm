using Microsoft.AspNetCore.Mvc;
using Nop.Services.CRM;
using Nop.Web.Api.Models.CRM;

namespace Nop.Web.Api.Controllers
{
    public class CrmMarketingController : BaseApiController
    {
        private readonly IMarMarketingService _marMarketingService;
        private readonly IMarMarketingDieuKienService _marMarketingDieuKienService;

        public CrmMarketingController(IMarMarketingService marMarketingService,
            IMarMarketingDieuKienService marMarketingDieuKienService)
        {
            this._marMarketingService = marMarketingService;
            this._marMarketingDieuKienService = marMarketingDieuKienService;
        }

        [HttpPost]
        public IActionResult ApDungChuongTrinhMarketing([FromBody] GiaoDichModel model)
        {
            var item = _marMarketingService.LocDieuKienMarketing(model.HangKhachHang, model.DonGia, model.NgayGiaoDich, model.DichVuId);

            return Ok(item);
        }
    }
}
