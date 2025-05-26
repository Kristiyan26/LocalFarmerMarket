using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocalFarmerMarket.Core.Models
{
    public class Customer : User
    {
       public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
