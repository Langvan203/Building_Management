using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix_field_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "tnKhoiNhas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaKN",
                table: "tnKhachHangs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaTL",
                table: "tnKhachHangs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaTN",
                table: "tnKhachHangs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaTN",
                table: "dvLoaiDVs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaKN",
                table: "dvHoaDons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaTL",
                table: "dvHoaDons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaTN",
                table: "dvHoaDons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaKN",
                table: "dvDichVuSuDungs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaTL",
                table: "dvDichVuSuDungs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaTN",
                table: "dvDichVuSuDungs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaTN",
                table: "dvDichVus",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tnKhachHangs_MaKN",
                table: "tnKhachHangs",
                column: "MaKN");

            migrationBuilder.CreateIndex(
                name: "IX_tnKhachHangs_MaTL",
                table: "tnKhachHangs",
                column: "MaTL");

            migrationBuilder.CreateIndex(
                name: "IX_tnKhachHangs_MaTN",
                table: "tnKhachHangs",
                column: "MaTN");

            migrationBuilder.CreateIndex(
                name: "IX_dvLoaiDVs_MaTN",
                table: "dvLoaiDVs",
                column: "MaTN");

            migrationBuilder.CreateIndex(
                name: "IX_dvHoaDons_MaKN",
                table: "dvHoaDons",
                column: "MaKN");

            migrationBuilder.CreateIndex(
                name: "IX_dvHoaDons_MaTL",
                table: "dvHoaDons",
                column: "MaTL");

            migrationBuilder.CreateIndex(
                name: "IX_dvHoaDons_MaTN",
                table: "dvHoaDons",
                column: "MaTN");

            migrationBuilder.CreateIndex(
                name: "IX_dvDichVuSuDungs_MaKN",
                table: "dvDichVuSuDungs",
                column: "MaKN");

            migrationBuilder.CreateIndex(
                name: "IX_dvDichVuSuDungs_MaTL",
                table: "dvDichVuSuDungs",
                column: "MaTL");

            migrationBuilder.CreateIndex(
                name: "IX_dvDichVuSuDungs_MaTN",
                table: "dvDichVuSuDungs",
                column: "MaTN");

            migrationBuilder.CreateIndex(
                name: "IX_dvDichVus_MaTN",
                table: "dvDichVus",
                column: "MaTN");

            migrationBuilder.AddForeignKey(
                name: "FK_dvDichVus_tnToaNhas_MaTN",
                table: "dvDichVus",
                column: "MaTN",
                principalTable: "tnToaNhas",
                principalColumn: "MaTN");

            migrationBuilder.AddForeignKey(
                name: "FK_dvDichVuSuDungs_tnKhoiNhas_MaKN",
                table: "dvDichVuSuDungs",
                column: "MaKN",
                principalTable: "tnKhoiNhas",
                principalColumn: "MaKN");

            migrationBuilder.AddForeignKey(
                name: "FK_dvDichVuSuDungs_tnTangLaus_MaTL",
                table: "dvDichVuSuDungs",
                column: "MaTL",
                principalTable: "tnTangLaus",
                principalColumn: "MaTL");

            migrationBuilder.AddForeignKey(
                name: "FK_dvDichVuSuDungs_tnToaNhas_MaTN",
                table: "dvDichVuSuDungs",
                column: "MaTN",
                principalTable: "tnToaNhas",
                principalColumn: "MaTN");

            migrationBuilder.AddForeignKey(
                name: "FK_dvHoaDons_tnKhoiNhas_MaKN",
                table: "dvHoaDons",
                column: "MaKN",
                principalTable: "tnKhoiNhas",
                principalColumn: "MaKN");

            migrationBuilder.AddForeignKey(
                name: "FK_dvHoaDons_tnTangLaus_MaTL",
                table: "dvHoaDons",
                column: "MaTL",
                principalTable: "tnTangLaus",
                principalColumn: "MaTL");

            migrationBuilder.AddForeignKey(
                name: "FK_dvHoaDons_tnToaNhas_MaTN",
                table: "dvHoaDons",
                column: "MaTN",
                principalTable: "tnToaNhas",
                principalColumn: "MaTN");

            migrationBuilder.AddForeignKey(
                name: "FK_dvLoaiDVs_tnToaNhas_MaTN",
                table: "dvLoaiDVs",
                column: "MaTN",
                principalTable: "tnToaNhas",
                principalColumn: "MaTN");

            migrationBuilder.AddForeignKey(
                name: "FK_tnKhachHangs_tnKhoiNhas_MaKN",
                table: "tnKhachHangs",
                column: "MaKN",
                principalTable: "tnKhoiNhas",
                principalColumn: "MaKN");

            migrationBuilder.AddForeignKey(
                name: "FK_tnKhachHangs_tnTangLaus_MaTL",
                table: "tnKhachHangs",
                column: "MaTL",
                principalTable: "tnTangLaus",
                principalColumn: "MaTL");

            migrationBuilder.AddForeignKey(
                name: "FK_tnKhachHangs_tnToaNhas_MaTN",
                table: "tnKhachHangs",
                column: "MaTN",
                principalTable: "tnToaNhas",
                principalColumn: "MaTN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dvDichVus_tnToaNhas_MaTN",
                table: "dvDichVus");

            migrationBuilder.DropForeignKey(
                name: "FK_dvDichVuSuDungs_tnKhoiNhas_MaKN",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropForeignKey(
                name: "FK_dvDichVuSuDungs_tnTangLaus_MaTL",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropForeignKey(
                name: "FK_dvDichVuSuDungs_tnToaNhas_MaTN",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropForeignKey(
                name: "FK_dvHoaDons_tnKhoiNhas_MaKN",
                table: "dvHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_dvHoaDons_tnTangLaus_MaTL",
                table: "dvHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_dvHoaDons_tnToaNhas_MaTN",
                table: "dvHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_dvLoaiDVs_tnToaNhas_MaTN",
                table: "dvLoaiDVs");

            migrationBuilder.DropForeignKey(
                name: "FK_tnKhachHangs_tnKhoiNhas_MaKN",
                table: "tnKhachHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_tnKhachHangs_tnTangLaus_MaTL",
                table: "tnKhachHangs");

            migrationBuilder.DropForeignKey(
                name: "FK_tnKhachHangs_tnToaNhas_MaTN",
                table: "tnKhachHangs");

            migrationBuilder.DropIndex(
                name: "IX_tnKhachHangs_MaKN",
                table: "tnKhachHangs");

            migrationBuilder.DropIndex(
                name: "IX_tnKhachHangs_MaTL",
                table: "tnKhachHangs");

            migrationBuilder.DropIndex(
                name: "IX_tnKhachHangs_MaTN",
                table: "tnKhachHangs");

            migrationBuilder.DropIndex(
                name: "IX_dvLoaiDVs_MaTN",
                table: "dvLoaiDVs");

            migrationBuilder.DropIndex(
                name: "IX_dvHoaDons_MaKN",
                table: "dvHoaDons");

            migrationBuilder.DropIndex(
                name: "IX_dvHoaDons_MaTL",
                table: "dvHoaDons");

            migrationBuilder.DropIndex(
                name: "IX_dvHoaDons_MaTN",
                table: "dvHoaDons");

            migrationBuilder.DropIndex(
                name: "IX_dvDichVuSuDungs_MaKN",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropIndex(
                name: "IX_dvDichVuSuDungs_MaTL",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropIndex(
                name: "IX_dvDichVuSuDungs_MaTN",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropIndex(
                name: "IX_dvDichVus_MaTN",
                table: "dvDichVus");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "tnKhoiNhas");

            migrationBuilder.DropColumn(
                name: "MaKN",
                table: "tnKhachHangs");

            migrationBuilder.DropColumn(
                name: "MaTL",
                table: "tnKhachHangs");

            migrationBuilder.DropColumn(
                name: "MaTN",
                table: "tnKhachHangs");

            migrationBuilder.DropColumn(
                name: "MaTN",
                table: "dvLoaiDVs");

            migrationBuilder.DropColumn(
                name: "MaKN",
                table: "dvHoaDons");

            migrationBuilder.DropColumn(
                name: "MaTL",
                table: "dvHoaDons");

            migrationBuilder.DropColumn(
                name: "MaTN",
                table: "dvHoaDons");

            migrationBuilder.DropColumn(
                name: "MaKN",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropColumn(
                name: "MaTL",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropColumn(
                name: "MaTN",
                table: "dvDichVuSuDungs");

            migrationBuilder.DropColumn(
                name: "MaTN",
                table: "dvDichVus");
        }
    }
}
