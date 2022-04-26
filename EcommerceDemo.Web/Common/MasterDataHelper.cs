using EcommerceDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Web.Common
{
    public static class MasterDataHelper
    {
        /// <summary>
        /// The _key value type
        /// </summary>
        private static List<MasterData> _MasterData = new List<MasterData>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<MasterData> GetAllMasterDetailType()
        {
            return _MasterData;
        }

        /// <summary>
        /// Get Attribute Name List
        /// </summary>
        /// <returns></returns>
        public static List<NameValueData> GetAttributeNameList()
        {
            return _MasterData.FirstOrDefault().attributeNameList;
        }
    }
}
