using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LandmarkRemark.Persistence.Migrations
{
    public partial class addColumnLocationToNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<IPoint>(
                name: "Location",
                table: "Notes",
                type: "geometry",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Notes");
        }
    }
}
