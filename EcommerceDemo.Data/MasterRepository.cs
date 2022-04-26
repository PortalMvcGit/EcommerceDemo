﻿using Core.Common;
using EcommerceDemo.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace EcommerceDemo.Data
{
    public class MasterRepository : IMasterDataRepository
    {
        public MasterRepository(IConfiguration configuration)
        {
            BaseDataAccess.ConnectionString = configuration["AppSettings:ConnectionString"];
        }
        public void Delete(MasterData entity)
        {
            throw new NotImplementedException();
        }

        public List<MasterData> GetAll()
        {
            DbDataReader dbDataReader = BaseDataAccess.GetDataReader("sp_GetMasterData", null);
            List<MasterData> masterData = new List<MasterData>();
            MasterData masters = new MasterData();
            masters.attributeNameList = new List<NameValueData>();
            while (dbDataReader.Read())
            {
                NameValueData nameValueData = new NameValueData();
                nameValueData.Name = dbDataReader["AttributeName"].ToString();
                nameValueData.Value = Convert.ToInt32(dbDataReader["AttributeId"]);
                nameValueData.ParentId = Convert.ToInt32(dbDataReader["ProdCatId"]);
                masters.attributeNameList.Add(nameValueData);
            }
            masterData.Add(masters);

            return masterData;
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