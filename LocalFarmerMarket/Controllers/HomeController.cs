using System.Diagnostics;
using LocalFarmerMarket.Core;
using Microsoft.AspNetCore.Mvc;

namespace LocalFarmerMarket.Controllers
{
    public class HomeController : Controller
    {



        public IActionResult Index()
        {
            return View();
        }


  
    }
}
