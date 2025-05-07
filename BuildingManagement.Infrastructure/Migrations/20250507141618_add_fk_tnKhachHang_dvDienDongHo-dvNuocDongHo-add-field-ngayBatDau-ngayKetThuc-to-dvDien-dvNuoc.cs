using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_fk_tnKhachHang_dvDienDongHodvNuocDongHoaddfieldngayBatDaungayKetThuctodvDiendvNuoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayBatDauSuDung",
                table: "dvNuocs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDenHang",
                table: "dvNuocs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MaKH",
                table: "dvNuocDongHos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayBatDauSuDung",
                table: "dvDiens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDenHang",
                table: "dvDiens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MaKH",
                table: "dvDienDongHos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_dvNuocDongHos_MaKH",
                table: "dvNuocDongHos",
                column: "MaKH",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dvDienDongHos_MaKH",
                table: "dvDienDongHos",
                column: "MaKH",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_dvDienDongHos_tnKhachHangs_MaKH",
                table: "dvDienDongHos",
                column: "MaKH",
                principalTable: "tnKhachHangs",
                principalColumn: "MaKH",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dvNuocDongHos_tnKhachHangs_MaKH",
                table: "dvNuocDongHos",
                column: "MaKH",
                principalTable: "tnKhachHangs",
                principalColumn: "MaKH",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dvDienDongHos_tnKhachHangs_MaKH",
                table: "dvDienDongHos");

            migrationBuilder.DropForeignKey(
                name: "FK_dvNuocDongHos_tnKhachHangs_MaKH",
                table: "dvNuocDongHos");

            migrationBuilder.DropIndex(
                name: "IX_dvNuocDongHos_MaKH",
                table: "dvNuocDongHos");

            migrationBuilder.DropIndex(
                name: "IX_dvDienDongHos_MaKH",
                table: "dvDienDongHos");

            migrationBuilder.DropColumn(
                name: "NgayBatDauSuDung",
                table: "dvNuocs");

            migrationBuilder.DropColumn(
                name: "NgayDenHang",
                table: "dvNuocs");

            migrationBuilder.DropColumn(
                name: "MaKH",
                table: "dvNuocDongHos");

            migrationBuilder.DropColumn(
                name: "NgayBatDauSuDung",
                table: "dvDiens");

            migrationBuilder.DropColumn(
                name: "NgayDenHang",
                table: "dvDiens");

            migrationBuilder.DropColumn(
                name: "MaKH",
                table: "dvDienDongHos");
        }
    }
}
