using HegicMvc_Poc.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace HegicMvc_Poc
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUserService, UserService>();

            services.AddMvc(
                 config =>
                 {
                     config.Filters.Add(typeof(ExceptionHandler));
                 }
                );
            services.Configure<AppSetting>(Configuration.GetSection("AppSettings"));
            ServiceConfig.ServiceUrl = new Uri(ConfigHelper.AppSetting("ServiceUrl"));
            ServiceConfig.WebApplicationUrl = new Uri(ConfigHelper.AppSetting("WebApplicationUrl"));
            ServiceConfig.CommonServiceUrl = new Uri(ConfigHelper.AppSetting("CommonServiceUrl"));
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });
            //services.Configure<IISOptions>(options =>
            //{
            //    options.ForwardClientCertificate = false;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //AppHttpContext.Services = app.ApplicationServices;
            app.UseSession();
            if (string.Equals(env.EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase))
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

           

            app.UseStaticFiles();
            NLog.GlobalDiagnosticsContext.Set("defaultConnection", ConfigHelper.AppSetting("PORTAL_LOG"));
            NLog.LogManager.LoadConfiguration(env.ContentRootPath + "\\NLog.config");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Vehicle}/{action=Index}/{id?}");
            });
        }
    }
}
