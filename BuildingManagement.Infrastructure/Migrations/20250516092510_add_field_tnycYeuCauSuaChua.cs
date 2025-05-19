using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_field_tnycYeuCauSuaChua : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaTN",
                table: "tnycYeuCauSuaChuas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tnycYeuCauSuaChuas_MaTN",
                table: "tnycYeuCauSuaChuas",
                column: "MaTN");

            migrationBuilder.AddForeignKey(
                name: "FK_tnycYeuCauSuaChuas_tnToaNhas_MaTN",
                table: "tnycYeuCauSuaChuas",
                column: "MaTN",
                principalTable: "tnToaNhas",
                principalColumn: "MaTN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tnycYeuCauSuaChuas_tnToaNhas_MaTN",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropIndex(
                name: "IX_tnycYeuCauSuaChuas_MaTN",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropColumn(
                name: "MaTN",
                table: "tnycYeuCauSuaChuas");
        }
    }
}
