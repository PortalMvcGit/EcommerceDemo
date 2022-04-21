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

namespace EcommerceDemo.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(ILogger<ProductController> logger, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            ServiceHelperWebApi serviceHelperWebApi = new ServiceHelperWebApi(_httpContextAccessor);
            serviceHelperWebApi.ExecuteServiceRequest<string, string>("Product", "Test",string.Empty);
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
