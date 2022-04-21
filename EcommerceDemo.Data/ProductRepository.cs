using Core.Common;
using EcommerceDemo.Data.Interface;
using EcommerceDemo.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;

namespace EcommerceDemo.Data
{
    public class ProductRepository : BaseDataAccess
    {


        public ProductRepository(IConfiguration configuration)
        {
            base.ConnectionString = configuration["AppSettings:ConnectionString"];
        }

        public List<Product> List(Product product)
        {
            List<DbParameter> test = new List<DbParameter>();

            string serializedMyObjects = string.Empty;
            DbDataReader dbDataReader = base.GetDataReader("sp_GetProductList", test);
            Product product1;
            List<Product> products = new List<Product>();
            while (dbDataReader.Read())
            {
                product1 = new Product();
                product1.ProdDescription = dbDataReader["ProdDescription"].ToString();
                product1.ProdName = dbDataReader["ProdName"].ToString();
                product1.ProductId = Convert.ToInt32(dbDataReader["ProductId"]);
                products.Add(product1);
            }

            return products;
        }

        public string Add(Product product)
        {
            throw new NotImplementedException();
        }

        public string Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public string Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
