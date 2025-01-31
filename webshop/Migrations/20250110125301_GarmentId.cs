using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webshop.Migrations
{
    /// <inheritdoc />
    public partial class GarmentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GarmentId",
                table: "Supplier",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GarmentId",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_Id",
                table: "Supplier",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Garment_Id",
                table: "Garment",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_Id",
                table: "Cart",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Supplier_Id",
                table: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Garment_Id",
                table: "Garment");

            migrationBuilder.DropIndex(
                name: "IX_Cart_Id",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "GarmentId",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "GarmentId",
                table: "Cart");
        }
    }
}
