using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ItemCategoryEnumData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingItemCategory");

            migrationBuilder.CreateTable(
                name: "BookingCategory",
                columns: table => new
                {
                    BookingsId = table.Column<string>(type: "text", nullable: false),
                    ItemCategoriesName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingCategory", x => new { x.BookingsId, x.ItemCategoriesName });
                    table.ForeignKey(
                        name: "FK_BookingCategory_Bookings_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingCategory_ItemCategories_ItemCategoriesName",
                        column: x => x.ItemCategoriesName,
                        principalTable: "ItemCategories",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ItemCategories",
                columns: new[] { "Name", "Created", "CreatedBy", "LastModified", "LastModifiedBy" },
                values: new object[,]
                {
                    { "Furniture", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { "Electronics", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { "Art", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingCategory_ItemCategoriesName",
                table: "BookingCategory",
                column: "ItemCategoriesName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingCategory");

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "Name",
                keyValue: "Furniture");

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "Name",
                keyValue: "Electronics");

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "Name",
                keyValue: "Art");

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
    }
}
