using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalFarmerMarket.Core.Models.ResponseDTOs
{
    public class ProductListResponse
    {
        public int TotalProducts { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();

    }
}
