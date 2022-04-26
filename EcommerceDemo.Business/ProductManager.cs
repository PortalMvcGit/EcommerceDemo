using System;
using System.Collections.Generic;
using EcommerceDemo.Data;
using EcommerceDemo.Model;
using Microsoft.Extensions.Configuration;

namespace EcommerceDemo.Business
{
    public class ProductManager
    {
        #region Variable Declaration
        /// <summary>
        /// Using product Repository
        /// </summary>
        private IRepository<Product> _productRepo;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public ProductManager(IConfiguration configuration)
        {
            _productRepo = new ProductRepository(configuration);
        }

        /// <summary>
        /// Get the Product list
        /// </summary>
        /// <returns></returns>
        public List<Product> ProductList()
        {
            return _productRepo.GetAll();
        }

        /// <summary>
        /// Create product
        /// </summary>
        /// <returns></returns>
        public int Create(Product product)
        {
            return _productRepo.Insert(product);
        }

        /// <summary>
        /// Edit product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProduct(int id)
        {
            return _productRepo.GetById(id);
        }
    }
}
