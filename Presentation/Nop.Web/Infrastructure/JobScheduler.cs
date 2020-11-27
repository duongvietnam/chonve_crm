using Nop.Core.Configuration;
using Nop.Web.Areas.CRM.Tasks;
using Quartz;
using Quartz.Impl;

namespace Nop.Web.Infrastructure
{
    public class JobScheduler
    {
        #region Fields

        private NopConfig _nopConfig { get; set; }

        #endregion Fields
        #region Properties

        /// <summary>
        /// Gets the task manger instance
        /// </summary>
        public static JobScheduler Instance { get; } = new JobScheduler();

        #endregion Properties
        #region Ctor

        private JobScheduler()
        {
        }
        public void Initialize(NopConfig nopConfig)
        {
            this._nopConfig = nopConfig;
        }
        #endregion Ctor
        public async void Start()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            IScheduler scheduler = await schedFact.GetScheduler();
            await scheduler.Start();

            #region Job getData, chạy mỗi 30 phút 1 lần
            IJobDetail jobGetData = JobBuilder.Create<SchedulerApiGetData>()
                .Build();

            ITrigger triggerGetData = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(30).RepeatForever())
                .Build();

            await scheduler.ScheduleJob(jobGetData, triggerGetData);
            #endregion

            #region Job phân hạng khách hàng, chạy vào 23:00:00 hàng ngày
            IJobDetail jobPhanHangKhachHang = JobBuilder.Create<SchedulerPhanHangKhachHang>()
                .Build();

            ITrigger triggerPhanHangKhachHang = TriggerBuilder.Create()
                .StartAt(DateBuilder.DateOf(23, 00, 00)) // some Date 
                .ForJob(jobPhanHangKhachHang) // identify job with name, group strings
                .Build();

            await scheduler.ScheduleJob(jobPhanHangKhachHang, triggerPhanHangKhachHang);
            #endregion
        }
    }
}
