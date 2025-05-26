using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalFarmerMarket.Core.Models.ResponseDTOs
{
    public class LoginResponse
    {
        public string Token { get; set; } // JWT или друг маркер
        public string Role { get; set; }  // "customer" или "farmer"
        public string Message { get; set; } // Успешен вход
    }
}
