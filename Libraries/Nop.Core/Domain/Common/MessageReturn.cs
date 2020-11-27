using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Core.Domain.Common
{
    public class MessageReturn
    {
        public const string SUCCESS_CODE = "00";
        public const string ERROR_CODE = "01";
        public const string EXISTS_CODE = "02";
        public const string NOT_FOUND_CODE = "04";
        public MessageReturn() { }
        public MessageReturn(string _code, string _msg)
        {
            Code = _code;
            Message = _msg;
        }

        public string Code { get; set; }
        public string Message { get; set; }
        public string IdRecord { get; set; }
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Co the luu struct 1 object, hoac 1 list object
        /// </summary>
        public object ObjectInfo { get; set; }

        public static MessageReturn CreateSuccessMessage(string _msg, object _objectInfo = null)
        {
            var msgret = new MessageReturn(SUCCESS_CODE, _msg);
            msgret.ObjectInfo = _objectInfo;
            msgret.IsSuccess = true;
            return msgret;
        }
        public static MessageReturn CreateErrorMessage(string _msg, object _objectInfo = null)
        {
            var msgret = new MessageReturn(ERROR_CODE, _msg);
            msgret.ObjectInfo = _objectInfo;
            msgret.IsSuccess = false;
            return msgret;
        }
        public static MessageReturn CreateExistsMessage(string _msg, object _objectInfo = null)
        {
            var msgret = new MessageReturn(EXISTS_CODE, _msg);
            msgret.ObjectInfo = _objectInfo;
            msgret.IsSuccess = false;
            return msgret;
        }
        public static MessageReturn CreateNotFoundMessage(string _msg, object _objectInfo = null)
        {
            var msgret = new MessageReturn(NOT_FOUND_CODE, _msg);
            msgret.ObjectInfo = _objectInfo;
            msgret.IsSuccess = false;
            return msgret;
        }
    }
}
