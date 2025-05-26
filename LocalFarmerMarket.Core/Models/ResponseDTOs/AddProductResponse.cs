using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalFarmerMarket.Core.Models.ResponseDTOs
{
    public class AddProductResponse
    {
        public int ProductId { get; set; }
        public string Message { get; set; } // Продуктът е добавен успешно.
    }
}
