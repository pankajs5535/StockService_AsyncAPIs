using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockService_AsyncAPI.Models;
using StockService_AsyncAPI.Services;

namespace StockService_AsyncAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public HomeController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("Show-products")]
        public IActionResult GetAllProductList()
        {
            return Ok(_orderService.GetAllProduct());
        }

        // POST: api/products
        [HttpPost("Add-products")]
        public IActionResult AddProduct(Product product)
        {
            _orderService.AddProduct(product);
            return Ok();
        }

        // POST: api/orders/place
        [HttpPost("orders/place")]
        public IActionResult PlaceOrder(int productId, int quantity)
        {
            var result = _orderService.PlaceOrder(productId, quantity);
            return Ok(result);
        }
    }
}
