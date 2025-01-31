using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webshop.Migrations
{
    /// <inheritdoc />
    public partial class Uniqes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Supplier_Id",
                table: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Garment_Id",
                table: "Garment");

            migrationBuilder.DropIndex(
                name: "IX_Customer_Id_UserName_Email",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Cart_Id",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Admin_Id_UserName",
                table: "Admin");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserName_Email",
                table: "Customer",
                columns: new[] { "UserName", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admin_UserName",
                table: "Admin",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_UserName_Email",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Admin_UserName",
                table: "Admin");

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
                name: "IX_Customer_Id_UserName_Email",
                table: "Customer",
                columns: new[] { "Id", "UserName", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_Id",
                table: "Cart",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admin_Id_UserName",
                table: "Admin",
                columns: new[] { "Id", "UserName" },
                unique: true);
        }
    }
}
