using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_field_tnMatBang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaKN",
                table: "tnMatBangs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tnMatBangs_MaKN",
                table: "tnMatBangs",
                column: "MaKN");

            migrationBuilder.AddForeignKey(
                name: "FK_tnMatBangs_tnKhoiNhas_MaKN",
                table: "tnMatBangs",
                column: "MaKN",
                principalTable: "tnKhoiNhas",
                principalColumn: "MaKN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tnMatBangs_tnKhoiNhas_MaKN",
                table: "tnMatBangs");

            migrationBuilder.DropIndex(
                name: "IX_tnMatBangs_MaKN",
                table: "tnMatBangs");

            migrationBuilder.DropColumn(
                name: "MaKN",
                table: "tnMatBangs");
        }
    }
}
