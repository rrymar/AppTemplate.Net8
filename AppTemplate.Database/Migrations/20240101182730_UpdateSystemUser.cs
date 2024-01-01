using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppTemplate.Net8.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSystemUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedById",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedById",
                value: null);
        }
    }
}
