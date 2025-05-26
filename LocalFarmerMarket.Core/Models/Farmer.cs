using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LocalFarmerMarket.Core.Models
{
    public class Farmer :User
    {
        [JsonIgnore]

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
