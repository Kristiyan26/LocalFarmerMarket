using LocalFarmerMarket.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocalFarmerMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersRepository _ordersRepo;
        private readonly OrderProductRepository _orderProductsRepo;

        public OrdersController(OrdersRepository ordersRepo, OrderProductRepository orderProductsRepo)
        {
            _orderProductsRepo = orderProductsRepo;
            _ordersRepo = ordersRepo;
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("{id}/products")]
        public IActionResult GetProductsInOrder(int id)
        {
            // 🔹 Validate that the order exists
            var order = _ordersRepo.GetFirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            // 🔹 Extract products associated with this order
            var productsInOrder = _orderProductsRepo.GetAll(op => op.OrderId == id)
                .Select(op => new
                {
                    ProductId = op.ProductId,
                    Name = op.Product.Name,
                    Description = op.Product.Description,
                    Quantity = op.Quantity,
                    PricePerKgAtPurchaseTime = op.PricePerKgAtPurchaseTime
                }).ToList();            

            return Ok(new
            {
                OrderId = id,
                Products = productsInOrder
            });
        }
    }


}
