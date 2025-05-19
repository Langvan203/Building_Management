using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_field_tnToaNha_tnKhoiNha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "tnKhoiNhas");

            migrationBuilder.AddColumn<int>(
                name: "TrangThaiToaNha",
                table: "tnToaNhas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrangThaiKhoiNha",
                table: "tnKhoiNhas",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThaiToaNha",
                table: "tnToaNhas");

            migrationBuilder.DropColumn(
                name: "TrangThaiKhoiNha",
                table: "tnKhoiNhas");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "tnKhoiNhas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
