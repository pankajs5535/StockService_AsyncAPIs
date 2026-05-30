using StockService_AsyncAPI.Models;
using StockService_AsyncAPI.Repository;

namespace StockService_AsyncAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IProductRepository _productRepository;

        public OrderService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void AddProduct(Product product)
        {

            // ✅ BUSINESS RULES
            if (product == null)
                throw new Exception("Prouct data is Required");

            // Rule 2: stock must be available
            if (product.Stock <= 0)
                throw new Exception("Stock is not Available");

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new Exception("Product name is required");

            // Rule 3: price must be valid

            if (product.Price <= 0)
                throw new Exception("Price must be greater than zero");

            // Delegation to Repository
            _productRepository.Insert(product);
        }


        public List<Product> GetAllProduct()
        {
            return _productRepository.GetAllProductList();
        }

        public string PlaceOrder(int productId, int productQuantity)
        {

            if (productQuantity <= 0)
                return "Invalid order quantity";

            var product = _productRepository.GetPoductById(productId);
            if (product == null)
                return "Product not found";

            if (product.Stock < productQuantity)
                return "Insufficient stock";

            // Reduce stock
            product.Stock -= productQuantity;

            // Update product stock
            _productRepository.Update(product);

            // TODO: Save order in OrderRepository (not ProductRepository)

            return "Order placed successfully";
        }
    }
}
