using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_filed_dvDienDongHo_dvNuocDongHo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoDH",
                table: "dvNuocDongHos",
                newName: "SoDongHo");

            migrationBuilder.AddColumn<int>(
                name: "ChiSoSuDung",
                table: "dvNuocDongHos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaTN",
                table: "dvNuocDongHos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai",
                table: "dvNuocDongHos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ChiSoSuDung",
                table: "dvDienDongHos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaTN",
                table: "dvDienDongHos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai",
                table: "dvDienDongHos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_dvNuocDongHos_MaTN",
                table: "dvNuocDongHos",
                column: "MaTN");

            migrationBuilder.CreateIndex(
                name: "IX_dvDienDongHos_MaTN",
                table: "dvDienDongHos",
                column: "MaTN");

            migrationBuilder.AddForeignKey(
                name: "FK_dvDienDongHos_tnToaNhas_MaTN",
                table: "dvDienDongHos",
                column: "MaTN",
                principalTable: "tnToaNhas",
                principalColumn: "MaTN");

            migrationBuilder.AddForeignKey(
                name: "FK_dvNuocDongHos_tnToaNhas_MaTN",
                table: "dvNuocDongHos",
                column: "MaTN",
                principalTable: "tnToaNhas",
                principalColumn: "MaTN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dvDienDongHos_tnToaNhas_MaTN",
                table: "dvDienDongHos");

            migrationBuilder.DropForeignKey(
                name: "FK_dvNuocDongHos_tnToaNhas_MaTN",
                table: "dvNuocDongHos");

            migrationBuilder.DropIndex(
                name: "IX_dvNuocDongHos_MaTN",
                table: "dvNuocDongHos");

            migrationBuilder.DropIndex(
                name: "IX_dvDienDongHos_MaTN",
                table: "dvDienDongHos");

            migrationBuilder.DropColumn(
                name: "ChiSoSuDung",
                table: "dvNuocDongHos");

            migrationBuilder.DropColumn(
                name: "MaTN",
                table: "dvNuocDongHos");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "dvNuocDongHos");

            migrationBuilder.DropColumn(
                name: "ChiSoSuDung",
                table: "dvDienDongHos");

            migrationBuilder.DropColumn(
                name: "MaTN",
                table: "dvDienDongHos");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "dvDienDongHos");

            migrationBuilder.RenameColumn(
                name: "SoDongHo",
                table: "dvNuocDongHos",
                newName: "SoDH");
        }
    }
}
