using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using NLog;
using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Web.Api.Infrastructure
{
    public interface IApiLog
    {
        void Information(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(string message);
        void Trace(HttpRequest request, string methodName, object paraInput);
    }
    public class NLogger : IApiLog
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        public NLogger()
        {
        }
        public void Trace(HttpRequest request, string methodName, object paraInput)
        {
            var message = new StringBuilder();


            if (request != null)
            {
                message.Append("Method: " + request.Method + Environment.NewLine);
                message.Append("URL: " + request.Path.Value + Environment.NewLine);
                StringValues authorizationToken;
                request.Headers.TryGetValue("Authorization", out authorizationToken);
                if (!string.IsNullOrEmpty(authorizationToken.ToString()))
                {
                    message.Append("Token: " + authorizationToken.ToString() + Environment.NewLine);
                }

            }
            message.Append("Method Name: " + methodName + Environment.NewLine);
            message.Append("Model Input: " + paraInput.toStringJson());

            Information(message.ToString());
        }
        public void Information(string message)
        {
            _logger.Info(message);
        }

        public void Warning(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }
    }

}
