using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalFarmerMarket.Core.Models
{
    public class User : BaseEntity
    {

        [Required]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(150)]
        public string Address { get; set; }

        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; } // "Customer" или "Farmer" 
    }
}
