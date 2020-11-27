using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Core.Domain.BaoCao
{

    /// <summary>
    /// Lớp này sử dụng chứa thông tin thống kê để vẽ biểu đồ, gồ các cặp thông tin: 
    /// (SoLuong;GiaTri); (SoLuong1;GiaTri1); (SoLuong2;GiaTri2)
    /// SoLuong, Giá trị 1 hoac 2 su dung trong truong hop lay them thong tin để vẽ bieu do so sanh
    /// </summary>
    public class BieuDo: ViewEntity
    {
        /// <summary>
        /// Số thứ tự hoặc Id dữ liệu
        /// </summary>
        public int Id { get; set; }
        public string Nhan { get; set; }
        public string Nhan1 { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaTri { get; set; }
        public int SoLuong1 { get; set; }
        public decimal GiaTri1 { get; set; }
        public int SoLuong2 { get; set; }
        public decimal GiaTri2 { get; set; }
    }
    public class BieuDo1:ViewEntity
    {
        /// <summary>
        /// Số thứ tự hoặc Id dữ liệu. SoLuong kieu so thuc
        /// </summary>
        public int Id { get; set; }
        public string Nhan { get; set; }
        public decimal SoLuong { get; set; }
        public decimal GiaTri { get; set; }
    }
}
