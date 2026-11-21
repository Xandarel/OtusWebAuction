using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtusWebAuction.DB.Migrations
{
    /// <inheritdoc />
    public partial class MakeModerationStatusNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ModerationStatus",
                table: "Lots",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            // До исправления CreateLot всегда проставлял Moderating (1),
            // хотя такие лоты ещё не отправлялись на модерацию.
            migrationBuilder.Sql("""
                UPDATE "Lots"
                SET "ModerationStatus" = NULL
                WHERE "ModerationStatus" = 1;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ModerationStatus",
                table: "Lots",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
