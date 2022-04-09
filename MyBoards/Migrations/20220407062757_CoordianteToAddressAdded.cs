using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoards.Migrations
{
    public partial class CoordianteToAddressAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Coordinate_Latitude",
                table: "Addresses",
                type: "decimal(18,7)",
                precision: 18,
                scale: 7,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Coordinate_Longitude",
                table: "Addresses",
                type: "decimal(18,7)",
                precision: 18,
                scale: 7,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinate_Latitude",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Coordinate_Longitude",
                table: "Addresses");
        }
    }
}
