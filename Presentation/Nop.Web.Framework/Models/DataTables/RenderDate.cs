using Microsoft.SqlServer.Server;

namespace Nop.Web.Framework.Models.DataTables
{
    public enum EnumRenderDateFormat
    {
        FULL_TIME=1,
        NO_TIME=2,
        CUT_TIME=3
    }
    /// <summary>
    /// Represents date render for DataTables column
    /// </summary>
    public partial class RenderDate : IRender
    {
        #region Constants

        /// <summary>
        /// Default date format
        /// </summary>
        private string DEFAULT_DATE_FORMAT = "DD/MM/YYYY HH:mm:ss";
        #endregion

        #region Ctor

        public RenderDate()
        {
            //set default values
            Format = DEFAULT_DATE_FORMAT;
        }
        public RenderDate(EnumRenderDateFormat formatDate)
        {
            switch(formatDate)
            {
                case EnumRenderDateFormat.FULL_TIME:
                    this.Format = DEFAULT_DATE_FORMAT;
                    break;
                case EnumRenderDateFormat.NO_TIME:
                    this.Format = "DD/MM/YYYY";
                    break;
                case EnumRenderDateFormat.CUT_TIME:
                    this.Format = "DD/MM/YY HH:mm";
                    break;
            }    
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets format date (moment.js)
        /// See also "http://momentjs.com/"
        /// </summary>
        public string Format { get; set; }

        #endregion
    }
}