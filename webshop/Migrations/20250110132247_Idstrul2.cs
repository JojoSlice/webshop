using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webshop.Migrations
{
    /// <inheritdoc />
    public partial class Idstrul2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Garment_Supplier_SupplierId1",
                table: "Garment");

            migrationBuilder.DropIndex(
                name: "IX_Garment_SupplierId1",
                table: "Garment");

            migrationBuilder.DropColumn(
                name: "SupplierId1",
                table: "Garment");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "Garment",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Garment_SupplierId",
                table: "Garment",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Garment_Supplier_SupplierId",
                table: "Garment",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Garment_Supplier_SupplierId",
                table: "Garment");

            migrationBuilder.DropIndex(
                name: "IX_Garment_SupplierId",
                table: "Garment");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierId",
                table: "Garment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SupplierId1",
                table: "Garment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Garment_SupplierId1",
                table: "Garment",
                column: "SupplierId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Garment_Supplier_SupplierId1",
                table: "Garment",
                column: "SupplierId1",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
