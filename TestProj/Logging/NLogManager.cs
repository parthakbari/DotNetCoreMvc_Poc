using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace Hegic.Core.Common
{
    public static class NLogManager
    {
        public static ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// InfoLog
        /// </summary>
        /// <param name="nLogData"></param>
        public static void InfoLog(NLogData nLogData)
        {
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Info, NLogManager._logger.Name, nLogData.Message);
            SetLogEventInfo(theEvent, nLogData);
            _logger.Log(theEvent);
        }
        /// <summary>
        /// DebugLog
        /// </summary>
        /// <param name="nLogData"></param>
        public static void DebugLog(NLogData nLogData)
        {
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Debug, NLogManager._logger.Name, nLogData.Message);
            SetLogEventInfo(theEvent, nLogData);
            _logger.Log(theEvent);
        }
        /// <summary>
        /// ErrorLog
        /// </summary>
        /// <param name="nLogData"></param>
        public static void ErrorLog(NLogData nLogData)
        {
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Error, NLogManager._logger.Name, nLogData.Message);
            SetLogEventInfo(theEvent, nLogData);
            _logger.Log(theEvent);
        }
        /// <summary>
        /// TraceLog
        /// </summary>
        /// <param name="nLogData"></param>
        public static void TraceLog(NLogData nLogData)
        {
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Trace, NLogManager._logger.Name, nLogData.Message);
            SetLogEventInfo(theEvent, nLogData);
            _logger.Log(theEvent);
        }

        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void Error(string message, Exception exception)
        {
            _logger.Error(exception, message);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Error(object message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// Information the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void Info(string message, Exception exception)
        {
            _logger.Info(message, exception, null);
        }

        /// <summary>
        /// Information the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Info(object message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Set Log Event Info
        /// </summary>
        /// <param name="theEvent"></param>
        /// <param name="nLogData"></param>
        private static void SetLogEventInfo(LogEventInfo theEvent, NLogData nLogData)
        {
            theEvent.Properties["SessionId"] = "sadsadsadasdkasdjasdjklasl";
            theEvent.Properties["BrowserDetail"] = nLogData.BrowserDetail;
            theEvent.Properties["RequestUrl"] = nLogData.RequestUrl;
            theEvent.Properties["ErrorMessage"] = nLogData.ErrorMessage;
            theEvent.Properties["EmailBody"] = string.IsNullOrEmpty(nLogData.EmailBody) ? string.Empty : nLogData.EmailBody;
            theEvent.Properties["ProducerCode"] = nLogData.AgentCode;
            theEvent.Properties["QuoteNo"] = nLogData.QuoteNo;
            theEvent.Properties["CUSTOMER_IP_ADDRESS"] = nLogData.CustomerIPAddress;
            theEvent.Properties["SERVER_IP_ADDRESS"] = nLogData.ServerIPAddress;
            theEvent.Properties["POLICY_NO"] = nLogData.PolicyNumber;

        }
    }
}
