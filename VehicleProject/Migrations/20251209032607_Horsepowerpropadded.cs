using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleProject.Migrations
{
    /// <inheritdoc />
    public partial class Horsepowerpropadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Horsepower",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Horsepower",
                table: "Cars");
        }
    }
}
