using Hegic.Core.Common;
using HegicMvc_Poc.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace HegicMvc_Poc
{
    public class ExceptionHandler : ExceptionFilterAttribute
    {
        private readonly IHttpContextAccessor _httpContextAccessor = null;
        private readonly IUserService userService = null;
        public ExceptionHandler(IHttpContextAccessor _httpContextAccessor, IUserService _userService)
        {
            this._httpContextAccessor = _httpContextAccessor;
            this.userService = _userService;
        }

        public override void OnException(ExceptionContext filterContext)
        {
            BaseController basecont = new BaseController(_httpContextAccessor, userService);
            if (filterContext.Exception != null)
            {
                try
                {
                    NLogManager.ErrorLog(basecont.SetNLogModel(filterContext.Exception));
                }
                catch
                {
                    NLogManager.Error(filterContext.Exception.Message, filterContext.Exception);
                }
                if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    filterContext.Result = new JsonResult("")
                    {
                        Value = new { status = "ERROR" }
                    };
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "Controller", "Vehicle" },
                            { "Action", "Error" }
                        });
                    filterContext.ExceptionHandled = true;
                }
            }
        }
    }
}
