using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix_field_dvDichVu_dvDichVuSuDung : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonGia",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropColumn(
                name: "DonViTinh",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropColumn(
                name: "IsThanhToanTheoKy",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropColumn(
                name: "KyThanhToan",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropColumn(
                name: "TienBVMT",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropColumn(
                name: "TienVAT",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropColumn(
                name: "TyLeBVMT",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropColumn(
                name: "TyLeVAT",
                table: "dvDichVuSuDungs");

            migrationBuilder.AddColumn<decimal>(
                name: "DonGia",
                table: "dvDichVus",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "DonViTinh",
                table: "dvDichVus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsThanhToanTheoKy",
                table: "dvDichVus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "KyThanhToan",
                table: "dvDichVus",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<decimal>(
                name: "TyLeBVMT",
                table: "dvDichVus",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TyLeVAT",
                table: "dvDichVus",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonGia",
                table: "dvDichVus");

            migrationBuilder.DropColumn(
                name: "DonViTinh",
                table: "dvDichVus");

            migrationBuilder.DropColumn(
                name: "IsThanhToanTheoKy",
                table: "dvDichVus");

            migrationBuilder.DropColumn(
                name: "KyThanhToan",
                table: "dvDichVus");

            migrationBuilder.DropColumn(
                name: "TienBVMT",
                table: "dvDichVus");

            migrationBuilder.DropColumn(
                name: "TienVAT",
                table: "dvDichVus");

            migrationBuilder.DropColumn(
                name: "TyLeBVMT",
                table: "dvDichVus");

            migrationBuilder.DropColumn(
                name: "TyLeVAT",
                table: "dvDichVus");

            migrationBuilder.AddColumn<decimal>(
                name: "DonGia",
                table: "dvDichVuSuDungs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "DonViTinh",
                table: "dvDichVuSuDungs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsThanhToanTheoKy",
                table: "dvDichVuSuDungs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "KyThanhToan",
                table: "dvDichVuSuDungs",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<decimal>(
                name: "TyLeBVMT",
                table: "dvDichVuSuDungs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TyLeVAT",
                table: "dvDichVuSuDungs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
