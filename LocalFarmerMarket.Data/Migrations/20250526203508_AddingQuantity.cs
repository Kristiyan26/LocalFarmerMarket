using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalFarmerMarket.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEBQ9dYhQpSG9UA7ML4MllkVAdlgVMO1hvW3d2aOuDoho2W7bIVJu37ZFrDdfsVEWAg==");

            migrationBuilder.UpdateData(
                table: "Farmers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEHP8ACLN5/pLt1Q1YzFWzbttxOP1ZLRm/z7RKb6QW4GutEHnRgmEHYDnDl/lCzq9wQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAECgEUuBVWEJELb+K6dOKHWN43jABCsrHBu1liZNCw7v7FEM7t05CYzJpXa13+Qca7w==");

            migrationBuilder.UpdateData(
                table: "Farmers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEDVsXccfwWvlagmOZHjs0yX3l89FOeEoObyIC5DmKFKSzOm6uebED8Ed54F5TSJ1uA==");
        }
    }
}
