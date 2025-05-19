using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix_filed_dvDienDongHo_dvNuocDongHo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_dvNuocDongHos_MaKH",
                table: "dvNuocDongHos",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_dvDienDongHos_MaKH",
                table: "dvDienDongHos",
                column: "MaKH");

            migrationBuilder.AddForeignKey(
                name: "FK_dvDienDongHos_tnKhachHangs_MaKH",
                table: "dvDienDongHos",
                column: "MaKH",
                principalTable: "tnKhachHangs",
                principalColumn: "MaKH");

            migrationBuilder.AddForeignKey(
                name: "FK_dvNuocDongHos_tnKhachHangs_MaKH",
                table: "dvNuocDongHos",
                column: "MaKH",
                principalTable: "tnKhachHangs",
                principalColumn: "MaKH");
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
    }
}
