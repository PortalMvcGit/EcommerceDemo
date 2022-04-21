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
            
            string serializedMyObjects=string.Empty;
            DataTable dataTable = base.GetDataReader("sp_GetProductList", test);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                serializedMyObjects = JsonConvert.SerializeObject(dataTable);
            }
            List<Product> productt = JsonConvert.DeserializeObject<List<Product>>(serializedMyObjects);
            return productt;
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
