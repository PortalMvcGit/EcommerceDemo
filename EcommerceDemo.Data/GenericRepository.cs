using System.Collections.Generic;

namespace EcommerceDemo.Data
{
    /// <summary>
    /// CRUD operation 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        int Insert(T entity);
        void Delete(T entity);
        T GetById(int id);
        /// <summary>
        /// Get All Details
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();
    }
}
