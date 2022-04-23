using EcommerceDemo.Model;

namespace EcommerceDemo.Data
{
    public interface IProductRepository : IRepository<Product>
    {
        //if you want extra method only for Product then you can use this Interface
    }
}
