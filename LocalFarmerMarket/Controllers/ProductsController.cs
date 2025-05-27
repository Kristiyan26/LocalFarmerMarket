using Microsoft.AspNetCore.Mvc;
using LocalFarmerMarket.Services;
using LocalFarmerMarket.Core.Models.RequestDTOs;
using System.Net.Http;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
namespace LocalFarmerMarket.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApiClient _api;

        public ProductsController(ApiClient api)
        {
            _api = api;
        }


    }
}
