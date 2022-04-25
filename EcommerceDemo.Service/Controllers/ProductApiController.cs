using EcommerceDemo.Business;
using EcommerceDemo.Data;
using EcommerceDemo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace EcommerceDemo.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductApiController : ControllerBase
    {
        #region Variable Declaration
        /// <summary>
        /// logging
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Reading AppSetting Files
        /// </summary>
        private readonly ILogger<ProductApiController> _logger;

        /// <summary>
        /// Product Manager
        /// </summary>
        private readonly ProductManager _productManager;
        #endregion

        /// <summary>
        /// Initialize the Controller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        public ProductApiController(ILogger<ProductApiController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
            _productManager = new ProductManager(_config);
        }

        /// <summary>
        /// Get the Product list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Product> ProductList()
        {
            return _productManager.ProductList();
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateProduct")]
        public int CreateProduct([FromBody] Product product)
        {
            return _productManager.Create(product);
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProduct/{id}")]
        public Product GetProduct(int id)
        {
            return _productManager.GetProduct(id);
        }

    }
}
