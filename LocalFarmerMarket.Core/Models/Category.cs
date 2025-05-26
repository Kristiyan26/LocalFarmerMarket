using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LocalFarmerMarket.Core.Models
{
    public class Category : BaseEntity
    {

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public bool IsOrganic { get; set; }

        public DateTime TypicalSeasonStart { get; set; }
        public DateTime TypicalSeasonEnd { get; set; }


        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
