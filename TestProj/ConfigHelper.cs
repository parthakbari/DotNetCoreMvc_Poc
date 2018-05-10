using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hegic.Core.Common
{
    public class ConfigHelperCommon
    {

        private static ConfigHelperCommon _appSettings;

        public string appSettingValue { get; set; }

        public static string AppSetting(string Key)
        {
            _appSettings = GetCurrentSettings(Key);
            return _appSettings.appSettingValue;
        }

        public ConfigHelperCommon(IConfiguration config, string Key)
        {
            this.appSettingValue = config.GetValue<string>(Key);
        }

        public static ConfigHelperCommon GetCurrentSettings(string Key)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var settings = new ConfigHelperCommon(configuration.GetSection("AppSettings"), Key);

            return settings;
        }
    }

}
