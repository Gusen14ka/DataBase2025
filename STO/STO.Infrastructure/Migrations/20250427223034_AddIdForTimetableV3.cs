using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdForTimetableV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1) Переименовать старую таблицу во временную
            migrationBuilder.Sql("ALTER TABLE Timetables RENAME TO _Timetables_old;");

            // 2) Создать новую таблицу с нужной структурой, включая Id PK
            migrationBuilder.Sql(@"
            CREATE TABLE Timetables (
                Id        INTEGER PRIMARY KEY AUTOINCREMENT,
                -- остальные ваши колонки, например:
                NextVisit TEXT NOT NULL,
                CarId     INTEGER NOT NULL
                -- и т. д.
            );
        ");

            // 3) Скопировать данные из старой таблицы в новую
            migrationBuilder.Sql(@"
            INSERT INTO Timetables (NextVisit, CarId /*, …другие колонки… */)
            SELECT NextVisit, CarId /*, … */ FROM _Timetables_old;
        ");

            // 4) Удалить временную таблицу
            migrationBuilder.Sql("DROP TABLE _Timetables_old;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // В Down откатываем изменения: убираем новую таблицу и возвращаем старую
            migrationBuilder.Sql("ALTER TABLE Timetables RENAME TO _Timetables_new;");

            migrationBuilder.Sql(@"
            CREATE TABLE Timetables (
                -- старая структура без Id
                NextVisit TEXT NOT NULL,
                CarId     INTEGER NOT NULL
                -- …
            );
        ");

            migrationBuilder.Sql(@"
            INSERT INTO Timetables (NextVisit, CarId /*, … */)
            SELECT NextVisit, CarId /*, … */ FROM _Timetables_new;
        ");

            migrationBuilder.Sql("DROP TABLE _Timetables_new;");
        }
    }
}
