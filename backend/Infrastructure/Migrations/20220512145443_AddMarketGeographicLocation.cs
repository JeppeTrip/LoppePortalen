using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Infrastructure.Migrations
{
    public partial class AddMarketGeographicLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "MarketTemplates",
                type: "geometry",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "MarketTemplates");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:postgis", ",,");
        }
    }
}
