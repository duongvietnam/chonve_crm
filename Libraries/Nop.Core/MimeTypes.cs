namespace Nop.Core
{
    public enum MimeTypeGroup
    {
        Application,
        Image,
        Text
    }
    /// <summary>
    /// Collection of MimeType Constants for using to avoid Typos
    /// If needed MimeTypes missing feel free to add
    /// </summary>
    public static class MimeTypes
    {
        public static MimeTypeGroup getMimeTypeGroup(this string inputType)
        {
            switch (inputType)
            {
                case ImageBmp:
                case ImageGif:
                case ImageJpeg:
                case ImagePJpeg:
                case ImagePng:
                case ImageTiff:
                    return MimeTypeGroup.Image;
                case TextCss:
                case TextCsv:
                case TextJavascript:
                case TextPlain:
                case TextXlsx:
                case TextDocx:
                case ApplicationPdf:
                    return MimeTypeGroup.Text;
            }
            return MimeTypeGroup.Application;
        }
        #region application/*

        /// <summary>
        /// Type
        /// </summary>
        public const string ApplicationForceDownload = "application/force-download";

        /// <summary>
        /// Type
        /// </summary>
        public const string ApplicationJson = "application/json";

        /// <summary>
        /// Type
        /// </summary>
        public const string ApplicationManifestJson = "application/manifest+json";

        /// <summary>
        /// Type
        /// </summary>
        public const string ApplicationOctetStream = "application/octet-stream";

        /// <summary>
        /// Type
        /// </summary>
        public const string ApplicationPdf = "application/pdf";

        /// <summary>
        /// Type
        /// </summary>
        public const string ApplicationRssXml = "application/rss+xml";

        /// <summary>
        /// Type
        /// </summary>
        public const string ApplicationXml = "application/xml";

        /// <summary>
        /// Type
        /// </summary>
        public const string ApplicationXWwwFormUrlencoded = "application/x-www-form-urlencoded";

        /// <summary>
        /// Type
        /// </summary>
        public const string ApplicationXZipCo = "application/x-zip-co";

        #endregion

        #region image/*

        /// <summary>
        /// Type
        /// </summary>
        public const string ImageBmp = "image/bmp";

        /// <summary>
        /// Type
        /// </summary>
        public const string ImageGif = "image/gif";

        /// <summary>
        /// Type
        /// </summary>
        public const string ImageJpeg = "image/jpeg";

        /// <summary>
        /// Type
        /// </summary>
        public const string ImagePJpeg = "image/pjpeg";

        /// <summary>
        /// Type
        /// </summary>
        public const string ImagePng = "image/png";

        /// <summary>
        /// Type
        /// </summary>
        public const string ImageTiff = "image/tiff";

        #endregion

        #region text/*

        /// <summary>
        /// Type
        /// </summary>
        public const string TextCss = "text/css";

        /// <summary>
        /// Type
        /// </summary>
        public const string TextCsv = "text/csv";

        /// <summary>
        /// Type
        /// </summary>
        public const string TextJavascript = "text/javascript";

        /// <summary>
        /// Type
        /// </summary>
        public const string TextPlain = "text/plain";

        /// <summary>
        /// Type
        /// </summary>
        public const string TextXlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        /// <summary>
        /// Type office doc
        /// </summary>
        public const string TextDocx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        #endregion
    }
}