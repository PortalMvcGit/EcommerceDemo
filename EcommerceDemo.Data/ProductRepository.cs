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
            throw new NotImplementedException();
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
                products.Add(product1);
            }

            dbDataReader.NextResult();
            Dictionary<int, ProductAttribute> dic = new Dictionary<int, ProductAttribute>();
            while (dbDataReader.Read())
            {
                ProductAttribute productAttribute = new ProductAttribute();
                productAttribute.Name = dbDataReader["AttributeName"].ToString();
                if (dbDataReader["AttributeValue"] != DBNull.Value)
                {
                    productAttribute.Value = dbDataReader["AttributeValue"].ToString();
                }
                productAttribute.ProdCatId = Convert.ToInt32(dbDataReader["ProdCatId"]);
                productAttribute.ProductId = Convert.ToInt32(dbDataReader["ProductId"]);
                dic.Add(Convert.ToInt32(dbDataReader["AttributeId"]), productAttribute);
            }


            foreach (var pitem in products)
            {
                var ProductId = pitem.ProductId;
                var ProdCatId = pitem.ProdCatId;
                pitem.attributeList = new Dictionary<int, ProductAttribute>();
                foreach (var item in dic.Where(x => x.Value.ProdCatId == ProdCatId && x.Value.ProductId == ProductId))
                {
                    pitem.attributeList.Add(item.Key, item.Value);
                }
            }

            return products;
        }

    }
}
