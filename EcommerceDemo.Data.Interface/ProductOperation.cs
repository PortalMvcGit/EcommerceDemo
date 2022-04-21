using EcommerceDemo.Model;
using System;

namespace EcommerceDemo.Data.Interface
{
    public interface ProductOperation
    {
        public string List(Product product);

        public string Add(Product product);

        public string Delete(Product product);

        public string Update(Product product);
    }
}
