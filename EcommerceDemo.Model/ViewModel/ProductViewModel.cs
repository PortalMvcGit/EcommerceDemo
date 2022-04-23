using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceDemo.Model
{
    public class ProductViewModel
    {
        public List<Product> products { get; set; }
        public int ProductId { get; set; }

        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "Please Enter Product Description")]
        public string ProdDescription { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Please Enter Product Name")]
        public string ProdName { get; set; }

        [Display(Name = "Product Category")]
        [Required(ErrorMessage = "Please Enter Product Category")]
        public int ProdCatId { get; set; }
    }
}
