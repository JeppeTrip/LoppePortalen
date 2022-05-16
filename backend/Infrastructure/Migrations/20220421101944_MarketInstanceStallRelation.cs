using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class MarketInstanceStallRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarketInstanceId",
                table: "Stalls",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stalls_MarketInstanceId",
                table: "Stalls",
                column: "MarketInstanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stalls_MarketInstances_MarketInstanceId",
                table: "Stalls",
                column: "MarketInstanceId",
                principalTable: "MarketInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stalls_MarketInstances_MarketInstanceId",
                table: "Stalls");

            migrationBuilder.DropIndex(
                name: "IX_Stalls_MarketInstanceId",
                table: "Stalls");

            migrationBuilder.DropColumn(
                name: "MarketInstanceId",
                table: "Stalls");
        }
    }
}
