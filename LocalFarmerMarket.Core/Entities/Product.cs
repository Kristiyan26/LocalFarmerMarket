using LocalFarmerMarket.Core.Entities;
using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(200)]
    public string Description { get; set; }

    [Range(0.1, 10000)]
    public decimal PricePerKg { get; set; }

    [Range(0, double.MaxValue)]
    public double QuantityAvailable { get; set; }

    public DateTime HarvestDate { get; set; }

    public int FarmerId { get; set; }
    public virtual Farmer Farmer { get; set; }

    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
