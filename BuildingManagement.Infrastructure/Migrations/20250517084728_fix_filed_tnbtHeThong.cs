using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix_filed_tnbtHeThong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaMB",
                table: "tnbtHeThongs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaMB",
                table: "tnbtHeThongs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
