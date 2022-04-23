﻿using EcommerceDemo.Web.Models;
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
using EcommerceDemo.Model;

namespace EcommerceDemo.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ServiceHelperWebApi _serviceHelperWebApi;

        private readonly IConfiguration _config;

        /// <summary>
        /// Initialize the Controller 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="contextAccessor"></param>
        /// <param name="config"></param>
        public ProductController(ILogger<ProductController> logger, IHttpContextAccessor contextAccessor, IConfiguration config)
        {
            _logger = logger;
            _httpContextAccessor = contextAccessor;
            _config = config;
            _serviceHelperWebApi = new ServiceHelperWebApi(_httpContextAccessor, config);
        }

        /// <summary>
        /// Show the first page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.products = _serviceHelperWebApi.ExecuteServiceGetRequest<List<Product>>("ProductApi");
            return View(productViewModel);
        }

        /// <summary>
        /// Create New Product
        /// </summary>
        /// <param name="productViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateProduct(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {

            }
                return Json(new object());
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