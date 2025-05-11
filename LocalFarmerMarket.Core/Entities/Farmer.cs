using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocalFarmerMarket.Core.Entities
{
    public class Farmer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }

        public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
