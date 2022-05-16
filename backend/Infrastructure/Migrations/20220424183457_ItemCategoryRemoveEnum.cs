using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ItemCategoryRemoveEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "Name",
                keyValue: "Art");

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "Name",
                keyValue: "Electronics");

            migrationBuilder.DeleteData(
                table: "ItemCategories",
                keyColumn: "Name",
                keyValue: "Furniture");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ItemCategories",
                columns: new[] { "Name", "Created", "CreatedBy", "LastModified", "LastModifiedBy" },
                values: new object[,]
                {
                    { "Furniture", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { "Electronics", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null },
                    { "Art", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null }
                });
        }
    }
}
