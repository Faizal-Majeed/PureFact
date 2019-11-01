using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace UserRegistrationAPI.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthenticateFilterAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly IMemoryCache _cache;
        public AuthenticateFilterAttribute(IMemoryCache memoryCache)
        {
             _cache = memoryCache;
        }

        public void OnAuthorization(AuthorizationFilterContext actionContext)
        {
            var attrib = (actionContext.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetCustomAttributes(true).FirstOrDefault();
            if(attrib.GetType() != typeof(AllowAnonymousAttribute))
            {
                var request = actionContext.HttpContext.Request;
                var token = request.Headers.FirstOrDefault(h => h.Key.Equals("Authorization")).Value;                
                if(!_cache.TryGetValue(token[0], out string value))
                {
                    actionContext.Result = new JsonResult(new { HttpStatusCode.Unauthorized });
                }
            }
            
        }
    }
}