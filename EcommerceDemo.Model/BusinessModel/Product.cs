using System.Collections.Generic;

namespace EcommerceDemo.Model
{
    /// <summary>
    /// Product
    /// </summary>
    public class Product : ProductCategory
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Product Description
        /// </summary>
        public string ProdDescription { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string ProdName { get; set; }

        /// <summary>
        /// Get or Set Attribute list
        /// </summary>
        public Dictionary<int, string> attributeNameList { get; set; }

        /// <summary>
        /// Get or Set Attribute list
        /// </summary>
        public Dictionary<int, string> attributeValueList { get; set; }

    }

    /// <summary>
    /// Attribute
    /// </summary>
    public class ProductAttribute : ProductCategory
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Attribute Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Attribute Value
        /// </summary>
        public string Value { get; set; }
    }
}
