using EcommerceDemo.Business;
using EcommerceDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MasterApiController : ControllerBase
    {
        #region Variable Declaration
        /// <summary>
        /// logging
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Reading AppSetting Files
        /// </summary>
        private readonly ILogger<MasterApiController> _logger;

        /// <summary>
        /// Product Manager
        /// </summary>
        private readonly MasterManager _masterManager;
        #endregion

        public MasterApiController(ILogger<MasterApiController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
            _masterManager = new MasterManager(_config);
        }

        /// <summary>
        /// Get master data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMasterData")]
        public MasterData GetMasterData()
        {
            return _masterManager.GetMasterData();
        }

    }
}
