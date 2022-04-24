using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceDemo.Model
{
    public class ProductViewModel
    {
        /// <summary>
        /// Get All products
        /// </summary>
        public List<Product> products { get; set; }

        /// <summary>
        /// Product Id
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Product Description
        /// </summary>
        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "Please Enter Product Description")]
        public string ProdDescription { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Please Enter Product Name")]
        public string ProdName { get; set; }

        /// <summary>
        /// Product Category ID
        /// </summary>
        [Display(Name = "Product Category")]
        [Required(ErrorMessage = "Please Enter Product Category")]
        public int ProdCatId { get; set; }

    }
}
