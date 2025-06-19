using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_filed_dvDienDinhMuc_dvNuocDinhMuc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChiSoCuoi",
                table: "dvNuocDinhMucs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChiSoDau",
                table: "dvNuocDinhMucs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "dvNuocDinhMucs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChiSoCuoi",
                table: "dvDienDinhMucs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChiSoDau",
                table: "dvDienDinhMucs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "dvDienDinhMucs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChiSoCuoi",
                table: "dvNuocDinhMucs");

            migrationBuilder.DropColumn(
                name: "ChiSoDau",
                table: "dvNuocDinhMucs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "dvNuocDinhMucs");

            migrationBuilder.DropColumn(
                name: "ChiSoCuoi",
                table: "dvDienDinhMucs");

            migrationBuilder.DropColumn(
                name: "ChiSoDau",
                table: "dvDienDinhMucs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "dvDienDinhMucs");
        }
    }
}
