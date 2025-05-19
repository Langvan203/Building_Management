using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_nvDanhGia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nvDanhGias",
                columns: table => new
                {
                    MaDanhGia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiemDanhGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaNV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nvDanhGias", x => x.MaDanhGia);
                    table.ForeignKey(
                        name: "FK_nvDanhGias_tnNhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "tnNhanViens",
                        principalColumn: "MaNV");
                });

            migrationBuilder.CreateIndex(
                name: "IX_nvDanhGias_MaNV",
                table: "nvDanhGias",
                column: "MaNV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nvDanhGias");
        }
    }
}
