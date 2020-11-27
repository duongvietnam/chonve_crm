using Microsoft.AspNetCore.Mvc;
using Nop.Services.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Api.Controllers
{
    public class TestController: BaseApiController
    {
        #region Fields
        private readonly IBankService _bankService;
        #endregion

        #region Ctor
        public TestController(           
            IBankService itemService
            )
        {
           
            this._bankService = itemService;
        }
        #endregion
        [HttpGet]
        public IActionResult Ping(string s)
        {
            return Ok("Hello world: "+s);
        }
        [HttpGet]
        public IActionResult GetAllBanks()
        {
            return Ok(_bankService.GetAllBanks());
        }
    }
}
