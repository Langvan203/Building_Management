using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_filed_tnbtHeThong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaTN",
                table: "tnbtHeThongs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tnbtHeThongs_MaTN",
                table: "tnbtHeThongs",
                column: "MaTN");

            migrationBuilder.AddForeignKey(
                name: "FK_tnbtHeThongs_tnToaNhas_MaTN",
                table: "tnbtHeThongs",
                column: "MaTN",
                principalTable: "tnToaNhas",
                principalColumn: "MaTN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tnbtHeThongs_tnToaNhas_MaTN",
                table: "tnbtHeThongs");

            migrationBuilder.DropIndex(
                name: "IX_tnbtHeThongs_MaTN",
                table: "tnbtHeThongs");

            migrationBuilder.DropColumn(
                name: "MaTN",
                table: "tnbtHeThongs");
        }
    }
}
