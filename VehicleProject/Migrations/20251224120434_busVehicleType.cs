using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleProject.Migrations
{
    /// <inheritdoc />
    public partial class busVehicleType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VehicleTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Bus" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
