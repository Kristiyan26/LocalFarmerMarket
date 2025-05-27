using Microsoft.AspNetCore.Mvc;
using LocalFarmerMarket.Services;
using LocalFarmerMarket.Core.Models.RequestDTOs;
using System.Net.Http;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

using LocalFarmerMarket.Core.Models.ResponseDTOs;

using System.Linq;
using System.Threading.Tasks;

namespace LocalFarmerMarket.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApiClient _api;

        public ProductsController(ApiClient api)
        {
            _api = api;
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateRequest request)
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "No authentication token found. Please log in again." });
            }

            _api.SetBearerToken(token);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            var userRoleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Azp);

            if (userRoleClaim == null || userRoleClaim.Value != "Farmer")
            {
                return Unauthorized(new { message = "Only farmers can add products." });
            }

            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "Invalid authentication token." });
            }

            int farmerId = int.Parse(userIdClaim.Value); // ✅ Extract the Farmer ID
            request.FarmerId = farmerId; // ✅ Set FarmerId in the request

            var response = await _api.PostAsync<ProductCreateResponse>("api/Products/add", request);

            if (response == null)
            {
                ViewData["ErrorMessage"] = "Failed to add product. Please try again.";
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home"); // ✅ Redirect user after successful product addition
        }
    }
}