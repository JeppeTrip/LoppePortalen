using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Migrations
{
    public partial class AddStallTypeEntityDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stalls_MarketTemplates_MarketTemplateId",
                table: "Stalls");

            migrationBuilder.RenameColumn(
                name: "MarketTemplateId",
                table: "Stalls",
                newName: "StallTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Stalls_MarketTemplateId",
                table: "Stalls",
                newName: "IX_Stalls_StallTypeId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Stalls",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Stalls",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Stalls",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Stalls",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StallTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    MarketTemplateId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StallTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StallTypes_MarketTemplates_MarketTemplateId",
                        column: x => x.MarketTemplateId,
                        principalTable: "MarketTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StallTypes_MarketTemplateId",
                table: "StallTypes",
                column: "MarketTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stalls_StallTypes_StallTypeId",
                table: "Stalls",
                column: "StallTypeId",
                principalTable: "StallTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stalls_StallTypes_StallTypeId",
                table: "Stalls");

            migrationBuilder.DropTable(
                name: "StallTypes");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Stalls");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Stalls");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Stalls");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Stalls");

            migrationBuilder.RenameColumn(
                name: "StallTypeId",
                table: "Stalls",
                newName: "MarketTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_Stalls_StallTypeId",
                table: "Stalls",
                newName: "IX_Stalls_MarketTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stalls_MarketTemplates_MarketTemplateId",
                table: "Stalls",
                column: "MarketTemplateId",
                principalTable: "MarketTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
