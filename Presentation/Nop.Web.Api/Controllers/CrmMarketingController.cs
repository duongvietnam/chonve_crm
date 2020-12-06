using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.CRM;
using Nop.Web.Api.Models.CRM;

namespace Nop.Web.Api.Controllers
{
    public class CrmMarketingController : BaseApiController
    {
        private readonly IMarMarketingService _marMarketingService;

        public CrmMarketingController(IMarMarketingService marMarketingService)
        {
            this._marMarketingService = marMarketingService;
        }

        public IActionResult ApDungChuongTrinhMarketing(int hangKhachHang, Decimal donGia, DateTime ngayGiaoDich, int dichVuId)
        {
            var item = _marMarketingService.LocDieuKienMarketing(hangKhachHang, donGia, ngayGiaoDich, dichVuId);

            return Ok(item);
        }

        public IActionResult ApDungPhieuGiamGia(IList<string> listMa, int doanhNghiepId, int khachHangId, int giaoDichId, DateTime ngayGiaoDich)
        {
            var item = _marMarketingService.ApDungMaGiamGia(listMa, doanhNghiepId, khachHangId, giaoDichId, ngayGiaoDich);

            return Ok(item);
        }

        public IActionResult CheckMaGiamGia(string maGiamGia, int doanhNghiepId, decimal donGia, DateTime ngayGiaoDich)
        {
            var item = _marMarketingService.CheckMaGiamGia(maGiamGia, doanhNghiepId, donGia, ngayGiaoDich);

            return Ok(item);
        }
    }
}
