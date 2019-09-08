using Microsoft.EntityFrameworkCore.Migrations;

namespace TvMazeScraper.Scraper.Migrations
{
    public partial class AddUniqueIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Shows_ExternalId",
                table: "Shows",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CastMembers_ExternalId",
                table: "CastMembers",
                column: "ExternalId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shows_ExternalId",
                table: "Shows");

            migrationBuilder.DropIndex(
                name: "IX_CastMembers_ExternalId",
                table: "CastMembers");
        }
    }
}
