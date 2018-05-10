using Hegic.Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Newtonsoft.Json;

namespace HegicMvc_Poc.Controllers
{
    public class BaseController : Controller
    {
        //protected readonly UserService userService;
        protected readonly ISession session = null;
        protected readonly IUserService userService = null;
        public BaseController(IHttpContextAccessor _httpContextAccessor, IUserService _userService)
        {
            this.userService = _userService;
            session = _httpContextAccessor.HttpContext.Session;
        }

        /// <summary>
        /// Gets the current logged in user detail.
        /// </summary>
        /// <value>
        /// The current logged in user detail.
        /// </value>
        public string CurrentLoggedInUserDetail
        {
            get
            {
                //var request = context.HttpContext.Request;
                //module = string.IsNullOrEmpty(module) ? request.Path.ToString().Replace("{", "").Replace("}", "").Replace("/", "") : module;

                var str = session.GetString("");
                var userInfo = JsonConvert.DeserializeObject<string>(str);
                return userInfo;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.BasePath = ServiceConfig.WebApplicationUrl.AbsoluteUri;
            ViewBag.BasePathAPI = ServiceConfig.ServiceUrl.AbsoluteUri;
            ViewBag.BasePathCommonAPI = ServiceConfig.CommonServiceUrl.AbsoluteUri;
        }


        /// <summary>
        /// Fill the NLogData Model with some extra value
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public NLogData SetNLogModel(Exception exception)
        {
            NLogData nLogData = new NLogData();
            nLogData.Message = exception.Message;
            nLogData.ErrorMessage = exception.StackTrace;
            nLogData.RequestUrl = "";
            nLogData.SessionId = session.Id;
            nLogData.Method = userService.Method();
            nLogData.BrowserDetail = userService.BrowserDetail(); //Current.Request.Headers["User-Agent"].ToString();
            nLogData.EmailBody = "";
            nLogData.AgentCode = "";
            nLogData.QuoteNo = 123231231;
            nLogData.CustomerIPAddress = userService.RemoteIpAddress();
            //nLogData.ServerIPAddress = Current.Connection.LocalIpAddress.ToString();
            nLogData.PolicyNumber = "";
            return nLogData;
        }

        public NLogData SetNLogModel(string message)
        {
            NLogData nLogData = new NLogData();
            nLogData.Message = message;
            nLogData.RequestUrl = "";
            nLogData.SessionId = "";//HttpContext.Session.Id;
            nLogData.Method = "";//HttpContext.Request.Method;
            nLogData.BrowserDetail = "";//Request.Headers["User-Agent"].ToString();
            nLogData.AgentCode = "";
            nLogData.QuoteNo = 1686687878;
            nLogData.CustomerIPAddress = "";//HttpContext.Connection.RemoteIpAddress.ToString();
            nLogData.ServerIPAddress = "";//HttpContext.Connection.LocalIpAddress.ToString();
            nLogData.PolicyNumber = "";
            return nLogData;
        }
    }
}