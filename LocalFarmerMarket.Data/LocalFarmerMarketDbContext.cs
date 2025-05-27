using Microsoft.EntityFrameworkCore;
using LocalFarmerMarket.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LocalFarmerMarket.Data
{
    public class LocalFarmerMarketDbContext : DbContext
    {
        public LocalFarmerMarketDbContext(DbContextOptions<LocalFarmerMarketDbContext> options)
            : base(options)
        {
        }

        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<Product>()
                .Property(p => p.PricePerKg)
                .HasColumnType("decimal(18,2)");

            var passwordHasher = new PasswordHasher<User>();


            modelBuilder.Entity<Farmer>().HasData(
        new Farmer
        {
            Id = 1,
            Username = "JohnFarmer",
            Password = passwordHasher.HashPassword(null, "securepassword"), // 🔹 Hashed password
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@farm.com",
            Address = "Green Fields, Asenovgrad",
            PhoneNumber = "0888123456",
            Role = "Farmer"
        }
    );

            // ✅ Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Username = "KriskoVliza",
                    Password = passwordHasher.HashPassword(null, "securepassword"), // 🔹 Hashed password
                    FirstName = "Kristiyan",
                    LastName = "Lyubenov",
                    Email = "Krisko@mail.bg",
                    Address = "Asenovgrad han krum 1",
                    PhoneNumber = "088888888",
                    Role = "Customer"
                }
            );

            // ✅ Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Fruits",
                    IsOrganic = true,
                    TypicalSeasonStart = new DateTime(2025, 04, 01),
                    TypicalSeasonEnd = new DateTime(2025, 10, 31)
                }
            );

            // ✅ Seed Products Linked to Farmer & Category
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Organic Apples",
                    Description = "Freshly harvested organic apples",
                    PricePerKg = 3.5m,
                    QuantityAvailable = 50,
                    HarvestDate = new DateTime(2025, 05, 25),
                    FarmerId = 1,  // 🔹 Linked to seeded Farmer
                    CategoryId = 1,  // 🔹 Linked to seeded Category
                    ImageUrl = "https://example.com/test-image.png"
                }
            );

          
        }
        }
    }
