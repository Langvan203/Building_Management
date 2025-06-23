using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_field_nkbtLichSuBaoTri_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaKeHoach",
                table: "nkbtLichSuBaoTris",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_nkbtLichSuBaoTris_MaKeHoach",
                table: "nkbtLichSuBaoTris",
                column: "MaKeHoach");

            migrationBuilder.AddForeignKey(
                name: "FK_nkbtLichSuBaoTris_nkbtKeHoachBaoTris_MaKeHoach",
                table: "nkbtLichSuBaoTris",
                column: "MaKeHoach",
                principalTable: "nkbtKeHoachBaoTris",
                principalColumn: "MaKeHoach");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nkbtLichSuBaoTris_nkbtKeHoachBaoTris_MaKeHoach",
                table: "nkbtLichSuBaoTris");

            migrationBuilder.DropIndex(
                name: "IX_nkbtLichSuBaoTris_MaKeHoach",
                table: "nkbtLichSuBaoTris");

            migrationBuilder.DropColumn(
                name: "MaKeHoach",
                table: "nkbtLichSuBaoTris");
        }
    }
}
