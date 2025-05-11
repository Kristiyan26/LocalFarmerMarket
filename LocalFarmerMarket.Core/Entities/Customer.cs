using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocalFarmerMarket.Core.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(150)]
        public string Address { get; set; }

        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
