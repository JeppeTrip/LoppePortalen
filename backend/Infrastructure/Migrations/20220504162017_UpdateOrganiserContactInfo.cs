using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Migrations
{
    public partial class UpdateOrganiserContactInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInformations_Organisers_OrganiserId",
                table: "ContactInformations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactInformations",
                table: "ContactInformations");

            migrationBuilder.DropIndex(
                name: "IX_ContactInformations_OrganiserId",
                table: "ContactInformations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ContactInformations");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ContactInformations",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactInformations",
                table: "ContactInformations",
                columns: new[] { "OrganiserId", "Value" });

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInformations_Organisers_OrganiserId",
                table: "ContactInformations",
                column: "OrganiserId",
                principalTable: "Organisers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInformations_Organisers_OrganiserId",
                table: "ContactInformations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactInformations",
                table: "ContactInformations");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ContactInformations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ContactInformations",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactInformations",
                table: "ContactInformations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformations_OrganiserId",
                table: "ContactInformations",
                column: "OrganiserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInformations_Organisers_OrganiserId",
                table: "ContactInformations",
                column: "OrganiserId",
                principalTable: "Organisers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
