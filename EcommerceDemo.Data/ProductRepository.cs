using Core.Common;
using EcommerceDemo.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace EcommerceDemo.Data
{
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// Inilize Product Repository
        /// </summary>
        /// <param name="configuration"></param>
        public ProductRepository(IConfiguration configuration)
        {
            BaseDataAccess.ConnectionString = configuration["AppSettings:ConnectionString"];
        }

        public ProductRepository()
        {
        }

        /// <summary>
        /// Insert Product
        /// </summary>
        /// <param name="entity"></param>
        public int Insert(Product entity)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(BaseDataAccess.GetParameter("@prodDescription", entity.ProdDescription));
            parameters.Add(BaseDataAccess.GetParameter("@prodName", entity.ProdName));
            parameters.Add(BaseDataAccess.GetParameter("@prodCatId", entity.ProdCatId));
            parameters.Add(BaseDataAccess.GetParameter("@PRODCTID", entity.ProductId));            
            parameters.Add(BaseDataAccess.GetParameter("@Attrvalue", entity.attributeValueList));
            parameters.Add(BaseDataAccess.GetParameterOut("@id", System.Data.SqlDbType.Int));
            int check = BaseDataAccess.ExecuteNonQuery("sp_CreateProduct", parameters);
            return check;
        }

        /// <summary>
        /// Delete Product Base id
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetById(int id)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(BaseDataAccess.GetParameter("@id", id));
            DbDataReader dbDataReader = BaseDataAccess.GetDataReader("sp_GetProductByid", parameters);
            Product product = new Product();
            while (dbDataReader.Read())
            {
                product.ProdDescription = dbDataReader["ProdDescription"].ToString();
                product.ProdName = dbDataReader["ProdName"].ToString();
                product.ProductId = Convert.ToInt32(dbDataReader["ProductId"]);
                product.ProdCatId = Convert.ToInt32(dbDataReader["ProdCatId"]);
            }
            dbDataReader.NextResult();
            product.attributeValueList = new Dictionary<int, string>();
            product.attributeNameList = new Dictionary<int, string>();
            while (dbDataReader.Read())
            {
                product.attributeValueList.Add(Convert.ToInt32(dbDataReader["AttributeId"]),
                            dbDataReader["AttributeValue"] == DBNull.Value ? string.Empty : dbDataReader["AttributeValue"].ToString());
                product.attributeNameList.Add(Convert.ToInt32(dbDataReader["AttributeId"]),
                            dbDataReader["AttributeName"].ToString());
            }

            return product;
        }

        /// <summary>
        /// Get Product
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAll()
        {
            DbDataReader dbDataReader = BaseDataAccess.GetDataReader("sp_GetProductList", null);
            List<Product> products = new List<Product>();
            while (dbDataReader.Read())
            {

                Product product1 = new Product();
                product1.ProdDescription = dbDataReader["ProdDescription"].ToString();
                product1.ProdName = dbDataReader["ProdName"].ToString();
                product1.ProductId = Convert.ToInt32(dbDataReader["ProductId"]);
                product1.ProdCatId = Convert.ToInt32(dbDataReader["ProdCatId"]);
                product1.CategoryName = dbDataReader["CategoryName"].ToString();
                if (products.Any(x => x.ProductId == product1.ProductId))
                {
                    if (dbDataReader["AttributeId"] != DBNull.Value && dbDataReader["AttributeName"] != DBNull.Value)
                    {
                        products.FirstOrDefault(x => x.ProductId == product1.ProductId).attributeNameList.Add(Convert.ToInt32(dbDataReader["AttributeId"]),
                            dbDataReader["AttributeName"].ToString());
                        products.FirstOrDefault(x => x.ProductId == product1.ProductId).attributeValueList.Add(Convert.ToInt32(dbDataReader["AttributeId"]),
                            dbDataReader["AttributeValue"] == DBNull.Value ? string.Empty : dbDataReader["AttributeValue"].ToString());
                    }
                }
                else
                {
                    product1.attributeNameList = new Dictionary<int, string>();
                    product1.attributeValueList = new Dictionary<int, string>();
                    if (dbDataReader["AttributeId"] != DBNull.Value && dbDataReader["AttributeName"] != DBNull.Value)
                    {
                        product1.attributeNameList?.Add(Convert.ToInt32(dbDataReader["AttributeId"]),
                            dbDataReader["AttributeName"].ToString());
                        product1.attributeValueList?.Add(Convert.ToInt32(dbDataReader["AttributeId"]),
                            dbDataReader["AttributeValue"] == DBNull.Value ? string.Empty : dbDataReader["AttributeValue"].ToString());
                    }
                    products.Add(product1);
                }

            }
            return products;
        }

        public Product GetAllMaster()
        {
            throw new NotImplementedException();
        }
    }
}
