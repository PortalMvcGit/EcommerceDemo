using Core.Common;
using EcommerceDemo.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace EcommerceDemo.Data
{
    public class MasterRepository : IMasterDataRepository
    {
        private readonly IMemoryCache memoryCache;

        public MasterRepository(IConfiguration configuration)
        {
            BaseDataAccess.ConnectionString = configuration["AppSettings:ConnectionString"];
            if (memoryCache == null)
            {
                memoryCache = new MemoryCache(new MemoryCacheOptions());
            }
        }
        public void Delete(MasterData entity)
        {
            throw new NotImplementedException();
        }

        public List<MasterData> GetAll()
        {
            throw new NotImplementedException();
        }

        public MasterData GetAllMaster()
        {
            MasterData masters = memoryCache.Get<MasterData>("MASTER_DATA");
            if (masters == null)
            {
                masters = new MasterData();
                DbDataReader dbDataReader = BaseDataAccess.GetDataReader("sp_GetMasterData", null);
                masters.attributeNameList = new List<NameValueData>();
                while (dbDataReader.Read())
                {
                    NameValueData nameValueData = new NameValueData();
                    nameValueData.Name = dbDataReader["AttributeName"].ToString();
                    nameValueData.Value = Convert.ToInt32(dbDataReader["AttributeId"]);
                    nameValueData.ParentId = Convert.ToInt32(dbDataReader["ProdCatId"]);
                    masters.attributeNameList.Add(nameValueData);
                }
                memoryCache.Set<MasterData>("MASTER_DATA", masters);
            }
            return masters;
        }

        public MasterData GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(MasterData entity)
        {
            throw new NotImplementedException();
        }
    }
}
