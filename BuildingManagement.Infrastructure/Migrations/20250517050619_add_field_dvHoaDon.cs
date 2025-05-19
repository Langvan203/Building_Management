using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_field_dvHoaDon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MucDoYeuCau",
                table: "tnycYeuCauSuaChuas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdTrangThai",
                table: "tnycYeuCauSuaChuas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaKH",
                table: "dvHoaDons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaMB",
                table: "dvHoaDons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BienSo",
                table: "dvgxTheXes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ViTri",
                table: "dvgxTheXes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "tnycTrangThai",
                columns: table => new
                {
                    IdTrangThai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tnycTrangThai", x => x.IdTrangThai);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tnycYeuCauSuaChuas_IdTrangThai",
                table: "tnycYeuCauSuaChuas",
                column: "IdTrangThai");

            migrationBuilder.CreateIndex(
                name: "IX_dvHoaDons_MaKH",
                table: "dvHoaDons",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_dvHoaDons_MaMB",
                table: "dvHoaDons",
                column: "MaMB");

            migrationBuilder.AddForeignKey(
                name: "FK_dvHoaDons_tnKhachHangs_MaKH",
                table: "dvHoaDons",
                column: "MaKH",
                principalTable: "tnKhachHangs",
                principalColumn: "MaKH");

            migrationBuilder.AddForeignKey(
                name: "FK_dvHoaDons_tnMatBangs_MaMB",
                table: "dvHoaDons",
                column: "MaMB",
                principalTable: "tnMatBangs",
                principalColumn: "MaMB");

            migrationBuilder.AddForeignKey(
                name: "FK_tnycYeuCauSuaChuas_tnycTrangThai_IdTrangThai",
                table: "tnycYeuCauSuaChuas",
                column: "IdTrangThai",
                principalTable: "tnycTrangThai",
                principalColumn: "IdTrangThai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dvHoaDons_tnKhachHangs_MaKH",
                table: "dvHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_dvHoaDons_tnMatBangs_MaMB",
                table: "dvHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_tnycYeuCauSuaChuas_tnycTrangThai_IdTrangThai",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropTable(
                name: "tnycTrangThai");

            migrationBuilder.DropIndex(
                name: "IX_tnycYeuCauSuaChuas_IdTrangThai",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropIndex(
                name: "IX_dvHoaDons_MaKH",
                table: "dvHoaDons");

            migrationBuilder.DropIndex(
                name: "IX_dvHoaDons_MaMB",
                table: "dvHoaDons");

            migrationBuilder.DropColumn(
                name: "IdTrangThai",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropColumn(
                name: "MaKH",
                table: "dvHoaDons");

            migrationBuilder.DropColumn(
                name: "MaMB",
                table: "dvHoaDons");

            migrationBuilder.DropColumn(
                name: "BienSo",
                table: "dvgxTheXes");

            migrationBuilder.DropColumn(
                name: "ViTri",
                table: "dvgxTheXes");

            migrationBuilder.AlterColumn<int>(
                name: "MucDoYeuCau",
                table: "tnycYeuCauSuaChuas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
