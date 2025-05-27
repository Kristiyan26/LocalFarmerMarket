using LocalFarmerMarket.Core.Models.ResponseDTOs;
using LocalFarmerMarket.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LocalFarmerMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersRepository _ordersRepo;
        private readonly ProductsRepository _productsRepo;


        public OrdersController(OrdersRepository ordersRepo,ProductsRepository productsRepo)
        {
            _productsRepo = productsRepo;
            _ordersRepo = ordersRepo;
        }

        [HttpGet("orders")]
        public IActionResult GetOrders()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);


            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int customerId))
            {
                return Unauthorized(new { message = "Invalid authentication token." });
            }

            var orders = _ordersRepo.GetAll(o => o.CustomerId == customerId).ToList();

            if (!orders.Any())
            {
                return NotFound(new { message = "No orders found." });
            }

            var ordersList = orders.Select(order =>
            {
                var product = _productsRepo.GetFirstOrDefault(p => p.Id == order.ProductId);

                Console.WriteLine("product name:"+product.Name);

                return new OrderDTO
                {
                    ProductName = product.Name, //order.Product.Name,
                    Id = order.Id,
                    QuantityOrdered = order.Quantity,
                    TotalPrice = order.TotalPrice,
                    OrderDate = order.OrderDate,
                    Status = order.Status
                };
            }).ToList();

            return Ok(ordersList); // ✅ Returns refined order data instead of full DB entities
        }
    }


}
