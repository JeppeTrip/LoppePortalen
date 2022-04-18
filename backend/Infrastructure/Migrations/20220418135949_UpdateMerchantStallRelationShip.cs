using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdateMerchantStallRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
