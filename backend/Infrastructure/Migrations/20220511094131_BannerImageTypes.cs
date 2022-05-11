using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class BannerImageTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingImages",
                columns: table => new
                {
                    BookingId = table.Column<string>(type: "text", nullable: false),
                    ImageTitle = table.Column<string>(type: "text", nullable: false),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingImages", x => new { x.BookingId, x.ImageTitle });
                    table.ForeignKey(
                        name: "FK_BookingImages_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketImages",
                columns: table => new
                {
                    MarketTemplateId = table.Column<int>(type: "integer", nullable: false),
                    ImageTitle = table.Column<string>(type: "text", nullable: false),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketImages", x => new { x.MarketTemplateId, x.ImageTitle });
                    table.ForeignKey(
                        name: "FK_MarketImages_MarketTemplates_MarketTemplateId",
                        column: x => x.MarketTemplateId,
                        principalTable: "MarketTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MerchantImages",
                columns: table => new
                {
                    MerchantId = table.Column<int>(type: "integer", nullable: false),
                    ImageTitle = table.Column<string>(type: "text", nullable: false),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantImages", x => new { x.MerchantId, x.ImageTitle });
                    table.ForeignKey(
                        name: "FK_MerchantImages_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingImages_BookingId",
                table: "BookingImages",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarketImages_MarketTemplateId",
                table: "MarketImages",
                column: "MarketTemplateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MerchantImages_MerchantId",
                table: "MerchantImages",
                column: "MerchantId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingImages");

            migrationBuilder.DropTable(
                name: "MarketImages");

            migrationBuilder.DropTable(
                name: "MerchantImages");
        }
    }
}
