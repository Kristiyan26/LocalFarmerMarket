using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalFarmerMarket.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsOrganic = table.Column<bool>(type: "bit", nullable: false),
                    TypicalSeasonStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypicalSeasonEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Farmers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PricePerKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityAvailable = table.Column<double>(type: "float", nullable: false),
                    HarvestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FarmerId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Farmers_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    PricePerKgAtPurchaseTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsOrganic", "Name", "TypicalSeasonEnd", "TypicalSeasonStart" },
                values: new object[] { 1, true, "Fruits", new DateTime(2025, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Address", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "Role", "Username" },
                values: new object[] { 1, "Asenovgrad han krum 1", "Krisko@mail.bg", "Kristiyan", "Lyubenov", "AQAAAAIAAYagAAAAENPrhUZYVcpP8EZ8E8FL/DggYjAqGiywXK4X4Z/lcA/oLoQCK7Qehr9q4Bdql026tg==", "088888888", "Customer", "KriskoVliza" });

            migrationBuilder.InsertData(
                table: "Farmers",
                columns: new[] { "Id", "Address", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "Role", "Username" },
                values: new object[] { 1, "Green Fields, Asenovgrad", "johndoe@farm.com", "John", "Doe", "AQAAAAIAAYagAAAAEOdkEizlr3qoQCK2caqF/6r/WdhejcU9nL2vO0p5UFrcEYHMtvXwfsm+iavjN1RLew==", "0888123456", "Farmer", "JohnFarmer" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "DeliveryDate", "OrderDate", "Status", "TotalPrice" },
                values: new object[] { 1, 1, new DateTime(2025, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 17.5m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "FarmerId", "HarvestDate", "ImageUrl", "Name", "PricePerKg", "QuantityAvailable" },
                values: new object[] { 1, 1, "Freshly harvested organic apples", 1, new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/test-image.png", "Organic Apples", 3.5m, 50.0 });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "Id", "OrderId", "PricePerKgAtPurchaseTime", "ProductId", "Quantity" },
                values: new object[] { 1, 1, 3.5m, 1, 5.0 });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FarmerId",
                table: "Products",
                column: "FarmerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Farmers");
        }
    }
}
