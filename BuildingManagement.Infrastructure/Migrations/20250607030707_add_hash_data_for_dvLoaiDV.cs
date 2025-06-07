using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_hash_data_for_dvLoaiDV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "dvLoaiDVs",
                columns: new[] { "MaLDV", "CreatedDate", "Icon", "IsEssential", "MaTN", "MoTa", "NguoiSua", "NguoiTao", "TenLDV", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "<Zap className=\"h-5 w-5 text-yellow-500\" />", true, null, "Dịch vụ điện cho cư dân", "", "Admin", "Dịch vụ điện", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "<Droplet className=\"h-5 w-5 text-blue-500\" />", true, null, "Dịch vụ nước cho cư dân", "", "Admin", "Dịch vụ nước", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "<Wifi className=\"h-5 w-5 text-purple-500\" />", true, null, "Dịch vụ Internet cho cư dân", "", "Admin", "Dịch vụ Internet", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "<Car className=\"h-5 w-5 text-gray-500\" />", true, null, "Dịch vụ gửi xe cho cư dân", "", "Admin", "Dịch vụ gửi xe", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "<Dumbbell className=\"h-5 w-5 text-green-500\" />", false, null, "Dịch vụ phòng tập Gym cho cư dân", "", "Admin", "Dịch vụ Gym", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "dvLoaiDVs",
                keyColumn: "MaLDV",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "dvLoaiDVs",
                keyColumn: "MaLDV",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "dvLoaiDVs",
                keyColumn: "MaLDV",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "dvLoaiDVs",
                keyColumn: "MaLDV",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "dvLoaiDVs",
                keyColumn: "MaLDV",
                keyValue: 5);
        }
    }
}
