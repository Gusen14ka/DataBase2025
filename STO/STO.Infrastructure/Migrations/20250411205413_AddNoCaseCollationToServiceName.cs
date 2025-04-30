using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNoCaseCollationToServiceName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TABLE Services_temp (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    Name TEXT COLLATE NOCASE NOT NULL,
                    Price REAL NOT NULL,
                    NextVisit TEXT NULL
                );

                INSERT INTO Services_temp (Id, Name, Price, NextVisit)
                SELECT Id, Name, Price, NextVisit FROM Services;

                DROP TABLE Services;

                ALTER TABLE Services_temp RENAME TO Services;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
