using EcommerceDemo.Data;
using EcommerceDemo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger,IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        [HttpGet]
        public List<Product> ProductList()
        {
            ProductRepository productRepository = new ProductRepository(_config);
            Product product = new Product();
            return productRepository.List(product);
        }

    }
}
