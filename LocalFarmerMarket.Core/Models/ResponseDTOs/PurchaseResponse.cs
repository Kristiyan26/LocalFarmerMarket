using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalFarmerMarket.Core.Models.ResponseDTOs
{
    public class PurchaseResponse
    {
        public int OrderId { get; set; }
        public string Message { get; set; } // Закупуването е успешно.
    }
}
