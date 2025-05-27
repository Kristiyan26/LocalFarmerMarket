using Azure.Core;
using LocalFarmerMarket.Core.Models;
using LocalFarmerMarket.Core.Models.RequestDTOs;
using LocalFarmerMarket.Core.Models.ResponseDTOs;
using LocalFarmerMarket.Services;
using LocalFarmerMarket.ViewModels.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;

namespace LocalFarmerMarket.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApiClient _apiClient;

        public OrdersController(ApiClient api)
        {
            _apiClient = api;
        }



        public async Task<IActionResult> Buy([FromBody] ProductPurchaseRequest request)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "No authentication token found. Please log in again." });
            }
            _apiClient.SetBearerToken(token);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "Invalid authentication token." });
            }

            int customerId = int.Parse(userIdClaim.Value);


            //var purchaseRequest = new ProductPurchaseRequest
            //{
            //    CustomerId = customerId,
            //    ProductId = request.ProductId,
            //    Quantity = request.Quantity
            //};

            request.CustomerId = customerId; 
            var response = await _apiClient.PostAsync<PurchaseResponse>("api/Products/purchase", request);
            if (response==null)
            {
                ViewData["ErrorMessage"] = "Failed to complete purchase. Please try again.";
                return RedirectToAction("Index", "Home");

            }

            return RedirectToAction("OrderHistory"); 
        }

        public async Task<IActionResult> OrdersHistory()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "No authentication token found. Please log in again." });
            }
            _apiClient.SetBearerToken(token);


            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "Invalid authentication token." });
            }

            int customerId = int.Parse(userIdClaim.Value); // ✅ Extract the ID
            OrderListResponse orders;
            try
            {
                orders = await _apiClient.GetAsyncOrders("api/Orders/orders");
                OrdersHistoryVM orderHistoryVM = new OrdersHistoryVM
                {
                    Orders = orders.Orders
                };


                return View("OrdersHistory", orderHistoryVM);

            }
            catch (HttpRequestException ex)
            {
            
                ViewData["ErrorMessage"] = ex.Message;
              
            }


            return View("OrdersHistory");

        }

        //[Authorize(Roles = "Customer")]
        //public async Task<IActionResult> OrderHistory()
        //{
        //    var customerId = HttpContext.Session.GetInt32("CustomerId");
        //    if (customerId == null) return RedirectToAction("Login", "Account");

        //    var orders = await _apiClient.GetAsync("api/Orders/history?customerId={customerId}");

        //    var orderHistoryVM = new OrdersHistoryVM
        //    {
        //        Orders = orders
        //    };

        //    return View(orderHistoryVM);
        //}


    }
}
