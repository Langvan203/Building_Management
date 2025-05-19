using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_filed_tnycYeuCauSuaChua : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaMB",
                table: "tnycYeuCauSuaChuas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tnycYeuCauSuaChuas_MaMB",
                table: "tnycYeuCauSuaChuas",
                column: "MaMB");

            migrationBuilder.AddForeignKey(
                name: "FK_tnycYeuCauSuaChuas_tnMatBangs_MaMB",
                table: "tnycYeuCauSuaChuas",
                column: "MaMB",
                principalTable: "tnMatBangs",
                principalColumn: "MaMB");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tnycYeuCauSuaChuas_tnMatBangs_MaMB",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropIndex(
                name: "IX_tnycYeuCauSuaChuas_MaMB",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropColumn(
                name: "MaMB",
                table: "tnycYeuCauSuaChuas");
        }
    }
}
