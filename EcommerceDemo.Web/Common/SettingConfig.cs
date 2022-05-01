using Core.Common;
using EcommerceDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Web.Common
{
    public class SettingConfig
    {
        /// <summary>
        /// Gets or sets the current HttpContext.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IConfiguration _config;

        /// <summary>
        /// Web Api Calling Lib
        /// </summary>
        private readonly ServiceHelperWebApi _serviceHelperWebApi;

        public SettingConfig(IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = contextAccessor;
            _config = configuration;
        }
        public void Register()
        {
            ServiceHelperWebApi serviceHelperWebApi = new ServiceHelperWebApi(_httpContextAccessor, _config);
            List<MasterData> allmaster = MasterDataHelper.GetAllMasterDetailType();
            MasterData master = serviceHelperWebApi.ExecuteServiceGetRequest<MasterData>("MasterApi/GetMasterData");
            allmaster.Add(new MasterData()
            {
                attributeNameList = master.attributeNameList,
                productCategory = master.productCategory
            });


        }
    }
}
