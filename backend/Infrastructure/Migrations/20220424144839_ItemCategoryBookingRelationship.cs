using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ItemCategoryBookingRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemCategories",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategories", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "BookingItemCategory",
                columns: table => new
                {
                    BookingsId = table.Column<string>(type: "text", nullable: false),
                    ItemCategoriesName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingItemCategory", x => new { x.BookingsId, x.ItemCategoriesName });
                    table.ForeignKey(
                        name: "FK_BookingItemCategory_Bookings_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingItemCategory_ItemCategories_ItemCategoriesName",
                        column: x => x.ItemCategoriesName,
                        principalTable: "ItemCategories",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingItemCategory_ItemCategoriesName",
                table: "BookingItemCategory",
                column: "ItemCategoriesName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingItemCategory");

            migrationBuilder.DropTable(
                name: "ItemCategories");
        }
    }
}
