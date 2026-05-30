using StockService_AsyncAPI.Models;

namespace StockService_AsyncAPI.Services
{

    // Service contains BUSINESS RULES
    // Service = Business Rules +Workflow + Decisions

    // Repository = gets data from DB

    // Service = decides what to do with data
    public interface IOrderService
    {
        public string PlaceOrder(int productid, int productQuantity);

        List<Product> GetAllProduct();

        void AddProduct(Product product);
    }
}
