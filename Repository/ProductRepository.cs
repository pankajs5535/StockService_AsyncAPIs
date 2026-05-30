using StockService_AsyncAPI.Data;
using StockService_AsyncAPI.Models;

namespace StockService_AsyncAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProductList()
        {
            return _context.Products.ToList();
        }

        public Product GetPoductById(int id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges(true);
        }
    }
}
