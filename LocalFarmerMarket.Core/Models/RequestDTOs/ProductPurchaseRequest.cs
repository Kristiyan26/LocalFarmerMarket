using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalFarmerMarket.Core.Models.RequestDTOs
{
    public class ProductPurchaseRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CustomerId { get; set; }
    }
}
