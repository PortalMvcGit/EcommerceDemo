using EcommerceDemo.Data;
using EcommerceDemo.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDemo.Business
{
    public class MasterManager
    {
        #region Variable Declaration
        /// <summary>
        /// Using product Repository
        /// </summary>
        private IRepository<MasterData> _masterRepo;
        #endregion

        public MasterManager(IConfiguration configuration)
        {
            _masterRepo = new MasterRepository(configuration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MasterData> GetMasterData()
        {
            return _masterRepo.GetAll();
        }

    }
}
