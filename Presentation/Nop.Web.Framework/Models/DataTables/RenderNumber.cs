using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Web.Framework.Models.DataTables
{
    public enum EnumRenderNumberFormat
    {
        NUMBER = 1,
        FLOAT = 2,
        PERCENT = 3,
        PRICE=4
    }
    public class RenderNumber : IRender
    {
        public RenderNumber()
        {
            //set default values
            FormatNum = EnumRenderNumberFormat.NUMBER;
        }
        public RenderNumber(EnumRenderNumberFormat _formatNum)
        {
            FormatNum = _formatNum;
        }
        public EnumRenderNumberFormat FormatNum { get; set; }
    }
}
