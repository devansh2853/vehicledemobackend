using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleProject.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPropVehicleType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleType",
                table: "Cars");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleType",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
