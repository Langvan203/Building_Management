using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_filed_dvDienDongHo_dvNuocDongHo_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaKN",
                table: "dvNuocDongHos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaTL",
                table: "dvNuocDongHos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaKN",
                table: "dvDienDongHos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaTL",
                table: "dvDienDongHos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_dvNuocDongHos_MaKN",
                table: "dvNuocDongHos",
                column: "MaKN");

            migrationBuilder.CreateIndex(
                name: "IX_dvNuocDongHos_MaTL",
                table: "dvNuocDongHos",
                column: "MaTL");

            migrationBuilder.CreateIndex(
                name: "IX_dvDienDongHos_MaKN",
                table: "dvDienDongHos",
                column: "MaKN");

            migrationBuilder.CreateIndex(
                name: "IX_dvDienDongHos_MaTL",
                table: "dvDienDongHos",
                column: "MaTL");

            migrationBuilder.AddForeignKey(
                name: "FK_dvDienDongHos_tnKhoiNhas_MaKN",
                table: "dvDienDongHos",
                column: "MaKN",
                principalTable: "tnKhoiNhas",
                principalColumn: "MaKN");

            migrationBuilder.AddForeignKey(
                name: "FK_dvDienDongHos_tnTangLaus_MaTL",
                table: "dvDienDongHos",
                column: "MaTL",
                principalTable: "tnTangLaus",
                principalColumn: "MaTL");

            migrationBuilder.AddForeignKey(
                name: "FK_dvNuocDongHos_tnKhoiNhas_MaKN",
                table: "dvNuocDongHos",
                column: "MaKN",
                principalTable: "tnKhoiNhas",
                principalColumn: "MaKN");

            migrationBuilder.AddForeignKey(
                name: "FK_dvNuocDongHos_tnTangLaus_MaTL",
                table: "dvNuocDongHos",
                column: "MaTL",
                principalTable: "tnTangLaus",
                principalColumn: "MaTL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dvDienDongHos_tnKhoiNhas_MaKN",
                table: "dvDienDongHos");

            migrationBuilder.DropForeignKey(
                name: "FK_dvDienDongHos_tnTangLaus_MaTL",
                table: "dvDienDongHos");

            migrationBuilder.DropForeignKey(
                name: "FK_dvNuocDongHos_tnKhoiNhas_MaKN",
                table: "dvNuocDongHos");

            migrationBuilder.DropForeignKey(
                name: "FK_dvNuocDongHos_tnTangLaus_MaTL",
                table: "dvNuocDongHos");

            migrationBuilder.DropIndex(
                name: "IX_dvNuocDongHos_MaKN",
                table: "dvNuocDongHos");

            migrationBuilder.DropIndex(
                name: "IX_dvNuocDongHos_MaTL",
                table: "dvNuocDongHos");

            migrationBuilder.DropIndex(
                name: "IX_dvDienDongHos_MaKN",
                table: "dvDienDongHos");

            migrationBuilder.DropIndex(
                name: "IX_dvDienDongHos_MaTL",
                table: "dvDienDongHos");

            migrationBuilder.DropColumn(
                name: "MaKN",
                table: "dvNuocDongHos");

            migrationBuilder.DropColumn(
                name: "MaTL",
                table: "dvNuocDongHos");

            migrationBuilder.DropColumn(
                name: "MaKN",
                table: "dvDienDongHos");

            migrationBuilder.DropColumn(
                name: "MaTL",
                table: "dvDienDongHos");
        }
    }
}
