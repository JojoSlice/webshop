using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webshop.Migrations
{
    /// <inheritdoc />
    public partial class KeyHitoDit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Customer_CustomerId",
                table: "Cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "GarmentId",
                table: "Cart");

            migrationBuilder.RenameTable(
                name: "Cart",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_Id",
                table: "Order",
                newName: "IX_Order_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_CustomerId",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GarmentOrder",
                columns: table => new
                {
                    GarmentsId = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentOrder", x => new { x.GarmentsId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_GarmentOrder_Garment_GarmentsId",
                        column: x => x.GarmentsId,
                        principalTable: "Garment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GarmentOrder_Order_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GarmentOrder_OrdersId",
                table: "GarmentOrder",
                column: "OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "GarmentOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Cart");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Id",
                table: "Cart",
                newName: "IX_Cart_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                table: "Cart",
                newName: "IX_Cart_CustomerId");

            migrationBuilder.AddColumn<string>(
                name: "GarmentId",
                table: "Cart",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Customer_CustomerId",
                table: "Cart",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");
        }
    }
}
