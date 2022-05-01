using System.Collections.Generic;

namespace EcommerceDemo.Model
{
    public class MasterData
    {
        /// <summary>
        /// 
        /// </summary>
        public MasterData()
        {
            attributeNameList = new List<NameValueData>();
            productCategory = new List<NameValueData>();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<NameValueData> attributeNameList { get; set; }

        /// <summary>
        /// Set the product Category
        /// </summary>
        public List<NameValueData> productCategory { get; set; }
    }
}
