using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webshop.Migrations
{
    /// <inheritdoc />
    public partial class DescriptionOnGarment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Garment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Garment");
        }
    }
}
