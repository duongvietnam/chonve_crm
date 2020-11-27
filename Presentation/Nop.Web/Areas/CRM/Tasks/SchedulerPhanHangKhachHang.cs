using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Services.Stores;
using Quartz;

namespace Nop.Web.Areas.CRM.Tasks
{
    /// <summary>
    /// chạy hàng đêm để phân hạng khách hàng
    /// </summary>
    public class SchedulerPhanHangKhachHang : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var _dataProvider = EngineContext.Current.Resolve<INopDataProvider>();
            var _storeService = EngineContext.Current.Resolve<IStoreService>();
            List<int> ListDoanhNghiepId = _storeService.GetAllStores().Select(c => c.Id).ToList();
            foreach (int DoanhNghiepId in ListDoanhNghiepId)
            {
                // exec procedure [CRM_PhanNhomKhachHang_HangNgay]
                var pStoreId = SqlParameterHelper.GetInt32Parameter("StoreId", DoanhNghiepId);
                _dataProvider.ExecuteStoredProcedure("CRM_PhanNhomKhachHang_HangNgay", pStoreId);
            }
        }
    }
}
