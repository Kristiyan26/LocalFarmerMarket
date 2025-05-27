using LocalFarmerMarket.Services;
using Microsoft.AspNetCore.Mvc;
using LocalFarmerMarket.ViewModels.Home;
using LocalFarmerMarket.Core.Models.ResponseDTOs;

namespace LocalFarmerMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiClient _apiClient;

        public HomeController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<IActionResult> Index(string categoryFilter)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                return RedirectToAction("Login", "Account");
            }

            var productList = await _apiClient.GetAsync("api/Products/");
            var products = productList.Products; 


            var categories = products.Select(p => p.Category).Distinct().ToList();

            if (!string.IsNullOrEmpty(categoryFilter))
            {
                products = products.Where(p  => p.Category.Name == categoryFilter).ToList();
            }

            var viewModel = new IndexVM
            {
                Products = products,
                Categories = categories
            };

            return View(viewModel);
        }




    }
}
