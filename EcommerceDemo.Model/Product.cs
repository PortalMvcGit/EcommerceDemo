using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceDemo.Model
{
    public class ProductCategory
    {
        public int ProdCatId { get; set; }

        public string CategoryName { get; set; }
    }

    public class Product : ProductCategory
    {
        public int ProductId { get; set; }

        public string ProdDescription { get; set; }

        public string ProdName { get; set; }

    }
}
