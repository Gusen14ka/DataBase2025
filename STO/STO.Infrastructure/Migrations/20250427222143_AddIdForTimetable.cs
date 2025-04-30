using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdForTimetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Timetables",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Timetables");
        }
    }
}
