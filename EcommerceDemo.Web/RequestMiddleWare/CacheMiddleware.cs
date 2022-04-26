using EcommerceDemo.Web.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EcommerceDemo.Web
{
    public class CacheMiddleware
    {
        private readonly RequestDelegate _next;
        private static Object lockObj = new Object();

        private readonly IConfiguration _config;

        public CacheMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this._next = next;
            _config = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            IHttpContextAccessor httpContextAccessor = context.RequestServices.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
            if (context.Response.StatusCode != (int)HttpStatusCode.InternalServerError)
                FillCache(httpContextAccessor);
            await _next.Invoke(context);
        }

        /// <summary>
        /// FillCache
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        private void FillCache(IHttpContextAccessor httpContextAccessor)
        {
            SettingConfig settingConfig = new SettingConfig(httpContextAccessor, _config);
            if (MasterDataHelper.GetAllMasterDetailType().Count == 0)
            {
                lock (lockObj)
                {
                    if (MasterDataHelper.GetAllMasterDetailType().Count == 0)
                    {
                        settingConfig.Register();
                    }
                }
            }
        }
    }
}