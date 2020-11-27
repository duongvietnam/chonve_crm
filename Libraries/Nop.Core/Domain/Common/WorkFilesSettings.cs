using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Core.Domain.Common
{
    /// <summary>
    /// Những cấu hình chung của phần quản lý nhân sự
    /// </summary>
    public class WorkfilesSettings : ISettings
    {
        /// <summary>
        /// So luong toi da upload file trong 1 transaction
        /// </summary>
        public int SoLuongToiDa { get; set; }
        /// <summary>
        /// dung luong toi da cho 1 file, tinh bang M
        /// </summary>
        public int DungLuongToiDa { get; set; }

    }
}
