using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class MerchantContactInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MerchantContactInfos",
                columns: table => new
                {
                    Value = table.Column<string>(type: "text", nullable: false),
                    MerchantId = table.Column<int>(type: "integer", nullable: false),
                    ContactType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantContactInfos", x => new { x.MerchantId, x.Value });
                    table.ForeignKey(
                        name: "FK_MerchantContactInfos_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MerchantContactInfos");
        }
    }
}
