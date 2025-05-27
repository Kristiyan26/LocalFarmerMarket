using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LocalFarmerMarket.Core.Models
{
    public class Order :BaseEntity
    {

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime DeliveryDate { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]

        public string Status { get; set; } 
        public int ProductId { get; set; } 
        public virtual Product Product { get; set; }

    }
}
