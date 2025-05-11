using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocalFarmerMarket.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public bool IsOrganic { get; set; }

        public DateTime TypicalSeasonStart { get; set; }
        public DateTime TypicalSeasonEnd { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
