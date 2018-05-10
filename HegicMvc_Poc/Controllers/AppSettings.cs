namespace HegicMvc_Poc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using HegicMvc_Poc;
    using System.Configuration;
    using System.IO;

    public class AppSettings
    {
        private static AppSettings _appSettings;

        public string WebApplicationUrl { get; set; }

        public string ServiceUrl { get; set; }

        public string CommonServiceUrl { get; set; }

        public AppSettings(IConfiguration config)
        {
            this.WebApplicationUrl = config.GetValue<string>("WebApplicationUrl");

            // Now set Current
            _appSettings = this;
        }

        public static AppSettings Current
        {
            get
            {
                if (_appSettings == null)
                {
                    _appSettings = GetCurrentSettings();
                }

                return _appSettings;
            }
        }

        public static AppSettings GetCurrentSettings()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var settings = new AppSettings(configuration.GetSection("AppSettings"));

            return settings;
        }
    }
}
