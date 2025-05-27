using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalFarmerMarket.Core.Models.ResponseDTOs
{
    public class ProductCreateResponse
    {
        public int ProductId { get; set; } // Unique ID of the created product
        public string Name { get; set; } // Product name
        public string Message { get; set; } // Success message

    }
}
