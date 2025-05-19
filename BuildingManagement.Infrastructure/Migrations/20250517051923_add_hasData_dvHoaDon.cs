using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_hasData_dvHoaDon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tnycYeuCauSuaChuas_tnycTrangThai_IdTrangThai",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tnycTrangThai",
                table: "tnycTrangThai");

            migrationBuilder.RenameTable(
                name: "tnycTrangThai",
                newName: "tnycTrangThais");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "tnycTrangThais",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NguoiSua",
                table: "tnycTrangThais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NguoiTao",
                table: "tnycTrangThais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "tnycTrangThais",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_tnycTrangThais",
                table: "tnycTrangThais",
                column: "IdTrangThai");

            migrationBuilder.InsertData(
                table: "tnycTrangThais",
                columns: new[] { "IdTrangThai", "CreatedDate", "NguoiSua", "NguoiTao", "TenTrangThai", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Admin", "Chờ duyệt", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Admin", "Đã duyệt", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Admin", "Đang thực hiện", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Admin", "Đã hoàn thành", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_tnycYeuCauSuaChuas_tnycTrangThais_IdTrangThai",
                table: "tnycYeuCauSuaChuas",
                column: "IdTrangThai",
                principalTable: "tnycTrangThais",
                principalColumn: "IdTrangThai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tnycYeuCauSuaChuas_tnycTrangThais_IdTrangThai",
                table: "tnycYeuCauSuaChuas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tnycTrangThais",
                table: "tnycTrangThais");

            migrationBuilder.DeleteData(
                table: "tnycTrangThais",
                keyColumn: "IdTrangThai",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tnycTrangThais",
                keyColumn: "IdTrangThai",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tnycTrangThais",
                keyColumn: "IdTrangThai",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tnycTrangThais",
                keyColumn: "IdTrangThai",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "tnycTrangThais");

            migrationBuilder.DropColumn(
                name: "NguoiSua",
                table: "tnycTrangThais");

            migrationBuilder.DropColumn(
                name: "NguoiTao",
                table: "tnycTrangThais");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "tnycTrangThais");

            migrationBuilder.RenameTable(
                name: "tnycTrangThais",
                newName: "tnycTrangThai");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tnycTrangThai",
                table: "tnycTrangThai",
                column: "IdTrangThai");

            migrationBuilder.AddForeignKey(
                name: "FK_tnycYeuCauSuaChuas_tnycTrangThai_IdTrangThai",
                table: "tnycYeuCauSuaChuas",
                column: "IdTrangThai",
                principalTable: "tnycTrangThai",
                principalColumn: "IdTrangThai");
        }
    }
}
