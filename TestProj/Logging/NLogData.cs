using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hegic.Core.Common
{
    public class NLogData
    {
        /// <summary>
        /// Gets or Sets Session Id
        /// </summary>
        public string SessionId { get; set; }
        /// <summary>
        /// Gets or Sets Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Gets or Sets Request Url
        /// </summary>
        public string RequestUrl { get; set; }
        /// <summary>
        /// Gets or Sets Browser Detail
        /// </summary>
        public string BrowserDetail { get; set; }
        /// <summary>
        /// Gets or Sets Method
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// Gets or Sets Error Message
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Gets or Sets Email Body
        /// </summary>
        public string EmailBody { get; set; }
        /// <summary>
        /// Gets or Sets Agent Code
        /// </summary>
        public string AgentCode { get; set; }
        /// <summary>
        /// Gets or Sets Quote No
        /// </summary>
        public decimal QuoteNo { get; set; }
        /// <summary>
        /// Gets or Sets Customer IP Address
        /// </summary>
        public string CustomerIPAddress { get; set; }
        /// <summary>
        /// Gets or Sets Server IP Address
        /// </summary>
        public string ServerIPAddress { get; set; }

        /// <summary>
        /// gets and sets policy Number;
        /// </summary>
        public string PolicyNumber { get; set; }
    }
}
