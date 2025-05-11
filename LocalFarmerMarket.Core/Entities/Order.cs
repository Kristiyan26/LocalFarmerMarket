using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocalFarmerMarket.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        public DateTime DeliveryDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } // e.g., "Pending", "Completed"

        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
