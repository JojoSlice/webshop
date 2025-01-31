using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webshop.Migrations
{
    /// <inheritdoc />
    public partial class OrderListgamentid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Garment_Cart_OrderId",
                table: "Garment");

            migrationBuilder.DropIndex(
                name: "IX_Garment_OrderId",
                table: "Garment");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Garment");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Cart");

            migrationBuilder.AddColumn<string>(
                name: "GarmentId",
                table: "Cart",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GarmentId",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Garment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Cart",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_Garment_OrderId",
                table: "Garment",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Garment_Cart_OrderId",
                table: "Garment",
                column: "OrderId",
                principalTable: "Cart",
                principalColumn: "Id");
        }
    }
}
