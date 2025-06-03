using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix_field_dvDichVu_dvDichVuSuDung_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TienBVMT",
                table: "dvDichVus");

            migrationBuilder.DropColumn(
                name: "TienVAT",
                table: "dvDichVus");

            migrationBuilder.AddColumn<decimal>(
                name: "TienBVMT",
                table: "dvDichVuSuDungs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TienVAT",
                table: "dvDichVuSuDungs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TienBVMT",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropColumn(
                name: "TienVAT",
                table: "dvDichVuSuDungs");

            migrationBuilder.AddColumn<decimal>(
                name: "TienBVMT",
                table: "dvDichVus",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TienVAT",
                table: "dvDichVus",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
