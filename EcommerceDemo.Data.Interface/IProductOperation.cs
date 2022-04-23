using EcommerceDemo.Model;
using System;
using System.Collections.Generic;

namespace EcommerceDemo.Data.Interface
{
    public interface IProductOperation
    {
        public List<Product> List(Product product);

        public string Add(Product product);

        public string Delete(Product product);

        public string Update(Product product);
    }
}
