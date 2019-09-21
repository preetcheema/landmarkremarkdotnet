using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace LandmarkRemark.Persistence.Migrations
{
    public partial class alterLocationMakeLocationNonNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "Notes",
                type: "geometry",
                nullable: false,
                oldClrType: typeof(IPoint),
                oldType: "geometry",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<IPoint>(
                name: "Location",
                table: "Notes",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry");
        }
    }
}
