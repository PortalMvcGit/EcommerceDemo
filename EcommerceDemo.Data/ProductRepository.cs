using Core.Common;
using EcommerceDemo.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace EcommerceDemo.Data
{
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// Inilize Product Repository
        /// </summary>
        /// <param name="configuration"></param>
        public ProductRepository(IConfiguration configuration)
        {
            BaseDataAccess.ConnectionString = configuration["AppSettings:ConnectionString"];
        }

        /// <summary>
        /// Insert Product
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(Product entity)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(BaseDataAccess.GetParameter("@prodDescription", entity.ProdDescription));
            parameters.Add(BaseDataAccess.GetParameter("@prodName", entity.ProdName));
            parameters.Add(BaseDataAccess.GetParameter("@prodCatId", entity.ProdCatId));
            parameters.Add(BaseDataAccess.GetParameterOut("@id", System.Data.SqlDbType.Int));
            int check = BaseDataAccess.ExecuteNonQuery("sp_CreateProduct", parameters);
          
        }

        /// <summary>
        /// Delete Product Base id
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Product
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAll()
        {
            DbDataReader dbDataReader = BaseDataAccess.GetDataReader("sp_GetProductList", null);
            List<Product> products = new List<Product>();
            while (dbDataReader.Read())
            {
                Product product1 = new Product();
                product1.ProdDescription = dbDataReader["ProdDescription"].ToString();
                product1.ProdName = dbDataReader["ProdName"].ToString();
                product1.ProductId = Convert.ToInt32(dbDataReader["ProductId"]);
                product1.ProdCatId = Convert.ToInt32(dbDataReader["ProdCatId"]);
                product1.CategoryName = dbDataReader["CategoryName"].ToString();
                products.Add(product1);
            }
            return products;
        }

    }
}
