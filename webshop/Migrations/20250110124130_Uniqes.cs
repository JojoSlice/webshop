using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webshop.Migrations
{
    /// <inheritdoc />
    public partial class Uniqes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Customer",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customer",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Admin",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                name: "IX_Customer_Id_UserName_Email",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Cart_Id",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Admin_Id_UserName",
                table: "Admin");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Admin",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
