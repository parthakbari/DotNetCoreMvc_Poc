using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HegicMvc_Poc.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace HegicMvc_Poc.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IOptions<AppSetting> config;

        public HomeController(IHttpContextAccessor _httpContextAccessor,IOptions<AppSetting> Config, IUserService _userService) : base(_httpContextAccessor, _userService)
        {
            this.config = Config;
        }

        public IActionResult Index()
        {
            string ServiceUrl = ConfigHelper.AppSetting("ServiceUrl");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
