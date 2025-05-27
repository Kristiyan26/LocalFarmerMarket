using LocalFarmerMarket.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Product : BaseEntity
{


    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(200)]
    public string Description { get; set; }


    public decimal PricePerKg { get; set; }


    public double QuantityAvailable { get; set; }

    public DateTime HarvestDate { get; set; }

    public int FarmerId { get; set; }
    public virtual Farmer Farmer { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; }

    public string ImageUrl { get; set; }

}
