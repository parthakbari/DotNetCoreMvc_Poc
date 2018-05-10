using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HegicMvc_Poc.Controllers
{
    public class ServiceConfig
    {
        /// <summary>
        /// Gets or sets the web application URL.
        /// </summary>
        /// <value>
        /// The web application URL.
        /// </value>
        public static Uri WebApplicationUrl { get; set; }

        /// <summary>
        /// This will be set a Prefix to avoid NLB issue
        /// </summary>
        public static Uri PrefixPath { get; set; }

        /// <summary>
        /// Gets or sets the service URL.
        /// </summary>
        /// <value>
        /// The service URL.
        /// </value>
        public static Uri ServiceUrl { get; set; }

        /// <summary>
        /// CommonServiceUrl
        /// </summary>
        public static Uri CommonServiceUrl { get; set; }

    }
}
