using EcommerceDemo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EcommerceDemo.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ServiceHelperWebApi _serviceHelperWebApi;

        private readonly IConfiguration _config;

        public ProductController(ILogger<ProductController> logger, IHttpContextAccessor contextAccessor, IConfiguration config)
        {
            _logger = logger;
            _httpContextAccessor = contextAccessor;
            _config = config;
            _serviceHelperWebApi = new ServiceHelperWebApi(_httpContextAccessor, config);
        }

        public IActionResult Index()
        {
            _serviceHelperWebApi.ExecuteServiceRequest<string, string>("Product", "Test", string.Empty);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
