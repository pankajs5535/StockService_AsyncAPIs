using StockService_AsyncAPI.Models;

namespace StockService_AsyncAPI.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetAllProductList();

        Product GetPoductById(int id);

        void Insert(Product product);

        void Update(Product product);
       
    }
}
