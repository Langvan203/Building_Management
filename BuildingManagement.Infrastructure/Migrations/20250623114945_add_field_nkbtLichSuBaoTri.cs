using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_field_nkbtLichSuBaoTri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GhiChi",
                table: "nkbtLichSuBaoTris",
                newName: "TieuDe");

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "nkbtLichSuBaoTris",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "nkbtLichSuBaoTris");

            migrationBuilder.RenameColumn(
                name: "TieuDe",
                table: "nkbtLichSuBaoTris",
                newName: "GhiChi");
        }
    }
}
