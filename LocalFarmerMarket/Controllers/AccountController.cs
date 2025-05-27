
using Microsoft.AspNetCore.Mvc;
using LocalFarmerMarket.Services;
using LocalFarmerMarket.Core.Models.RequestDTOs;
using LocalFarmerMarket.Core.Models.ResponseDTOs;
using LocalFarmerMarket.Core.Models;

namespace LocalFarmerMarket.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApiClient _apiClient;

        public AccountController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _apiClient.PostAsync<LoginResponse>("api/Auth/login", model);

            if (response != null && response.Token != null)
            {
                HttpContext.Session.SetString("Token", response.Token);



                _apiClient.SetBearerToken(response.Token);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);

        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _apiClient.PostAsync<RegisterResponse>("api/Auth/register", model);

            if (response != null && !string.IsNullOrEmpty(response.Token))
            {
                _apiClient.SetBearerToken(response.Token);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", response?.Message ?? "Registration failed.");
            return View(model);
        }


        public IActionResult Logout()
        {
            _apiClient.SetBearerToken(null);
            return RedirectToAction("Login");
        }

    }
}
