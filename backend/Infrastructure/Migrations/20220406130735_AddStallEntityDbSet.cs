using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddStallEntityDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stall_MarketTemplates_MarketTemplateId",
                table: "Stall");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stall",
                table: "Stall");

            migrationBuilder.RenameTable(
                name: "Stall",
                newName: "Stalls");

            migrationBuilder.RenameIndex(
                name: "IX_Stall_MarketTemplateId",
                table: "Stalls",
                newName: "IX_Stalls_MarketTemplateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stalls",
                table: "Stalls",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stalls_MarketTemplates_MarketTemplateId",
                table: "Stalls",
                column: "MarketTemplateId",
                principalTable: "MarketTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stalls_MarketTemplates_MarketTemplateId",
                table: "Stalls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stalls",
                table: "Stalls");

            migrationBuilder.RenameTable(
                name: "Stalls",
                newName: "Stall");

            migrationBuilder.RenameIndex(
                name: "IX_Stalls_MarketTemplateId",
                table: "Stall",
                newName: "IX_Stall_MarketTemplateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stall",
                table: "Stall",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stall_MarketTemplates_MarketTemplateId",
                table: "Stall",
                column: "MarketTemplateId",
                principalTable: "MarketTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
