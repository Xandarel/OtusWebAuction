using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtusWebAuction.DB.Migrations
{
    /// <inheritdoc />
    public partial class RenameLotDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discription",
                table: "Lots",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Lots",
                newName: "Discription");
        }
    }
}
