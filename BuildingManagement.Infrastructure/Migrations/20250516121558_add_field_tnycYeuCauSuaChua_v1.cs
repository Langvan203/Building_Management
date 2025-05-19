using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_field_tnycYeuCauSuaChua_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MucDoYeuCau",
                table: "tnycYeuCauSuaChuas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MucDoYeuCau",
                table: "tnycYeuCauSuaChuas");
        }
    }
}
