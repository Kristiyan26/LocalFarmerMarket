using System;
using System.ComponentModel.DataAnnotations;

namespace LocalFarmerMarket.Core.Entities
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Range(0.1, double.MaxValue)]
        public double Quantity { get; set; } // e.g., kg

        [Range(0.1, 10000)]
        public decimal PricePerKgAtPurchaseTime { get; set; }
    }
}
