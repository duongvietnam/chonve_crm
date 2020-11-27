using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Services.BaoCao
{
    public class BaoCaoCauHinh
    {
        public CellInfo[] headerInfos { get; set; }
        public CellInfo[] footerInfos { get; set; }
        public int startDataRow { get; set; }
    }
    public class CellInfo
    {
        /// <summary>
        /// A1:R1
        /// </summary>
        public string CellRange { get; set; }
        /// <summary>
        /// Them vi tri tuong doi trong viec dinh vi row tren excel
        /// Vi du: sau khi row data ve xong thi moi biet dc row tiep theo de ve header la gi thi khi do row de ve tiep= row+RowOffset
        /// </summary>
        public int RowOffset { get; set; }
        /// <summary>
        /// Y nghia nhu Rowoffset
        /// </summary>
        public int ColOffset { get; set; }
        public string Val { get; set; }
        public bool isWrapText { get; set; }
        public bool isFontBold { get; set; }
        public bool isFontItalic { get; set; }
        public float FontSize { get; set; }
        public float RowHeight { get; set; }
        public int HorizontalAlignmentId { get; set; }
        public ExcelHorizontalAlignment horizontalAlignment { get { return (ExcelHorizontalAlignment)HorizontalAlignmentId; } }
        public int VerticalAlignmentId { get; set; }
        public ExcelVerticalAlignment verticalAlignment { get { return (ExcelVerticalAlignment)VerticalAlignmentId; } }
    }
}
