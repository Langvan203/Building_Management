using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_field_tnycYeuCauSuaChua : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "tnycYeuCauSuaChuas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "tnycYeuCauSuaChuas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaKN",
                table: "tnycYeuCauSuaChuas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaTL",
                table: "tnycYeuCauSuaChuas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MoTa",
                table: "tnycYeuCauSuaChuas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TieuDe",
                table: "tnycYeuCauSuaChuas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "YeuCau_NhanVien",
                columns: table => new
                {
                    MaYC = table.Column<int>(type: "int", nullable: false),
                    MaNV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YeuCau_NhanVien", x => new { x.MaYC, x.MaNV });
                    table.ForeignKey(
                        name: "FK_YeuCau_NhanVien_tnNhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "tnNhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YeuCau_NhanVien_tnycYeuCauSuaChuas_MaYC",
                        column: x => x.MaYC,
                        principalTable: "tnycYeuCauSuaChuas",
                        principalColumn: "MaYC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tnycYeuCauSuaChuas_MaKN",
                table: "tnycYeuCauSuaChuas",
                column: "MaKN");

            migrationBuilder.CreateIndex(
                name: "IX_tnycYeuCauSuaChuas_MaTL",
                table: "tnycYeuCauSuaChuas",
                column: "MaTL");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCau_NhanVien_MaNV",
                table: "YeuCau_NhanVien",
                column: "MaNV");

            migrationBuilder.AddForeignKey(
                name: "FK_tnycYeuCauSuaChuas_tnKhoiNhas_MaKN",
                table: "tnycYeuCauSuaChuas",
                column: "MaKN",
                principalTable: "tnKhoiNhas",
                principalColumn: "MaKN");

            migrationBuilder.AddForeignKey(
                name: "FK_tnycYeuCauSuaChuas_tnTangLaus_MaTL",
                table: "tnycYeuCauSuaChuas",
                column: "MaTL",
                principalTable: "tnTangLaus",
                principalColumn: "MaTL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tnycYeuCauSuaChuas_tnKhoiNhas_MaKN",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropForeignKey(
                name: "FK_tnycYeuCauSuaChuas_tnTangLaus_MaTL",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropTable(
                name: "YeuCau_NhanVien");

            migrationBuilder.DropIndex(
                name: "IX_tnycYeuCauSuaChuas_MaKN",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropIndex(
                name: "IX_tnycYeuCauSuaChuas_MaTL",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropColumn(
                name: "MaKN",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropColumn(
                name: "MaTL",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropColumn(
                name: "MoTa",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropColumn(
                name: "TieuDe",
                table: "tnycYeuCauSuaChuas");
        }
    }
}
