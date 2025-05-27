using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalFarmerMarket.Core.Models;

namespace LocalFarmerMarket.Core.Models.ResponseDTOs
{
    public class OrderListResponse
    {
        public List<OrderDTO> Orders { get; set; } = new List<OrderDTO>();
    }
}
