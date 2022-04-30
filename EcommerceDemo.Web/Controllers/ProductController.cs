using AutoMapper;
using Core.Common;
using EcommerceDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;

namespace EcommerceDemo.Web.Controllers
{
    public class ProductController : Controller
    {
        #region Variable Declaration

        /// <summary>
        /// logging
        /// </summary>
        private readonly ILogger<ProductController> _logger;

        /// <summary>
        /// Gets or sets the current HttpContext.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Web Api Calling Lib
        /// </summary>
        private readonly ServiceHelperWebApi _serviceHelperWebApi;

        /// <summary>
        /// Reading AppSetting Files
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Property Mapping Using AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        #endregion

        /// <summary>
        /// Initialize the Controller 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="contextAccessor"></param>
        /// <param name="config"></param>
        public ProductController(ILogger<ProductController> logger, IHttpContextAccessor contextAccessor, IConfiguration config, IMapper mapper)
        {
            _logger = logger;
            _httpContextAccessor = contextAccessor;
            _config = config;
            _mapper = mapper;
            _serviceHelperWebApi = new ServiceHelperWebApi(_httpContextAccessor, config);
        }

        public ActionResult Login()
        {
            return View();
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
        public JsonResult CreateProduct([FromBody] ProductViewModel productViewModel)
        {
            JsonResponse jsonResponse = new JsonResponse();
            if (ModelState.IsValid)
            {
                Product product = _mapper.Map<Product>(productViewModel);
                int success = _serviceHelperWebApi.ExecuteServicePostRequest<Product, int>("ProductApi/CreateProduct", product);
                if (success > 0)
                {
                    jsonResponse.Status = HttpStatusCode.OK.ToString();
                }
                else
                {
                    jsonResponse.Status = "ERROR";
                }
            }
            return Json(jsonResponse);
        }

        /// <summary>
        /// Edit Product Details
        /// </summary>
        /// <param name="productViewModel"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Product product = _serviceHelperWebApi.ExecuteServiceGetRequest<Product>("ProductApi/GetProduct/" + id);
            ProductViewModel productViewModel = _mapper.Map<ProductViewModel>(product);
            productViewModel.products = new List<Product>();
            productViewModel.products.Add(product);
            return View("~/Views/Product/_Edit.cshtml", productViewModel);
        }

        /// <summary>
        /// Delete Record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Delete(int? id)
        {
            JsonResponse jsonResponse = new JsonResponse();
            if (id == null)
            {
                jsonResponse.Status = "ERROR";
                return Json(jsonResponse);
            }
            int success = _serviceHelperWebApi.ExecuteServiceGetRequest<int>("ProductApi/DeleteProduct/" + id);
            if (success == 1)
            {
                jsonResponse.Status = HttpStatusCode.OK.ToString();
            }
            return Json(jsonResponse);
        }
    }
}
