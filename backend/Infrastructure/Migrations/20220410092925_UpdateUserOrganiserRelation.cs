using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdateUserOrganiserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Organisers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Organisers_UserId",
                table: "Organisers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisers_UserInfo_UserId",
                table: "Organisers",
                column: "UserId",
                principalTable: "UserInfo",
                principalColumn: "IdentityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisers_UserInfo_UserId",
                table: "Organisers");

            migrationBuilder.DropIndex(
                name: "IX_Organisers_UserId",
                table: "Organisers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Organisers");
        }
    }
}
