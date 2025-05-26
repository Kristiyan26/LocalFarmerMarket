using LocalFarmerMarket.Core.Models;
using LocalFarmerMarket.Core.Models.RequestDTOs;
using LocalFarmerMarket.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using LocalFarmerMarket.Core.Models.ResponseDTOs;

namespace LocalFarmerMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsRepository _productsRepo;
        private readonly FarmersRepository _farmersRepo;
        private readonly CategoriesRepository _categoriesRepo;
        private readonly OrdersRepository _ordersRepo;
        private readonly OrderProductRepository _orderProductsRepo;

        public ProductsController(ProductsRepository productsRepo,
            FarmersRepository farmersRepo,CategoriesRepository categoriesRepo,
            OrdersRepository ordersRepo, OrderProductRepository orderProductsRepo)
        {
            _productsRepo = productsRepo;
            _farmersRepo = farmersRepo;
            _categoriesRepo = categoriesRepo;
            _ordersRepo = ordersRepo;
            _orderProductsRepo = orderProductsRepo;
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery] int? categoryId, [FromQuery] int page = 1, [FromQuery] int itemsPerPage = 6)
        {
            if (page < 1 || itemsPerPage < 1)
            {
                return BadRequest(new { message = "Page and items per page must be positive integers." });
            }

            Expression<Func<Product, bool>> filter = categoryId.HasValue ? (x => x.CategoryId == categoryId.Value) : null;

            var products = _productsRepo.GetAll(filter, null, page, itemsPerPage);


         
            var totalProducts = products.Count();


            return Ok(new ProductListResponse
            {
                TotalProducts = totalProducts,
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                Products = products.ToList()
            });

        }

        [Authorize(Roles = "Farmer")]
        [HttpPost("add")]
        public IActionResult AddProduct([FromBody] ProductCreateRequest request)
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized(new { message = "Invalid authentication token." });

            var loggedInFarmerId = int.Parse(userIdClaim.Value);


            if (loggedInFarmerId != request.FarmerId)
            {
                return Unauthorized(new { message = "You can only add products for your own farmer account." });
            }

            if (string.IsNullOrWhiteSpace(request.Name) || request.PricePerKg <= 0 || request.QuantityAvailable <= 0)
            {
                return BadRequest(new { message = "Invalid product details. Check name, price, and quantity." });
            }

            var farmer = _farmersRepo.GetFirstOrDefault(f => f.Id == request.FarmerId);
            if (farmer == null)
            {
                return BadRequest(new { message = "Invalid farmer ID." });
            }

 
            var category = _categoriesRepo.GetFirstOrDefault(c => c.Id == request.CategoryId);
            if (category == null)
            {
                return BadRequest(new { message = "Invalid category ID." });
            }

            var newProduct = new Product
            {
                Name = request.Name,
                Description = request.Description,
                PricePerKg = request.PricePerKg,
                QuantityAvailable = request.QuantityAvailable,
                HarvestDate = request.HarvestDate,
                FarmerId = request.FarmerId,
                CategoryId = request.CategoryId,
                ImageUrl = request.ImageUrl
            };

 
            _productsRepo.Save(newProduct);

            return Created("", new { productId = newProduct.Id, message = "Product added successfully." });
        }
      [Authorize(Roles = "Customer")]
        [HttpPost("purchase")]
        public IActionResult Purchase([FromBody] ProductPurchaseRequest request)
        {
            Console.WriteLine($"Authorization Header: {Request.Headers["Authorization"]}");



            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "Invalid authentication token." });
            }

            var loggedInCustomerId = int.Parse(userIdClaim.Value);

            if (loggedInCustomerId != request.CustomerId)
            {
                return Unauthorized(new { message = "You can only purchase products for yourself." });
            }

            var product = _productsRepo.GetFirstOrDefault(p => p.Id == request.ProductId);
            if (product == null)
            {
                return BadRequest(new { message = "Invalid product ID." });
            }


            if (product.QuantityAvailable < request.Quantity)
            {
                return BadRequest(new { message = "Not enough stock available." });
            }

            var totalPrice = (decimal)request.Quantity * product.PricePerKg;

            var newOrder = new Order
            {
                CustomerId = request.CustomerId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                TotalPrice = totalPrice,
                DeliveryDate = DateTime.UtcNow.AddDays(3) // Assuming delivery in 3 days
            };

            _ordersRepo.Save(newOrder);

 
            var orderProduct = new OrderProduct
            {
                OrderId = newOrder.Id,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                PricePerKgAtPurchaseTime = product.PricePerKg
            };

            _orderProductsRepo.Save(orderProduct);

            product.QuantityAvailable -= request.Quantity;
            _productsRepo.Save(product);

            return Ok(new { orderId = newOrder.Id, message = "Purchase successful." });
        }


        [HttpGet("debug-token")]
        public IActionResult DebugToken()
        {
            // 1) Get raw header
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();

            // 2) Extract token string (after "Bearer ")
            var token = authHeader?.StartsWith("Bearer ") == true
                ? authHeader.Substring("Bearer ".Length).Trim()
                : null;

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "No Bearer token found in Authorization header." });

            // 3) Parse without validating
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt;
            try
            {
                jwt = handler.ReadJwtToken(token);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Invalid JWT format.", detail = ex.Message });
            }

            // 4) Return raw header and all claims
            var claims = jwt.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(new
            {
                RawAuthorizationHeader = authHeader,
                Claims = claims
            });
        }

  [Authorize]
        [HttpGet("debug-claims")]
        public IActionResult DebugClaims()
        {
            Console.WriteLine("debugging");
            var claims = User.Claims
                             .Select(c => new { c.Type, c.Value })
                             .ToList();
            return Ok(claims);
        }


        [Authorize(Roles = "Customer")]
        [HttpGet("purchased")]
        public IActionResult GetPurchasedProducts()
        {
          
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                Console.WriteLine("UserId claim is missing.");

                return Unauthorized(new { message = "Invalid authentication token." });
            }

            var loggedInCustomerId = int.Parse(userIdClaim.Value);

            var orders = _ordersRepo.GetAll(o => o.CustomerId == loggedInCustomerId);

            var purchasedProducts = orders
                .SelectMany(o => o.OrderProducts)
                .Select(op => op.Product)
                .Distinct()
                .ToList();

            return Ok(new { PurchasedProducts = purchasedProducts });
        }
    }
}
