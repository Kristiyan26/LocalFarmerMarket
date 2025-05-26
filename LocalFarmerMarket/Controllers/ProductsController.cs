using Microsoft.AspNetCore.Mvc;
using LocalFarmerMarket.Services;
using LocalFarmerMarket.Core.Models.RequestDTOs;
namespace LocalFarmerMarket.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApiClient _api;

        public ProductsController(ApiClient api)
        {
            _api = api;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var products = await _api.GetAsync<List<Product>>("products");
        //    return View(products);
        //}

        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(ProductCreateRequest model)
        {
            var response = await _api.PostAsync<object>("products/add", model);
            return RedirectToAction("Index");
        }
    }
}
