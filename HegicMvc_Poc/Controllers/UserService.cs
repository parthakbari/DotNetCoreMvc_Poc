using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HegicMvc_Poc
{
    public interface IUserService
    {
        string BrowserDetail();
        string Method();
        string RemoteIpAddress();
    }

    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string BrowserDetail()
        {
            var context = _httpContextAccessor.HttpContext;
            return context.Request.Headers["User-Agent"].ToString();
        }

        public string Method()
        {
            var context = _httpContextAccessor.HttpContext;
            return context.Request.Method;
        }

        public string RemoteIpAddress()
        {
            var context = _httpContextAccessor.HttpContext;
            return context.Connection.RemoteIpAddress.ToString();
        }
    }

           
}