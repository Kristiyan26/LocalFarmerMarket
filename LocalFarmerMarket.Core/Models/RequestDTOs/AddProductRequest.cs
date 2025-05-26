using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalFarmerMarket.Core.Models.RequestDTOs
{
    public class AddProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerKg { get; set; }
        public double QuantityAvailable { get; set; }
        public DateTime HarvestDate { get; set; }
        public int FarmerId { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }
    }
}
