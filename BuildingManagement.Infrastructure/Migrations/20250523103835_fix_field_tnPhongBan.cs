using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix_field_tnPhongBan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaTN",
                table: "tnPhongBans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tnPhongBans_MaTN",
                table: "tnPhongBans",
                column: "MaTN");

            migrationBuilder.AddForeignKey(
                name: "FK_tnPhongBans_tnToaNhas_MaTN",
                table: "tnPhongBans",
                column: "MaTN",
                principalTable: "tnToaNhas",
                principalColumn: "MaTN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tnPhongBans_tnToaNhas_MaTN",
                table: "tnPhongBans");

            migrationBuilder.DropIndex(
                name: "IX_tnPhongBans_MaTN",
                table: "tnPhongBans");

            migrationBuilder.DropColumn(
                name: "MaTN",
                table: "tnPhongBans");
        }
    }
}
