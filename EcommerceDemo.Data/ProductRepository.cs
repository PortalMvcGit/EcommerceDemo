using Core.Common;
using EcommerceDemo.Data.Interface;
using EcommerceDemo.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;

namespace EcommerceDemo.Data
{
    public class ProductRepository : BaseDataAccess
    {


        public ProductRepository(IConfiguration config)
        {

        }

        public string List(Product product)
        {
            return string.Empty;
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
