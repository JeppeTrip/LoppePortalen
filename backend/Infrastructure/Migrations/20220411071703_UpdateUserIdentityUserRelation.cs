using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdateUserIdentityUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisers_UserInfo_UserId",
                table: "Organisers");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityId",
                table: "UserInfo",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Organisers",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisers_UserInfo_UserId",
                table: "Organisers",
                column: "UserId",
                principalTable: "UserInfo",
                principalColumn: "IdentityId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisers_UserInfo_UserId",
                table: "Organisers");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdentityId",
                table: "UserInfo",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Organisers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Organisers_UserInfo_UserId",
                table: "Organisers",
                column: "UserId",
                principalTable: "UserInfo",
                principalColumn: "IdentityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
