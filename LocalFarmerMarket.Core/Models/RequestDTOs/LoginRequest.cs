using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalFarmerMarket.Core.Models.RequestDTOs
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
