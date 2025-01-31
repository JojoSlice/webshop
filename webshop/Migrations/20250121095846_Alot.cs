using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webshop.Migrations
{
    /// <inheritdoc />
    public partial class Alot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Garment_Cart_CartId",
                table: "Garment");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "Garment",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Garment_CartId",
                table: "Garment",
                newName: "IX_Garment_OrderId");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Cart",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "PaymentOption",
                table: "Cart",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Cart",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "ShippmentOption",
                table: "Cart",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Garment_Cart_OrderId",
                table: "Garment",
                column: "OrderId",
                principalTable: "Cart",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Garment_Cart_OrderId",
                table: "Garment");

            migrationBuilder.DropColumn(
                name: "PaymentOption",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "ShippmentOption",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Garment",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Garment_OrderId",
                table: "Garment",
                newName: "IX_Garment_CartId");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Cart",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Garment_Cart_CartId",
                table: "Garment",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id");
        }
    }
}
