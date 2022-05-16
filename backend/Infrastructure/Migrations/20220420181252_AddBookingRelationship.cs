using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddBookingRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stalls_Merchants_MerchantId",
                table: "Stalls");

            migrationBuilder.DropIndex(
                name: "IX_Stalls_MerchantId",
                table: "Stalls");

            migrationBuilder.DropColumn(
                name: "MerchantId",
                table: "Stalls");

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    MerchantId = table.Column<int>(type: "integer", nullable: false),
                    StallId = table.Column<int>(type: "integer", nullable: false),
                    BoothName = table.Column<string>(type: "text", nullable: true),
                    BoothDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Stalls_StallId",
                        column: x => x.StallId,
                        principalTable: "Stalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_MerchantId",
                table: "Bookings",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_StallId",
                table: "Bookings",
                column: "StallId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "MerchantId",
                table: "Stalls",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stalls_MerchantId",
                table: "Stalls",
                column: "MerchantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stalls_Merchants_MerchantId",
                table: "Stalls",
                column: "MerchantId",
                principalTable: "Merchants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
