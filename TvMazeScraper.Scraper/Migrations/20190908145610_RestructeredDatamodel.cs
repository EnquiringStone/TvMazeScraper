using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TvMazeScraper.Scraper.Migrations
{
    public partial class RestructeredDatamodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CastMembers_Shows_ShowId",
                table: "CastMembers");

            migrationBuilder.DropIndex(
                name: "IX_CastMembers_ExternalId",
                table: "CastMembers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "CastMembers");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "CastMembers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CastMembers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShowId",
                table: "CastMembers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "CastMembers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExternalId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scrapes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    IsScrapeInProgress = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scrapes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CastMembers_PersonId",
                table: "CastMembers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_People_ExternalId",
                table: "People",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scrapes_Name",
                table: "Scrapes",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CastMembers_People_PersonId",
                table: "CastMembers",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CastMembers_Shows_ShowId",
                table: "CastMembers",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CastMembers_People_PersonId",
                table: "CastMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_CastMembers_Shows_ShowId",
                table: "CastMembers");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Scrapes");

            migrationBuilder.DropIndex(
                name: "IX_CastMembers_PersonId",
                table: "CastMembers");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "CastMembers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShowId",
                table: "CastMembers",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "CastMembers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ExternalId",
                table: "CastMembers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CastMembers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CastMembers_ExternalId",
                table: "CastMembers",
                column: "ExternalId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CastMembers_Shows_ShowId",
                table: "CastMembers",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
