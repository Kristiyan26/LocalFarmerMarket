using LocalFarmerMarket.Services;
using Microsoft.AspNetCore.Mvc;

namespace LocalFarmerMarket.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApiClient _api;

        public OrdersController(ApiClient api)
        {
            _api = api;
        }

      
    }
}
