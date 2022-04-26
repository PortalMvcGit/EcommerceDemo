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
        }

        /// <summary>
        /// 
        /// </summary>
        public List<NameValueData> attributeNameList { get; set; }
    }
}
