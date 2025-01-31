using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webshop.Migrations
{
    /// <inheritdoc />
    public partial class category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Catergory",
                table: "Garment");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Garment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CatergoryId",
                table: "Garment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Garment_CategoryId",
                table: "Garment",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Garment_Category_CategoryId",
                table: "Garment",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Garment_Category_CategoryId",
                table: "Garment");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Garment_CategoryId",
                table: "Garment");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Garment");

            migrationBuilder.DropColumn(
                name: "CatergoryId",
                table: "Garment");

            migrationBuilder.AddColumn<string>(
                name: "Catergory",
                table: "Garment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
